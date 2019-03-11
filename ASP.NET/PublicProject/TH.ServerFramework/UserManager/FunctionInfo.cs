using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.UserManager
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public enum FunctionType
    {
        [EnumMember]
        None,
        [EnumMember]
        Menu,
        [EnumMember]
        MenuGroup,
        [EnumMember]
        Module,
        [EnumMember]
        Command
    }

    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class FunctionInfo
    {
        [DataMember]
        public Guid FunctionID { get; set; }
        [DataMember]
        public string FunctionName { get; set; }
        [DataMember]
        public string FunctionTitle { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public FunctionType FunctionType { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
        [DataMember]
        public FunctionInfo[] Functions { get; set; }
        [DataMember]
        public Guid? ParentID { get; set; }
        [DataMember]
        public string ServerInterfaceName { get; set; }
        [DataMember]
        public string ClientInterfaceName { get; set; }
        //[DataMember]
        //public string ImagePath { get; set; }


    }
}
