using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.Model
{
    public class PageQueryResult
    {
        public PageQueryResult(List<Dictionary<string, object>> dataList, int total) 
        {
            Total = total;
            DataList = dataList;
        }

        public int Total { get; set; }
        public List<Dictionary<string, object>> DataList { get; set; }
    }
}
