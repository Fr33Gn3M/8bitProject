using Microsoft.Web.Administration;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FD.Commons
{
    public class CommonHelper
    {
        #region 汉字获得其大写首字母方法。
        ///<summary>  
        ///   判断是否为汉字  
        ///</summary>  
        ///<param   name="chrStr">待检测字符串</param>  
        ///<returns>是汉字返回true</returns>
        public static bool IsChineseCharacters(string chrStr)
        {
            Regex reg = new Regex(@"^[\u4e00-\u9fa5]+");
            return reg.IsMatch(chrStr);
        }

        ///<summary>
        /// 得到每个汉字的字首拼音码字母(大写)
        ///</summary>
        ///<param name="chrStr">输入字符串</param>
        ///<returns>返回结果</returns>
        public static string GetHeadCharacter(string chrStr)
        {
            if (chrStr.Contains('/'))
            {
                var index = chrStr.IndexOf('/');
                chrStr = chrStr.Remove(index, 1);
            }
            if (IsChineseCharacters(chrStr) == false)
            {
                return chrStr;
            }
            string strHeadString = string.Empty;
            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
            for (int i = 0; i < chrStr.Length; i++)
            {
                //检测该字符是否为汉字
                //if (!IsChineseCharacters(chrStr.Substring(i, 1)))
                //{
                //    strHeadString += chrStr.Substring(i, 1);
                //    continue;
                //}

                byte[] bytes = gb.GetBytes(chrStr.Substring(i, 1));
                string lowCode = System.Convert.ToString(bytes[0] - 0xA0, 16);
                string hightCode = System.Convert.ToString(bytes[1] - 0xA0, 16);
                int nCode = Convert.ToUInt16(lowCode, 16) * 100 + Convert.ToUInt16(hightCode, 16);      //得到区位码
                strHeadString += FirstLetter(nCode);
            }
            return strHeadString;
        }

        ///<summary>
        /// 通过汉字区位码得到其首字母(大写)
        ///</summary>
        ///<param name="nCode">汉字编码</param>
        ///<returns></returns>
        private static string FirstLetter(int nCode)
        {
            if (nCode >= 1601 && nCode < 1637) return "A";
            if (nCode >= 1637 && nCode < 1833) return "B";
            if (nCode >= 1833 && nCode < 2078) return "C";
            if (nCode >= 2078 && nCode < 2274) return "D";
            if (nCode >= 2274 && nCode < 2302) return "E";
            if (nCode >= 2302 && nCode < 2433) return "F";
            if (nCode >= 2433 && nCode < 2594) return "G";
            if (nCode >= 2594 && nCode < 2787) return "H";
            if (nCode >= 2787 && nCode < 3106) return "J";
            if (nCode >= 3106 && nCode < 3212) return "K";
            if (nCode >= 3212 && nCode < 3472) return "L";
            if (nCode >= 3472 && nCode < 3635) return "M";
            if (nCode >= 3635 && nCode < 3722) return "N";
            if (nCode >= 3722 && nCode < 3730) return "O";
            if (nCode >= 3730 && nCode < 3858) return "P";
            if (nCode >= 3858 && nCode < 4027) return "Q";
            if (nCode >= 4027 && nCode < 4086) return "R";
            if (nCode >= 4086 && nCode < 4390) return "S";
            if (nCode >= 4390 && nCode < 4558) return "T";
            if (nCode >= 4558 && nCode < 4684) return "W";
            if (nCode >= 4684 && nCode < 4925) return "X";
            if (nCode >= 4925 && nCode < 5249) return "Y";
            if (nCode >= 5249 && nCode < 5590) return "Z";
            return "";
        }
        #endregion

        /// <summary>
        /// 重启IIS
        /// </summary>
        public static void ResetIIS()
        {
            const string WebSiteName = "Default Web Site";
            ServerManager sm = new ServerManager();
            var site = sm.Sites[WebSiteName];
            try
            {
                if (site != null && site.State == ObjectState.Started)
                    site.Stop();
                if (site != null && site.State == ObjectState.Stopped)
                    site.Start();
            }
            catch (Exception)
            { }
            finally
            {
                sm.Dispose();
            }
        }

        /// <summary>
        /// 记录日志文件
        /// </summary>
        /// <param name="str"></param>
        public static void WriteLog(string str, string filename = "Log.txt")
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            StreamWriter sw = null;
            if (!File.Exists(filePath))
            {
                sw = File.CreateText(filePath);
            }
            else
            {
                sw = File.AppendText(filePath);
            }
            sw.Write(DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒 ") + str + Environment.NewLine);
            sw.Close();
        }

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public static bool CompressImage(string sFile, string dFile, int flag = 90, int size = 300, bool sfsc = true)
        {
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                firstFileInfo.CopyTo(dFile);
                return true;
            }
            Image iSource = Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                    FileInfo fi = new FileInfo(dFile);
                    if (fi.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        CompressImage(sFile, dFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFileStream">原文件内存流</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public static MemoryStream CompressImage(MemoryStream sFileStream, int flag = 80, int size = 300, bool sfsc = true)
        {
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            if (sfsc == true && sFileStream.Length < size * 1024)
            {
                return sFileStream;
            }
            Image iSource = Image.FromStream(sFileStream);
            //Image iSource = Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {

                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    //var path = @"D:\text.jpg";
                    //ob.Save(path, jpegICIinfo, ep);//dFile是压缩后的新路径
                    //FileStream dFileStream = new FileStream(path, FileMode.Open);
                    MemoryStream stream = new MemoryStream();
                    ob.Save(stream, jpegICIinfo, ep);
                    return stream;
                }
                else
                {
                    return sFileStream;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }

        /// <summary>
        /// 判断计算机链接状态
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:domain\\" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
    }
}

