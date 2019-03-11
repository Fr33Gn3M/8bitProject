using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.TokenService
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class SqlConnectInfo
    {
        [DataMember]
        public string ClientServerIp { get; set; }
        [DataMember]
        public string ClientServerUserId { get; set; }
        [DataMember]
        public string ClientServerPwd { get; set; }
        [DataMember]
        public string ClientServerDatabase { get; set; }
    }

    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class ClientConfigInfo
    {

        [DataMember]
        public SqlConnectInfo SqlConnects { get; set; }
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string PenaltyRate { get; set; }
        [DataMember]
        public string BeginWorkPenaltyRate { get; set; }
        [DataMember]
        public string FinishWorkPenaltyRate { get; set; }
        [DataMember]
        public string StatusDeadline { get; set; }
        [DataMember]
        public string FlatMapUrl { get; set; }
        [DataMember]
        public string ImageMapUrl { get; set; }
        [DataMember]
        public string MapInitExtent { get; set; }
        [DataMember]
        public string AuthenticationServiceUrl { get; set; }
        [DataMember]
        public string ImageNodeMapUrl { get; set; }

    }

}
