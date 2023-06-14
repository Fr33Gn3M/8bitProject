using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FC.Core.Models
{
    public class ApiException : Exception
    {
        private string? ExMessage = string.Empty;
        public override string Message => ExMessage;

        public ApiResultCode Code { get; set; }

        /// <summary>
        /// 构造函数1：ApiResultCode枚举错误
        /// </summary>
        /// <param name="code">ApiResultCode枚举代码</param>
        /// <param name="IsHideMessageShow">是否隐藏message信息</param>
        public ApiException(ApiResultCode code, bool IsHideMessageShow = false)
        {
            Code = code;
            ExMessage = Enum.GetName(typeof(ApiResultCode), code);
            Data.Add("Code", Code);
            Data.Add("IsHideMessageShowStr", IsHideMessageShow);
        }

        /// <summary>
        /// 构造函数2：未知错误，message自定义
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="IsHideMessageShow">是否隐藏message信息</param>
        public ApiException(string message, bool IsHideMessageShow = false)
        {
            Code = ApiResultCode.FAIL;
            ExMessage = message;
            Data.Add("Code", Code);
            Data.Add("IsHideMessageShowStr", IsHideMessageShow);
        }

        /// <summary>
        /// 构造函数3：ApiResultCode枚举错误
        /// </summary>
        /// <param name="code">ApiResultCode枚举代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="IsHideMessageShow">是否隐藏message信息</param>
        public ApiException(ApiResultCode code, string message, bool IsHideMessageShow = false)
        {
            Code = code;
            ExMessage = message;
            Data.Add("Code", Code);
            Data.Add("IsHideMessageShowStr", IsHideMessageShow);
        }
    }
}
