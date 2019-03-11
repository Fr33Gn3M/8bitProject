using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.ApiImports.Renderers
{
	[Serializable()]
	[JsonObject()]
	public class ClassBreakInfo
	{
		[JsonProperty("classMaxValue")]
		public int ClassMaxValue { get; set; }
		[JsonProperty("label")]
		public string Label { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("symbol")]
		[JsonConverter(typeof(SymbolConverter))]
		public ISymbol Symbol { get; set; }
	}
}
