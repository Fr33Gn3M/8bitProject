using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.UserManager
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class UserInfo
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreateDate { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastSignTime { get; set; }
        [DataMember]
        public int SignState { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string UserLoginName { get; set; }
        [DataMember]
        public string[] RoleIds { get; set; }
    }
}
