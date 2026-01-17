using FC.Database.Enum;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.FilterModel
{
    /// <summary>
    /// 最小查询过滤器
    /// </summary>
    public class MiniQueryFilter : BaseQueryFilter
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// value值，如果是in和notIn，用,隔开
        /// </summary>
        public object Value { get; set; }//如果是in和notIn用,隔开

        /// <summary>
        /// sql简单操作符，大于等于小于 等等
        /// </summary>
        //Json字符串自动转枚举
        [JsonConverter(typeof(StringEnumConverter))]
        public SqlOperator Sign { get; set; }
    }
}
