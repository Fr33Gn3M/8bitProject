using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.GeometryService
{
	[Serializable()]
	[JsonObject()]
	public class RelationsResponse
	{
		[JsonProperty("relations")]
		public GeometryRelation[] Relations { get; set; }
	}
}
