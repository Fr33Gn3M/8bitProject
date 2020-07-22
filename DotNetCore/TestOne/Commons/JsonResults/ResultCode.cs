using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Commons
{
    public enum ResultCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("OK!")]
        SUCCESS=1000,
        /// <summary>
        /// 权限认证错误
        /// </summary>
        [Description("AUTHFAIL!")]
        AUTHFAIL=1001,
        /// <summary>
        /// 调用方入参或其他错误
        /// </summary>
        [Description("FAIL!")]
        FAIL=1002,
        /// <summary>
        /// 程序错误
        /// </summary>
        [Description("EXCEPTION!")]
        CUSTOMEXCEPTION=9999
    }
}
