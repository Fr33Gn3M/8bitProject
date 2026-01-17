using System.Text;
using Microsoft.International.Converters.PinYinConverter;

namespace LX.Commons
{
    public class PinyinHelper
    {
        /// <summary>
        /// 提取中文拼音首字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToInitialsCN(string text)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    ChineseChar chineseChar = new ChineseChar(c);
                    string pinyin = chineseChar.Pinyins[0];
                    if (!string.IsNullOrEmpty(pinyin))
                    {
                        result.Append(pinyin[0]);
                    }
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }
}
