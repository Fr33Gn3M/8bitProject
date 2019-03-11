using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class ArcGISManagerInfo
    {
        [DataMember]
        public string HostName { get; set; }
        [DataMember]
        public string MapServerUserName { get; set; }
        [DataMember]
        public string MapserverPwd { get; set; }
    }
}
