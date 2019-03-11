using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.FeatureBase
{
	[Serializable()]
	[JsonObject("feature")]
	public class EsriFeature
	{
		[JsonProperty("attributes")]
		public IDictionary<string, object> Attributes { get; set; }
		[JsonProperty("geometry", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(GeomertyConverter))]
		public AgsGeometryBase Geometry { get; set; }
	}
}
