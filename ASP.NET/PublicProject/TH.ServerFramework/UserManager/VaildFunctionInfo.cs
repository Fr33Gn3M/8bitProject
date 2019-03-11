using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.UserManager
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class VaildFunctionInfo
    {
        [DataMember]
        public string ClientIpAddress { get; set; }
        [DataMember]
        public int ClientPost { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
        [DataMember]
        public bool IsServiceVaild { get; set; }
        [DataMember]
        public string ClassTypeName { get; set; }
        [DataMember]
        public string MethodName { get; set; }
    }
}
