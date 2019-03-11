using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	[JsonObject("TileInfo")]
	public class TileInfo
	{
		[JsonProperty("rows", Order = 0)]
		public int Rows { get; set; }
		[JsonProperty("cols", Order = 1)]
		public int Cols { get; set; }
		[JsonProperty("dpi", Order = 2)]
		public int DPI { get; set; }
		[JsonProperty("format", Order = 3)]
		public string Format { get; set; }
		[JsonProperty("compressionQuality", Order = 4)]
		public string CompressionQuality { get; set; }
		[JsonProperty("spatialReference", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
		public AgsSpatialReference SpatialReference { get; set; }
		[JsonProperty("origin", Order = 6)]
		public AgsPoint Origin { get; set; }
		[JsonProperty("lods", Order = 7)]
		public Lod[] LODs { get; set; }
	}
}
