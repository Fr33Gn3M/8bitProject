using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ICell = NPOI.SS.UserModel.ICell;

namespace LX.Commons
{
    public class ExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs;
        private bool disposed;

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        public Dictionary<string, DataTable> GetExcelToDataTables(int startRowIndex)
        {
            var dic = new Dictionary<string, DataTable>();
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook(fs);
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook(fs);
            foreach (ISheet item in workbook)
            {
                var dt = ExcelToDataTable(workbook, item.SheetName, startRowIndex);
                dic.Add(item.SheetName, dt);
            }
            return dic;
        }

        public DataTable ExcelToDataTable(IWorkbook workbook, string sheetName, int startRowIndex)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(startRowIndex);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.ToString();
                            if (cellValue != null)
                            {
                                int index = cellValue.IndexOf("(");
                                if (index < 0)
                                    index = cellValue.IndexOf("：");
                                if (index > 0)
                                    cellValue = cellValue.Substring(0, index);

                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + startRowIndex + 1;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, int startRowIndex)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) //2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(startRowIndex);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                int index = cellValue.IndexOf("(");
                                if (index < 0)
                                    index = cellValue.IndexOf("：");
                                if (index > 0)
                                    cellValue = cellValue.Substring(0, index);

                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + startRowIndex;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            FileStream fs = null;

            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
        /// <summary>
        /// 生成的模板以文件流的形式下载到本地
        /// </summary>
        public static void SaveExcelTemplate(string strPath)
        {
            //String strPath = System.Web.HttpContext.Current.Server.MapPath("排洪沟模板.xlsx");
            FileInfo fi = new FileInfo(strPath);
            if (File.Exists(strPath))
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Charset = "GB2312";
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", HttpUtility.UrlEncode(fi.Name)));
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.Default;
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.WriteFile(fi.FullName);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
            }
            else
                throw new Exception("当前模板不存在！");
        }
        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        public static DataTable ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true, int startRow = 0)
        {
            DataTable data = new DataTable();
            ISheet sheet = null;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                Byte[] byData = br.ReadBytes((int)fs.Length);
                IWorkbook workbook = NPOI.SS.UserModel.WorkbookFactory.Create(fs);
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(startRow);
                    int cellCount = firstRow.LastCellNum;
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + startRow + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum + startRow;
                    }
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            ICell cell = row.GetCell(j);
                            if (cell == null) continue;

                            if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                            {
                                dataRow[j] = cell.DateCellValue.ToString();
                            }
                            else
                            {
                                dataRow[j] = cell.ToString();
                            }
                        }
                        data.Rows.Add(dataRow);

                    }
                }
                return data;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        public static DataTable ReadAllSheatExcelToDataTable(string fileName, bool isFirstRowColumn = true)
        {
            DataTable data = new DataTable();

            int startRow = 0;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                Byte[] byData = br.ReadBytes((int)fs.Length);
                IWorkbook workbook = NPOI.SS.UserModel.WorkbookFactory.Create(fs);

                int SheetCount = workbook.NumberOfSheets;

                for (var index = 0; index < SheetCount; index++)
                {
                    ISheet sheet = null;
                    sheet = workbook.GetSheetAt(index);
                    if (sheet != null)
                    {
                        IRow firstRow = sheet.GetRow(0);
                        int cellCount = firstRow.LastCellNum;
                        if (isFirstRowColumn)
                        {
                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue;
                                    if (!string.IsNullOrEmpty(cellValue) && !data.Columns.Contains(cellValue))
                                    {
                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                    }
                                }
                            }
                            startRow = sheet.FirstRowNum + 1;
                        }
                        else
                        {
                            startRow = sheet.FirstRowNum;
                        }
                        int rowCount = sheet.LastRowNum;
                        for (int i = startRow; i <= rowCount; ++i)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue;
                            DataRow dataRow = data.NewRow();
                            for (int j = row.FirstCellNum; j < data.Columns.Count; ++j)
                            {

                                ICell cell = row.GetCell(j);
                                if (cell == null)
                                    continue;
                                if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                                {
                                    dataRow[j] = cell.DateCellValue.ToString();
                                }
                                else
                                {
                                    dataRow[j] = cell.ToString();
                                }
                            }
                            data.Rows.Add(dataRow);
                        }
                    }
                }

                return data;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Dictionary[]数组转为DataTable 
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static DataTable DicToTable(Dictionary<string, object>[] dic)
        {
            var dict = new Dictionary<string, object>();
            DataTable dt = new DataTable();

            dict = dic[0];
            foreach (var ColumnName in dict.Keys)
            {
                dt.Columns.Add(ColumnName, typeof(string));
            }


            foreach (var item in dic)
            {
                DataRow dr = dt.NewRow();
                foreach (KeyValuePair<string, object> x in item)
                {
                    dr[x.Key] = x.Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static void DataToExcel(System.Data.DataTable m_DataTable, string s_FileName)
        {
            string FileName = System.Threading.Thread.GetDomain().BaseDirectory + "EXCEL导出数据\\" + s_FileName + ".xls"; //文件存放路径
            if (System.IO.File.Exists(FileName)) //存在则删除
            {
                System.IO.File.Delete(FileName);
            }
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objStreamWriter;
            string strLine = "";
            objFileStream = new System.IO.FileStream(FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            objStreamWriter = new System.IO.StreamWriter(objFileStream, Encoding.Unicode);
            for (int i = 0; i < m_DataTable.Columns.Count; i++)
            {
                strLine = strLine + m_DataTable.Columns[i].Caption.ToString() + Convert.ToChar(9); //写列标题
            }
            objStreamWriter.WriteLine(strLine);
            strLine = "";
            for (int i = 0; i < m_DataTable.Rows.Count; i++)
            {
                for (int j = 0; j < m_DataTable.Columns.Count; j++)
                {
                    if (m_DataTable.Rows[i].ItemArray[j] == null)
                        strLine = strLine + " " + Convert.ToChar(9); //写内容
                    else
                    {
                        string rowstr = "";
                        rowstr = m_DataTable.Rows[i].ItemArray[j].ToString();
                        if (rowstr.IndexOf("\r\n") > 0)
                            rowstr = rowstr.Replace("\r\n", " ");
                        if (rowstr.IndexOf("\t") > 0)
                            rowstr = rowstr.Replace("\t", " ");
                        strLine = strLine + rowstr + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }
            objStreamWriter.Close();
            objFileStream.Close();

            SaveExcelTemplate(FileName);
        }
    }


}