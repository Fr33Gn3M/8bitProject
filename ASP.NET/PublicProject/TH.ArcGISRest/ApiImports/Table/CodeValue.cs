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
	[JsonObject("codeValue")]
	public class CodeValue
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("code")]
		public string Code { get; set; }
	}
}
