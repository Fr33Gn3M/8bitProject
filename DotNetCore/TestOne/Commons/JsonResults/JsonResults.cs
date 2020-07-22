using Newtonsoft.Json;
using System;

namespace Commons
{
    public class JsonResults
    {
        public JsonResults()
        {
            Code = (int)ResultCode.SUCCESS;
            Msg = EnumHelper.GetDescriptionByEnum(ResultCode.SUCCESS);
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

        public JsonResults setCode(ResultCode code)
        {
            this.Code = (int)code;
            this.Msg = EnumHelper.GetDescriptionByEnum(code);
            return this;
        }
        public JsonResults setData<T>(T data)
        {
            this.Data = data;
            return this;
        }
        public JsonResults setMsg(String msg)
        {
            this.Msg = msg;
            return this;
        }
        public JsonResults setTotal(int total)
        {
            this.Total = total;
            return this;
        }
    }
}
