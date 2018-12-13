using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ArcGIsLib.ApiImports.Geometry
{
	[Serializable()]
	[JsonObject()]
	public class AgsPolyline : AgsGeometryBase
	{
		[JsonProperty("paths")]
		public double[][][] Paths { get; set; }
	}
}
