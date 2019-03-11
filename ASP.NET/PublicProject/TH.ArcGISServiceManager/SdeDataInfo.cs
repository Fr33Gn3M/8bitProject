using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class SdeDataInfo
    {
        [DataMember]
        public bool IsSdeData { get; set; }
        [DataMember]
        public Dictionary<string, object> Data { get; set; }
        [DataMember]
        public object Geom { get; set; }
    }

    [DataContract]
    public class FeatureDataInfo
    {
        [DataMember]
        public string FeatureId { get; set; }
        [DataMember]
        public Dictionary<string, object> Attributes { get; set; }
        [DataMember]
        public CoordTransfer.Point Geom { get; set; }
    }
}
