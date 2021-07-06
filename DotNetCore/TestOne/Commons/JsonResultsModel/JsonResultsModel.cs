using Newtonsoft.Json;
using System;

namespace Commons
{
    public class JsonResultModel
    {
        public JsonResultModel()
        {

        }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 结果集
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        public JsonResultModel Ok()
        {
            Code = (int)ResultCode.SUCCESS;
            Msg = EnumHelper.GetDescriptionByEnum(ResultCode.SUCCESS);
            return this;
        }

        public JsonResultModel AuthFail()
        {
            Code = (int)ResultCode.AUTHFAIL;
            Msg = EnumHelper.GetDescriptionByEnum(ResultCode.AUTHFAIL);
            return this;
        }

        public JsonResultModel SetCode(ResultCode code)
        {
            this.Code = (int)code;
            this.Msg = EnumHelper.GetDescriptionByEnum(code);
            return this;
        }
        public JsonResultModel SetCode(int code)
        {
            this.Code = (int)code;
            return this;
        }
        public JsonResultModel SetData<T>(T data)
        {
            this.Data = data;
            return this;
        }
        public JsonResultModel SetMsg(String msg)
        {
            this.Msg = msg;
            return this;
        }
        public JsonResultModel SetTotal(int total)
        {
            this.Total = total;
            return this;
        }
    }
}
