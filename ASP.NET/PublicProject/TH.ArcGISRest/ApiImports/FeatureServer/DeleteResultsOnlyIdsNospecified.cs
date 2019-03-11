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
	public class DeleteResultsOnlyIdsNospecified : DeleteResultsBase
	{
		public DeleteResultsOnlyIdsNospecified()
		{
			Success = true;
		}
		[JsonProperty("success")]
		public bool Success { get; set; }
	}
}
