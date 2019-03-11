using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{

    [DataContract]
    public class SdeConfigInfo
    {
        [DataMember]
        public string SDEServer { get; set; }
        [DataMember]
        public string Instance { get; set; }
        [DataMember]
        public string DataBase { get; set; }
        [DataMember]
        public string SDEUser { get; set; }
        [DataMember]
        public string SDEPassWord { get; set; }
        [DataMember]
        public string SDEVersion { get; set; }

    }
}
