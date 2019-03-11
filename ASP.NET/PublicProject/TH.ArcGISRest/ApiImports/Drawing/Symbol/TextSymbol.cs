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
	public class TextSymbol : WithTypeSymbolBase
	{
		[JsonProperty("color")]
		[JsonConverter(typeof(ColorConverter))]
		public Color Color { get; set; }
		[JsonProperty("backgroundColor")]
		[JsonConverter(typeof(ColorConverter))]
		public Color BackgroundColor { get; set; }
		[JsonProperty("borderLineColor")]
		[JsonConverter(typeof(ColorConverter))]
		public Color BorderLineColor { get; set; }
		[JsonProperty("verticalAlignment")]
		[JsonConverter(typeof(StringEnumConverter))]
		public VerticalAlignment VerticalAlignment { get; set; }
		[JsonProperty("horizontalAlignment")]
		[JsonConverter(typeof(StringEnumConverter))]
		public HorizontalAlignment HorizontalAlignment { get; set; }
		[JsonProperty("rightToLeft")]
		public bool RightToLeft { get; set; }
		[JsonProperty("angle")]
		public double Angle { get; set; }
		[JsonProperty("xoffset")]
		public double XOffset { get; set; }
		[JsonProperty("yoffset")]
		public double YOffset { get; set; }
		[JsonProperty("kerning")]
		public bool Kerning { get; set; }
		[JsonProperty("font")]
		public Font Font { get; set; }
	}
}
