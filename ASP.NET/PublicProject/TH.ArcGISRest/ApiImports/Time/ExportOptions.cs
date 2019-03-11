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
	public class ExportOptions
	{
		[JsonProperty("timeDataCumulative")]
		public bool TimeDataCumulative { get; set; }
		[JsonProperty("timeOffset")]
		public int? TimeOffset { get; set; }
		[JsonProperty("timeOffsetUnits")]
		public string TimeOffsetUnits { get; set; }
		[JsonProperty("useTime")]
		public bool UseTime { get; set; }
	}
}
