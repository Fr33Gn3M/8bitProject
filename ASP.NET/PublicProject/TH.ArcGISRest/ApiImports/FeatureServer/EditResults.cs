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
	public class EditResults
	{
		public EditResults()
		{
			AddResults = new List<EditResult>();
			UpdateResults = new List<EditResult>();
			DeleteResults = new List<EditResult>();
		}
		[JsonProperty("addResults")]
		public IList<EditResult> AddResults { get; set; }
		[JsonProperty("updateResults")]
		public IList<EditResult> UpdateResults { get; set; }
		[JsonProperty("deleteResults")]
		public IList<EditResult> DeleteResults { get; set; }
	}
}
