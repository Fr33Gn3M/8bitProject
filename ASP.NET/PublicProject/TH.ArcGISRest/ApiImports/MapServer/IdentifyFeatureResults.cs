using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	[JsonObject()]
	public class IdentifyFeatureResults
	{
		[JsonProperty("results")]
		public IList<IdentifyFeature> Results { get; set; }
	}
}