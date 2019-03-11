using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Web.Administration;

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
            chrStr =    chrStr.Remove(index, 1);
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

        public static void WriteLog(string str)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
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
    }
}
