using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.FeatureBase
{
	[Serializable()]
	[JsonObject()]
	public class FeatureIdSet
	{
		[JsonProperty("objectIdFieldName")]
		public string ObjectIdFieldName { get; set; }
		[JsonProperty("objectIds")]
		public IList<int> ObjectIds { get; set; }
	}
}
