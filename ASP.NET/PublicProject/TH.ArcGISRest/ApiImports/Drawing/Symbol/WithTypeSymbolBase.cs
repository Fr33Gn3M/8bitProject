using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TH.ArcGISRest.ApiImports.Symbols
{
	[Serializable()]
	[JsonObject()]
	public class WithTypeSymbolBase : ISymbol
	{
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public SymbolType Type { get; set; }
	}
}
