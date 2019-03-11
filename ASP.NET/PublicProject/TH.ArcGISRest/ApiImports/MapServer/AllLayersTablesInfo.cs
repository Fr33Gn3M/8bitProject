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
	[JsonObject()]
	public class AllLayersTablesInfo
	{
		[JsonProperty("layers")]
		public IList<MapServiceLayerTableInfo> Layers { get; set; }
		[JsonProperty("tables")]
		public IList<MapServiceLayerTableInfo> Tables { get; set; }
	}
}
