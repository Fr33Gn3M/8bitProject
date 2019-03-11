using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.FeatureBase
{
    [Serializable]
    [JsonObject()]
    public class FeatureSet
    {
        [JsonProperty("objectIdFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public String ObjectIdFieldName { get; set; }
        [JsonProperty("globalIdFieldName", NullValueHandling = NullValueHandling.Ignore)]
        public String GlobalIdFieldName { get; set; }
        [JsonProperty("geometryType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(NullableStringEnumConverter))]
        public EsriGeometryType? GeometryType { get; set; }
        [JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
        public AgsSpatialReference SpatialReference { get; set; }
        [JsonProperty("fields")]
        public IList<FeatureField> Fields { get; set; }
        [JsonProperty("features")]
        public IList<EsriFeature> Features { get; set; }
        [JsonProperty("fieldAliases", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> FieldAliases { get; set; }
    }
}
