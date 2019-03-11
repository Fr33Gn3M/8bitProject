using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class FeatureSymbolInfo
    {
        [DataMember]
        public string featureLayerName { get; set; }
        [DataMember]
        public SymbolBase symbols { get; set; }
        [DataMember]
        public Dictionary<string,object> renderer { get; set; }
    }
}
