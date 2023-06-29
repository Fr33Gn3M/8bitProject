namespace FC.Core.Models
{
    public class ApiResult
    {
        #region 构造函数

        public ApiResult()
        {
            Code = ApiResultCode.SUCCESS;
            Msg = Enum.GetName(typeof(ApiResultCode), ApiResultCode.SUCCESS);
        }

        public ApiResult(ApiResultCode code)
        {
            Code = code;
            Msg = Enum.GetName(typeof(ApiResultCode), code);
        }

        public ApiResult(ApiResultCode code, string msg)
        {
            Code = code;
            Msg = msg;
        }

        public ApiResult(ApiResultCode code, string msg, object data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }

        public ApiResult(object data)
        {
            Code = ApiResultCode.SUCCESS;
            Msg = Enum.GetName(typeof(ApiResultCode), ApiResultCode.SUCCESS);
            Data = data;
        }

        public ApiResult(object data, int total)
        {
            Code = ApiResultCode.SUCCESS;
            Msg = Enum.GetName(typeof(ApiResultCode), ApiResultCode.SUCCESS);
            Data = data;
            Total = total;
        }

        #endregion

        #region 静态调用方法

        public static ApiResult Ok() => new();
        public static ApiResult Ok(object data) => new(data);
        public static ApiResult Ok(object data, int total) => new(data, total);
        public static ApiResult Error(string msg) => new(ApiResultCode.FAIL, msg);
        public static ApiResult Error(ApiResultCode code) => new(code);
        public static ApiResult Error(ApiResultCode code, string msg) => new(code, msg);
        public static ApiResult Error(ApiException ex) => new(ex.Code, ex.Message);
        public static ApiResult Error(Exception ex) => new(ApiResultCode.FAIL, ex.Message);

        #endregion



        public ApiResultCode Code { get; set; }

        public object? Data { get; set; }

        public string? Msg { get; set; }

        public int Total { get; set; }
    }

    public enum ApiResultCode
    {
        //1000成功
        SUCCESS = 1000,
        //1001权限校验失败
        AUTH_FAIL = 1001,
        //1002入参错误
        PARAM_ERROR = 1002,
        //1003接口版本错误
        VERSION_ERROR = 1003,
        //业务逻辑异常
        FAIL = 9999
    }
}
