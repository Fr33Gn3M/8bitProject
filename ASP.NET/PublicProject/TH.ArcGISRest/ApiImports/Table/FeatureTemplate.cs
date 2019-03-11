using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
     [Serializable]
    [JsonObject()]
   public class FeatureTemplate
    {
        [JsonProperty("name")]
        public String Name {get;set;}
        [JsonProperty("description")]
        public String Description {get;set;}
        [JsonProperty("drawingTool")]
        [JsonConverter(typeof(NullableStringEnumConverter))]
        public DrawingTool? DrawingTool {get;set;}
        [JsonProperty("prototype")]
        public EsriFeature Prototype {get;set;}
    }
}
