using FC.Database.Enum;
using FC.Database.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.FilterModel
{
    /// <summary>
    /// 页面查询过滤器
    /// </summary>
    public class PageQueryFilter
    {
        public QueryFilter Filter { get; set; }

        public string[] Fields { get; set; }

        public OrderInfo[] Orders { get; set; }

        public PageInfo Page { get; set; }
    }
}
