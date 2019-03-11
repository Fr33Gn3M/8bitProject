using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class FieldModel
    {
        [DataMember]
        public string FieldName { get; set; }//字段名称
        [DataMember]
        public string FieldType { get; set; }//字段类型
        [DataMember]
        public string FieldAlias { get; set; }//字段别名
        [DataMember]
        public Nullable<int> FieldLength { get; set; }//字段长度
        [DataMember]
        public Nullable<bool> IsPrimaryKey { get; set; }//是否为主键
        [DataMember]
        public Nullable<bool> IsNull { get; set; }//字段长度
        [DataMember]
        public Nullable<bool> Editable { get; set; }
        [DataMember]
        public string DefaultValue { get; set; }
    }
}
