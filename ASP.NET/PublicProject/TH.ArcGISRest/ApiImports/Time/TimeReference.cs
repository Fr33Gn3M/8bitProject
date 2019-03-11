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
	public class TimeReference
	{
		[JsonProperty("respectsDaylightSaving")]
		public bool RespectsDaylightSaving { get; set; }
		[JsonProperty("timeZone")]
		public string TimeZone { get; set; }
	}
}
