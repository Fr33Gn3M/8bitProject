using FC.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.FilterModel
{
    /// <summary>
    /// 标准查询过滤器
    /// </summary>
    public class QueryFilter : BaseQueryFilter
    {
        /// <summary>
        /// 过滤器，可嵌套
        /// </summary>
        public BaseQueryFilter[] Filters { get; set; }

        /// <summary>
        /// sql逻辑运算符,and 或 or
        /// </summary>
        public SqlLogicOperator SqlLogicOperator { get; set; }
    }
}
