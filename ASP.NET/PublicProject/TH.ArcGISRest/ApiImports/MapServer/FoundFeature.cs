using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	[JsonObject()]
	public class FoundFeature
	{
		[JsonProperty("layerId")]
		public int LayerId { get; set; }
		[JsonProperty("layerName")]
		public string LayerName { get; set; }
		[JsonProperty("displayFieldName")]
		public string DisplayFieldName { get; set; }
		[JsonProperty("foundFieldName")]
		public string FoundFieldName { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
		[JsonProperty("attributes")]
		public IDictionary<string, object> Attributes { get; set; }
		[JsonProperty("geometryType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public EsriGeometryType GeometryType { get; set; }
		[JsonProperty("geometry")]
		[JsonConverter(typeof(GeomertyConverter))]
		public AgsGeometryBase Geometry { get; set; }
	}
}
