using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TH.DataBase
{
    [DataContract]
    public class Column
    {
        [DataMember]
       public string Label { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public bool IsPrimaryKey { get; set; }
        [DataMember]
        public byte Scale { get; set; }
        [DataMember]
        public bool IsNull { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public int Precision { get; set; }
    }
}
