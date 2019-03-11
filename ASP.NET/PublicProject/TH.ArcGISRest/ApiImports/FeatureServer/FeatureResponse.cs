using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.FeatureBase;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
	[Serializable()]
	[JsonObject()]
	public class FeatureResponse
	{
		[JsonProperty("feature")]
		public EsriFeature Feature { get; set; }
	}
}
