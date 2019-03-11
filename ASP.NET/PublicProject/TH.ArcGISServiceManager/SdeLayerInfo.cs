using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class SdeLayerInfo
    {
        [DataMember]
        public string SdeLayerName { get; set; }
        [DataMember]
        public string SdeTitleName { get; set; }
        [DataMember]
        public string SpatialReference { get; set; }
        [DataMember]
        public SdeConfigInfo SdeConfig { get; set; }
        [DataMember]
        public FieldModel[] FieldModels { get; set; }
        [DataMember]
        public GeometryType GeoType { get; set; }
    }
}
