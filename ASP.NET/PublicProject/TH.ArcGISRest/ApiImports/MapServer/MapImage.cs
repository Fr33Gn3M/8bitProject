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
	[JsonObject("MapImage")]
	public class MapImage
	{
		[JsonProperty("extent")]
		public AgsEnvelope Extent { get; set; }
		[JsonProperty("height")]
		public int Height { get; set; }
		[JsonProperty("href")]
		public string Href { get; set; }
		[JsonProperty("scale")]
		public double Scale { get; set; }
		[JsonProperty("width")]
		public int Width { get; set; }
	}
}
