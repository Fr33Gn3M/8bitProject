using System;
using System.Collections.Generic;
using System.Data;

using System.Text;

namespace TH.DataBase
{
    public class SqlTableInfo
    {
        public string TableName { get; set; }
        public string KeyFieldName { get; set; }
        public Dictionary<string, object> Fields { get; set; }
        public Type BaseType { get; set; }

    }
}
