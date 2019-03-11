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
	public class Font
	{
		[JsonProperty("family")]
		public string Family { get; set; }
		[JsonProperty("size")]
		public double Size { get; set; }
		[JsonProperty("style")]
		[JsonConverter(typeof(StringEnumConverter))]
		public FontStyle Style { get; set; }
		[JsonProperty("weight")]
		[JsonConverter(typeof(StringEnumConverter))]
		public FontWeight Weight { get; set; }
		[JsonProperty("decoration")]
		[JsonConverter(typeof(FontDecorationConverter))]
		public FontDecoration Decoration { get; set; }
	}
}
