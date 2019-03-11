using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports
{
	[Serializable()]
	[JsonObject()]
	public class NameTypePair
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
