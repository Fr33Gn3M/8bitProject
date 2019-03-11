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
	public class DeleteResultsOnlyIdsSpecified : DeleteResultsBase
	{
		public DeleteResultsOnlyIdsSpecified()
		{
			DeleteResults = new List<EditResult>();
		}
		[JsonProperty("deleteResults")]
		public IList<EditResult> DeleteResults { get; set; }
	}
}
