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
	public class TimeInfo
	{
		[JsonProperty("endTimeField")]
		public string EndTimeField { get; set; }
		[JsonProperty("exportOptions")]
		public ExportOptions ExportOptions { get; set; }
		[JsonProperty("startTimeField")]
		public string StartTimeField { get; set; }
		[JsonProperty("timeExtent")]
		public object[] TimeExtent { get; set; }
		//[long,long] or [null,long] or [long,null]
		[JsonProperty("timeInterval")]
		public int TimeInterval { get; set; }
		[JsonProperty("timeReference")]
		public TimeReference TimeReference { get; set; }
		[JsonProperty("trackIdField")]
		public string TrackIdField { get; set; }
	}
}
