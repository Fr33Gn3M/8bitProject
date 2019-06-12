using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
	[Serializable()]
	[JsonObject()]
	public class EditResult
	{
		[JsonProperty("objectId")]
		public int ObjectId { get; set; }
		[JsonProperty("globalId")]
		public int? GlobalId { get; set; }
		[JsonProperty("success")]
		public bool Success { get; set; }
		[JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
		public Error Error { get; set; }
	}
}