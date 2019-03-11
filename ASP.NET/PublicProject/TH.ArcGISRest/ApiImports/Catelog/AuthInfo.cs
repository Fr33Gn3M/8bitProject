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
	public class AuthInfo
	{
		[JsonProperty("isTokenBasedSecurity")]
		public bool TokenBasedSecurity { get; set; }
		[JsonProperty("tokenServiceUrl", NullValueHandling = NullValueHandling.Ignore)]
		public string TokenServiceUrl { get; set; }
	}
}
