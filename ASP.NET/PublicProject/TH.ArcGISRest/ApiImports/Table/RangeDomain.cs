using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.Domains
{
	[Serializable()]
	[JsonObject("rangeDomain")]
	public class RangeDomain : DomainBase
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("range")]
		public double[] Range { get; set; }
	}
}
