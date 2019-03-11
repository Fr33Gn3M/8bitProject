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
	[JsonObject("addResults")]
	public class AddResults
	{
		[JsonProperty("addResults")]
		public IList<EditResult> addResults { get; set; }
	}
}
