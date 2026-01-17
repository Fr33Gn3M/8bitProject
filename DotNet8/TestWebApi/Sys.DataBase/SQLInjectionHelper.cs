using System;
using System.Text.RegularExpressions;

namespace Sys.DataBase
{

    /// <summary>
    ///SQLInjectionHelper 的摘要说明
    /// </summary>
    public class SQLInjectionHelper
    {
        public SQLInjectionHelper()
        {
        }
        /// <summary>
        /// 验证是否存在注入代码
        /// </summary>
        /// <param name="inputData">输入字符</param>
        /// <returns></returns>
        public static bool ValidData(String inputData)
        {
            inputData = inputData.Replace("FilterAndOrType", "FilterType");
            inputData = inputData.Replace("MoreThan", "FilterMThan");
            inputData = inputData.Replace("MoreEqualThan", "FilterMEThan");
            //验证inputData是否包含恶意集合
            if (string.IsNullOrEmpty(inputData) == false && Regex.IsMatch(inputData, GetRegexString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取正则表达式
        /// </summary>
        private static String GetRegexString()
        {
            // {"TableName":"VWTCC_YCCLXXB","IsPage":true,"PageSize":15,"PageIndex":1,"Filters":[{"FieldName":"CJSJ","Value":"2022-08-28T00:00:00","Sign":"MoreThan"},{"FieldName":"CJSJ","Value":"2022-09-04T14:40:49","Sign":"LessThan"}],"FilterType":0,"OrderFieldNames":["CJSJ"],"GroupByFieldNames":[],"OrderByType":0,"ReturnFieldNames":[]}
            //构造SQL的注入关键字符
            String[] strBadChar = {"and","exec","insert","select","delete","update","join","drop","master","truncate","declare","net user","xp_cmdshell","/add",
                              "exec master.dbo.xp_cmdshell","net localgroup administrators"};
            //构造正则表达式
            String str_Regex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++)
            {
                str_Regex += strBadChar[i] + "|";
            }
            str_Regex += strBadChar[strBadChar.Length - 1] + ").*";

            return str_Regex;
        }
    }
}
