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
	public class Error
	{
		[JsonProperty("code")]
		public int Code { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
