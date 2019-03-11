using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TH.ArcGISRest.ApiImports.Renderers
{
	[Serializable()]
	[JsonObject()]
	public class RendererBase
	{
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public RendererType Type { get; set; }
	}
}
