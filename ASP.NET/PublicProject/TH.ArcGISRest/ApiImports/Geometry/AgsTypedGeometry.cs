using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.Geometry
{
	[Serializable()]
	[JsonObject()]
	public class AgsTypedGeometry
	{
		[JsonProperty("geometryType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public EsriGeometryType GeometryType { get; set; }
		[JsonProperty("geometry")]
        [JsonConverter(typeof(GeomertyConverter))]
        public AgsGeometryBase Geometry { get; set; }
	}
}
