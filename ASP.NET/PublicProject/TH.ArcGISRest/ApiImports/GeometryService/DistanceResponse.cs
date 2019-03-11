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
	public class DistanceResponse
	{
		[JsonProperty("distance")]
		public double Distance { get; set; }
	}
}
