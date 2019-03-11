using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.DataBase
{
     public class QueryFilterResult
    {
        public string TableName { get; set; }
        public int TotalCount { get; set; }
        public object[] Result { get; set; }
    }
     public class QueryFilterResultDic
     {
         public int TotalCount { get; set; }
         public string TableName { get; set; }
         public Dictionary<string, object>[] Result { get; set; }
     }

}
