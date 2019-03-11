using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.ApiImports.FeatureService;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Renderers;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
    [Serializable]
   public class FeatureServiceLayerInfo
    {
        [JsonProperty("id")]
        public int Id {get;set;}
        [JsonProperty("name")]
        public String Name {get;set;}
        [JsonProperty("type")]
        [JsonConverter(typeof(FeatureLayerTypeConverter))]
        public FeatureLayerType LayerType { get; set; }
        [JsonProperty("description")]
        public String Description {get;set;}
        [JsonProperty("copyrightText")]
        public String CopyrightText {get;set;}
        [JsonProperty("relationships")]
        public FeatureRelationshipPair[] Relationships {get;set;}
        [JsonProperty("geometryType")]
        [JsonConverter(typeof(NullableStringEnumConverter))]
        public EsriGeometryType? GeometryType {get;set;}
        [JsonProperty("minScale")]
        public Double MinScale {get;set;}
        [JsonProperty("maxScale")]
        public Double MaxScale {get;set;}
        [JsonProperty("extent")]
        public AgsEnvelope Extent {get;set;}
        [JsonProperty("drawingInfo")]
        public DrawingInfo DrawingInfo {get;set;}
        [JsonProperty("timeInfo", NullValueHandling=NullValueHandling.Ignore)]
        public TimeInfo TimeInfo {get;set;}
        [JsonProperty("hasAttachments")]
        public bool HasAttachments {get;set;}
        [JsonProperty("htmlPopupType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public HtmlPopupType HtmlPopupType {get;set;}
        [JsonProperty("objectIdField")]
        public String ObjectIdField {get;set;}
        [JsonProperty("globalIdField")]
        public String GlobalIdField {get;set;}
        [JsonProperty("displayField")]
        public String DisplayField {get;set;}
        [JsonProperty("typeIdField")]
        public String TypeIdField {get;set;}
        [JsonProperty("fields")]
        public FeatureField[] Fields {get;set;}
        [JsonProperty("types")]
        public FeatureTypeInfo[] Types {get;set;}
        [JsonProperty("templates")]
        public FeatureTemplate[] Templates {get;set;}
        [JsonProperty("capabilities")]
        public String Capabilities {get;set;}
    }
}
