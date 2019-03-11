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
	public class SimpleFillSymbol : WithTypeSymbolBase
	{
		//<JsonConverter(GetType(SimpleFillSymbolStyleConverter))>
		[JsonProperty("style")]
		[JsonConverter(typeof(StringEnumConverter))]
		public SimpleFillSymbolStyle Style { get; set; }
		[JsonProperty("color")]
		[JsonConverter(typeof(ColorConverter))]
		public Color Color { get; set; }
		[JsonProperty("outline")]
		public SimpleLineSymbol Outline { get; set; }
	}
}
