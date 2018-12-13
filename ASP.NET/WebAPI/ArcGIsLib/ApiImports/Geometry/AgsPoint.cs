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
	[JsonObject("point")]
	public class AgsPoint : AgsGeometryBase
	{
		[JsonProperty("x")]
		public double X { get; set; }
		[JsonProperty("y")]
		public double Y { get; set; }
	}
}
