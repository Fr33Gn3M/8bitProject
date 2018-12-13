using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ArcGIsLib.ApiImports.Geometry
{
	[Serializable()]
	[JsonObject("Geometry")]
	public class AgsGeometryBase
	{
		[JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore)]
		public AgsSpatialReference SpatialReference { get; set; }
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string GeoType { get; set; }
	}
}
