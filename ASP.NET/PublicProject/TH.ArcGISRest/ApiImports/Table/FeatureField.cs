using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Domains;

namespace TH.ArcGISRest.ApiImports.FeatureBase
{
	[Serializable()]
	[JsonObject("field")]
	public class FeatureField
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public EsriFieldType Type { get; set; }
		[JsonProperty("alias")]
		public string Alias { get; set; }
		[JsonProperty("editable")]
		public bool Editable { get; set; }
		[JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
		public int? Length { get; set; }
		[JsonProperty("domain")]
		[JsonConverter(typeof(DomainConverter))]
		public DomainBase Domain { get; set; }
	}
}
