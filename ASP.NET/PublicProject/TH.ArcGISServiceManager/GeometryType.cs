using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public enum GeometryType
    {
        [EnumMember]
        Point,
        [EnumMember]
        PolyLine,
        [EnumMember]
        Polygon
    }
}
