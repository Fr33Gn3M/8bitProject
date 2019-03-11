using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.ApiImports.Renderers
{
	[Serializable()]
	[JsonObject()]
	public class DrawingInfo
	{
		[JsonProperty("renderer")]
		[JsonConverter(typeof(RendererConverter))]
		public RendererBase Renderer { get; set; }
		[JsonProperty("transparency")]
		public double Transparency { get; set; }
		[JsonProperty("labelingInfo")]
		public LabelClass[] LabelingInfo { get; set; }
	}
}
