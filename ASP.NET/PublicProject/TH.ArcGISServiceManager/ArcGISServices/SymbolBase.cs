using ESRI.ArcGIS.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class SymbolBase
    {
        [DataMember]
        public GeoStyle[] Styles { get; set; } 
    }

    [DataContract]
    public class GeoStyle 
    {
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
        
        public ISymbol pSymbol { get; set; }
    }
}
