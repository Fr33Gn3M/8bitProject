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
	public class UniqueValueRenderer : RendererBase
	{
		[JsonProperty("field1")]
		public string Field1 { get; set; }
		[JsonProperty("field2")]
		public string Field2 { get; set; }
		[JsonProperty("field3")]
		public string Field3 { get; set; }
		[JsonProperty("fieldDelimiter")]
		public string FieldDelimiter { get; set; }
		[JsonProperty("defaultSymbol")]
		[JsonConverter(typeof(SymbolConverter))]
		public ISymbol DefaultSymbol { get; set; }
		[JsonProperty("defaultLabel")]
		public string DefaultLabel { get; set; }
		[JsonProperty("uniqueValueInfos")]
		public UniqueValueInfo[] UniqueValueInfos { get; set; }
	}
}
