using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.UserManager
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class RoleInfo
    {
        [DataMember]
        public Guid RoleID { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public string RoleTitle { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
