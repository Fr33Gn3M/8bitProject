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
	[JsonObject("LOD")]
	public class Lod
	{
		[JsonProperty("level")]
		public int Level { get; set; }
		[JsonProperty("resolution")]
		public double Resolution { get; set; }
		[JsonProperty("scale")]
		public double Scale { get; set; }
	}
}
