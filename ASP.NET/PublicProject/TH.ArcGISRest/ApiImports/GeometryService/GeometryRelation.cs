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
	public class GeometryRelation
	{
		[JsonProperty("geometry1Index")]
		public int Geometry1Index { get; set; }
		[JsonProperty("geometry2Index")]
		public int Geometry2Index { get; set; }
	}
}
