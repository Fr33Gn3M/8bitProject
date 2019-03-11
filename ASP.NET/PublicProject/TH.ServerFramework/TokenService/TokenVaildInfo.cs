using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.TokenService
{
    [Serializable]
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class TokenVaildInfo
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public string UserLoginName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        public IPEndPoint ClientIpEndpoint { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public DateTime LastSignTime { get; set; }
        [DataMember]
        public Guid Token { get; set; }
        [DataMember]
        public DateTime UpdateTime { get; set; }
        [DataMember]
        public Nullable<bool> IsLasting { get; set; }
        [DataMember]
        public string AddressIP { get; set; }
        [DataMember]
        public Nullable<int> Port { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string CompanyEnterprise { get; set; }
        

    }
}
