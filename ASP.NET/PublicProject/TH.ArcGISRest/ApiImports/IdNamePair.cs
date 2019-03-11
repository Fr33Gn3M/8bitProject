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
	public class IdNamePair
	{
		[JsonProperty("id")]
		public int ID { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
