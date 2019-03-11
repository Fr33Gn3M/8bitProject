using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.Geometry
{
   [Serializable]
  public  class AgsGeometryCollection
    {
       [JsonProperty("geometryType", NullValueHandling=NullValueHandling.Ignore)]
       [JsonConverter(typeof(StringEnumConverter))]
        public EsriGeometryType? GeometryType {get;set;} 
       [JsonProperty("geometries")]
       [JsonConverter(typeof(GeometryCollectionConverter))]
       public IList<AgsGeometryBase> Geometries { get; set; }  
    }
}
