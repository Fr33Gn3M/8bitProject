using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.Symbols
{
	[Serializable()]
	[JsonObject()]
	public class SimpleMarkerSymbol : WithTypeSymbolBase
	{
		[JsonProperty("style")]
		[JsonConverter(typeof(StringEnumConverter))]
		public SimpleMarkerSymbolStyle Style { get; set; }
		[JsonProperty("color")]
		[JsonConverter(typeof(ColorConverter))]
		public Color Color { get; set; }
		[JsonProperty("size")]
		public double Size { get; set; }
		[JsonProperty("angle")]
		public double Angle { get; set; }
		[JsonProperty("xoffset")]
		public double XOffset { get; set; }
		[JsonProperty("yoffset")]
		public double YOffset { get; set; }
		[JsonProperty("outline")]
		public SimpleLineSymbol Outline { get; set; }
	}
}
