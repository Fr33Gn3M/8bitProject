using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace TH.ArcGISRest.ApiImports.Domains
{
	[Serializable()]
	[JsonObject()]
	public class DomainBase
	{
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public DomainType Type { get; set; }
	}
}
