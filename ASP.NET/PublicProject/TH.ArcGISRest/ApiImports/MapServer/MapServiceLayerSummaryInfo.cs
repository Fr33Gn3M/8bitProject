using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	[JsonObject("Layer")]
	public class MapServiceLayerSummaryInfo
	{
		[JsonProperty("id", Order = 0)]
		public int ID { get; set; }
		[JsonProperty("name", Order = 1)]
		public string Name { get; set; }
		[JsonProperty("parentLayerId", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
		public int? ParentLayerId { get; set; }
		[JsonProperty("defaultVisibility", Order = 3)]
		public bool DefaultVisibility { get; set; }
		[JsonProperty("subLayerIds", Order = 4)]
		public int[] SubLayerIds { get; set; }
		[JsonIgnore()]
		public int ParentId { get; set; }
	}
}
