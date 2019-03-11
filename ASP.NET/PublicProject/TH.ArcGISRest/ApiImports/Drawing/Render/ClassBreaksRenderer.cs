using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.Renderers
{
	[Serializable()]
	[JsonObject()]
	public class ClassBreaksRenderer : RendererBase
	{
		[JsonProperty("field")]
		public string Field { get; set; }
		[JsonProperty("minValue")]
		public double MinValue { get; set; }
		[JsonProperty("classBreakInfos")]
		public ClassBreakInfo[] ClassBreakInfos { get; set; }
	}
}
