using FC.Core.AppSetting;
using FC.Core.Models;
using FC.FileBusiness.Services;
using FC.Utils.FileUtils;
using MathNet.Numerics.LinearAlgebra.Solvers;
using NPOI.HPSF;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XWPF.UserModel;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Reflection.Metadata;
using System.Web;
using System.Xml.Linq;

namespace FC.FileBusiness.Impl
{
    public class FileService : IFileService
    {

        public string LawnExcelToWord(Stream fileStream)
        {
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            //获取第一个sheet
            var sheet = workbook.GetSheetAt(0);
            //判断是否获取到 sheet
            if (sheet == null)
                throw new ApiException("sheet为空！");
            //获取第一行，标题行
            var row = sheet.GetRow(0);
            //包装数据到字典list，字典key是标题，value是对应的值
            var dataList = new List<Dictionary<string, string>>();
            for (var i = 1; i <= sheet.LastRowNum; i++)
            {
                var dataDic = new Dictionary<string, string>();
                //注意，列多了一行，不清楚为什么跟行数会不一样
                for (var j = 0; j < row.LastCellNum; j++)
                {
                    if (row.GetCell(j) == null || string.IsNullOrEmpty(row.GetCell(j).ToString())) continue;
                    var dataRow = sheet.GetRow(i);
                    dataDic.Add(row.GetCell(j).ToString(), dataRow.GetCell(j).ToString());
                }
                dataList.Add(dataDic);
            }
            string docName = DateTime.Now.ToString("yyyyMMddhhmmss") + "草地导出.docx";
            string wordModulePath = "Data/草地导出模板.docx";
            string savePath = Path.Combine(AppSettingHelper.ReadString("FilePath", "ExportDir"), docName);
            //拷贝模板文件到导出目录
            FileUtils.CopyFile(wordModulePath, savePath);
            XWPFDocument MyDoc;
            using FileStream file = new(savePath, FileMode.Open, FileAccess.Read);
            MyDoc = new XWPFDocument(file);
            //获取模板原始的所有段落
            var paragraphs = MyDoc.Paragraphs;
            var paraSize = paragraphs.Count;
            //遍历List数据，创建word页，复制页面内容
            dataList.ForEach(dataDic => {
                bool isLast = dataList.Last().Equals(dataDic);
                List<XWPFParagraph> newParagraph = new();
                if (!isLast)
                {
                    //如果不是最后一条数据，就为下一条数据拷贝一个页面出来用来填充内容
                    //先添加一个分页
                    XWPFParagraph xWPFParagraph = MyDoc.CreateParagraph();
                    xWPFParagraph.CreateRun().AddBreak();//分页
                    for(var i = 0; i<paraSize; i++)
                    {
                        var para = paragraphs[i];
                        //创建一个新段落
                        var newPara = MyDoc.CreateParagraph();
                        //复制段落
                        //注意SetParagraph的第二个参数pos是Paragraphs数组的位置
                        //而下面InsertTable的pos是bodyElement的pos
                        MyDoc.SetParagraph(para, MyDoc.Paragraphs.Count - 1);
                        if(i == 0)
                        {
                            //在第1个段落下面添加表格，这是模板确定的
                            //待优化，这里根据模板格式，个性化地在第1个段落下面添加表格
                            int targetIndex = MyDoc.Tables.Count;
                            CopyTable(MyDoc, 0, targetIndex);
                        }
                    }
                }
            });
            //表格内的模板内容替换
            ReplaceText(MyDoc.Tables, dataList);
            //保存修改后的文档
            MemoryStream ms = new();
            MyDoc.Write(ms);
            FileStream fs = new(savePath, FileMode.Create);
            BinaryWriter bw = new(fs);
            bw.Write(ms.GetBuffer());
            bw.Close();
            fs.Close();
            //返回文件名，以供下载
            return docName;
        }

        private void ReplaceText(IList<XWPFTable> tables, List<Dictionary<string, string>> dataList)
        {
            for(var i = 0; i<tables.Count; i++)
            {
                var table = tables[i];
                var dataDic = dataList[i];
                table.Rows.ForEach(row =>
                {
                    row.GetTableCells().ForEach(cell =>
                    {
                        foreach(var para in cell.Paragraphs)
                        {
                            ReplaceCellParagraphs(para, dataDic);
                        }
                    });
                });
            }
        }

        private void ReplaceCellParagraphs(XWPFParagraph para, Dictionary<string, string> dataDic)
        {
            var runs = para.Runs;
            foreach(var run in runs)
            {
                string text = run.ToString();
                foreach (string key in dataDic.Keys)
                {
                    if (text.Contains("{$" + key + "}"))
                    {
                        text = text.Replace("{$" + key + "}", dataDic[key].ToString());
                        break;
                    }
                }
                run.SetText(text);
            }
        }

        private void CopyTable(XWPFDocument fileWord, int sourceIndex, int targetIndex)
        {
            var sourceTable = fileWord.Tables[sourceIndex];
            CT_Tbl sourceCTTBl = fileWord.Document.body.GetTblArray(sourceIndex);

            var targetTable = fileWord.CreateTable();
            fileWord.SetTable(targetIndex, targetTable);
            var targetCTTbl = fileWord.Document.body.GetTblArray()[fileWord.Document.body.GetTblArray().Length - 1];

            targetCTTbl.tblPr = sourceCTTBl.tblPr;
            targetCTTbl.tblGrid = sourceCTTBl.tblGrid;

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                var tbRow = targetTable.CreateRow();
                var targetRow = tbRow.GetCTRow();
                tbRow.RemoveCell(0);
                XWPFTableRow row = sourceTable.Rows[i];
                targetRow.trPr = row.GetCTRow().trPr;
                targetRow.trPr = row.GetCTRow().trPr;
                targetRow.trPr = row.GetCTRow().trPr;
                targetRow.trPr = row.GetCTRow().trPr;
                for (int j = 0; j < row.GetTableCells().Count; j++)
                {
                    var tbCell = tbRow.CreateCell();
                    tbCell.RemoveParagraph(0);
                    var targetCell = tbCell.GetCTTc();
                    XWPFTableCell cell = row.GetTableCells()[j];
                    targetCell.tcPr = cell.GetCTTc().tcPr;
                    for (int z = 0; z < cell.Paragraphs.Count; z++)
                    {
                        var tbPhs = tbCell.AddParagraph();
                        XWPFParagraph para = cell.Paragraphs[z];
                        for (int y = 0; y < para.Runs.Count; y++)
                        {
                            var tbRun = tbPhs.CreateRun();
                            CT_R targetRun = tbRun.GetCTR();

                            XWPFRun run = para.Runs[y];
                            var runCTR = run.GetCTR();
                            targetRun.rPr = runCTR.rPr;
                            targetRun.rsidRPr = runCTR.rsidRPr;
                            targetRun.rsidR = runCTR.rsidR;
                            CT_Text text = targetRun.AddNewT();
                            text.Value = run.Text;
                        }
                    }
                }
            }
            targetTable.RemoveRow(0);
        }
    }
}
