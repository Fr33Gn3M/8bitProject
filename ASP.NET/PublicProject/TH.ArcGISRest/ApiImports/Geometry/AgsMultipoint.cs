using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.Geometry
{
	[Serializable()]
	[JsonObject()]
	public class AgsMultipoint : AgsGeometryBase
	{
		[JsonProperty("points")]
		public double[][] Points { get; set; }
	}
}
