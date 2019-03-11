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

namespace TH.ArcGISRest.ApiImports.MapServices
{
     [Serializable]
    [JsonObject()]
   public class MapServiceLayerTableInfo
    {
         [JsonProperty("id")]
        public int Id {get;set;} 
        [JsonProperty("name")]
        public String Name {get;set;} 
        [JsonProperty("type")]
        [JsonConverter(typeof(MapLayerTypeConverter))]
        public MapLayerType LayerType {get;set;} 
        [JsonProperty("description")]
        public String Description {get;set;} 
        [JsonProperty("definitionExpression")]
        public String DefinitionExpression {get;set;} 
        [JsonProperty("geometryType", NullValueHandling=NullValueHandling.Ignore, Required=Required.AllowNull)]
        [JsonConverter(typeof(NullableStringEnumConverter))]
        public  EsriGeometryType? GeometryType {get;set;}
        [JsonProperty("copyrightText")]
        public String CopyrightText {get;set;} 
        [JsonProperty("parentLayer")]
        public IdNamePair ParentLayer {get;set;} 
        [JsonProperty("subLayers")]
        public IdNamePair[] SubLayers {get;set;} 
        [JsonProperty("minScale")]
        public Double MinScale {get;set;} 
        [JsonProperty("maxScale")]
        public Double MaxScale {get;set;} 
        [JsonProperty("extent")]
        public AgsEnvelope Extent {get;set;} 
        [JsonProperty("timeInfo", NullValueHandling=NullValueHandling.Ignore)]
        public TimeInfo TimeInfo {get;set;} 
        [JsonProperty("drawingInfo", NullValueHandling=NullValueHandling.Ignore)]
        public DrawingInfo DrawingInfo {get;set;} 
        [JsonProperty("hasAttachments")]
        public Boolean HasAttachments {get;set;} 
        [JsonProperty("htmlPopupType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public HtmlPopupType HtmlPopupType {get;set;} 
        [JsonProperty("displayField")]
        public String DisplayField {get;set;} 
        [JsonProperty("typeIdField")]
        public String TypeIdField {get;set;} 
        [JsonProperty("fields")]
        public FeatureField[] Fields {get;set;} 
        [JsonProperty("types")]
        public FeatureTypeInfo[] Types {get;set;} 
        [JsonProperty("relationships")]
        public FeatureRelationshipPair[] Relationships {get;set;} 
    
    }
}
