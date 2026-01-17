using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LX.WebCommons.Models
{
    public class JsonResults
    {
        public JsonResults()
        {
            Status = 1000;
        }
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
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

        public static JsonResults Success(object data = null)
        {
            return new JsonResults() { Data = data };
        }

        public static JsonResults Success(object data, int total)
        {
            return new JsonResults() { Data = data, Total = total };
        }

        public static JsonResults Error(int status, string msg)
        {
            return new JsonResults() {Status = status, Msg = msg };
        }
    }
}
