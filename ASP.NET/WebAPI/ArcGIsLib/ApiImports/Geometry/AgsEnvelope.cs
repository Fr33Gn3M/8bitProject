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
	[JsonObject("Envelope")]
	public partial class AgsEnvelope : AgsGeometryBase
	{
		[JsonProperty("xmax")]
		public double XMax { get; set; }
		[JsonProperty("xmin")]
		public double XMin { get; set; }
		[JsonProperty("ymax")]
		public double YMax { get; set; }
		[JsonProperty("ymin")]
		public double YMin { get; set; }
	}
}
