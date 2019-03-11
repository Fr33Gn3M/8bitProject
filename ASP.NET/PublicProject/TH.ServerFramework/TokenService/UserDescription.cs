using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.TokenService
{
    [Serializable]
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class UserDescription
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserLoginName { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }

    }
}
