using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using BitMiracle.LibTiff.Classic;
using System.Drawing.Imaging;
namespace TH.ServerFramework
{
    class OpenTiff
    {
        #region 读入TIF图像的变量设置
        private int width;// 存储读入的TIF图像的宽度
        private int height;//存储读入的TIF图像的高度
        private int imageSize;
        private int[] raster;
        private Bitmap TifMap;//声明一个图像，存储读入的TIF图像

        private DataSet ds;
        private DataTable table;
        private DataRow dataRow;
        private DataColumn column1;
        private DataColumn column2;
        private DataColumn column3;
        private DataColumn column4;
        private DataColumn column5;
        #endregion

        #region 获取某像素红 绿 蓝分量
        public Color GetPixColor(int x, int y, int[] raster, int width, int height)
        {
            int offset = (height - y - 1) * width + x;
            int red = Tiff.GetR(raster[offset]);
            int green = Tiff.GetG(raster[offset]);
            int blue = Tiff.GetB(raster[offset]);

            return Color.FromArgb(red, green, blue);
        }
        #endregion

        #region 获取某像素信息
        public int[] GetInfo(int x, int y, int[] raster, int width, int height)
        {
            int[] rgb = new int[5];
            int offset = (height - y - 1) * width + x;
            int red = Tiff.GetR(raster[offset]);
            int green = Tiff.GetG(raster[offset]);
            int blue = Tiff.GetB(raster[offset]);
            rgb[0] = red;
            rgb[1] = green;
            rgb[2] = blue;
            rgb[3] = x;
            rgb[4] = y;
            return rgb;
        }
        #endregion

        #region 读取Tif图像
        public byte[] ReadTif(string path)
        {
            try
            {
                Tiff image = Tiff.Open(path, "r");
                if (image == null)
                {
                    return null;
                }
                do
                {
                    FieldValue[] value = image.GetField(TiffTag.IMAGEWIDTH);
                    width = value[0].ToInt();

                    value = image.GetField(TiffTag.IMAGELENGTH);
                    height = value[0].ToInt();

                    imageSize = width * height;
                    raster = new int[imageSize];
                    TifMap = new Bitmap(width, height);
                    if (!image.ReadRGBAImage(width, height, raster))
                    {
                        return null;
                    }
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            TifMap.SetPixel(i, j, GetPixColor(i, j, raster, width, height));
                        }
                    }
                }
                while (image.ReadDirectory());
            }
            catch(Exception ex)
            {
            }
            using (var stream = new MemoryStream())
            {
                TifMap.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }
        #endregion

        //#region 显示图像
        //public byte[] GetTifToJpg()
        //{

        //   // form.pictureBox1.Image = TifMap;
        //}
        //#endregion

        #region 读取图像信息

        //public DataSet ReadInfo()
        //{
        //    Tiff image = Tiff.Open(FilePath.Path, "r");
        //    if (image == null)
        //    {
        //        MessageBox.Show("请读取图像");
        //    }
        //    ds = new DataSet();
        //    table = new DataTable();
        //    column1 = new DataColumn("R");
        //    column2= new DataColumn("G");
        //    column3 = new DataColumn("B");
        //    column4 = new DataColumn("X");
        //    column5 = new DataColumn("Y");
        //    table.Columns.Add(column1);
        //    table.Columns.Add(column2);         
        //    table.Columns.Add(column3);
        //    table.Columns.Add(column4);
        //    table.Columns.Add(column5);
        //    for (int i = 0; i < width; i++)
        //    {
        //        for (int j = 0; j < height; j++)
        //        {
        //            int [] rgbxy=GetInfo(i, j, raster, width, height);
        //            dataRow = table.NewRow();
        //            for (int k = 0; k < 5; k++)
        //            {
        //                dataRow[k] = rgbxy[k].ToString();
        //            }
        //            table.Rows.Add(dataRow);
        //        }    
        //    }
        //    ds.Tables.Add(table);
        //    return ds;  
        //}
        #endregion


    }
}
