using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.Symbols
{
	[Serializable()]
	[JsonObject()]
	public class PictureFillSymbol : PictureMarkerSymbol
	{
		[JsonProperty("xscale")]
		public double XScale { get; set; }
		[JsonProperty("yscale")]
		public double YScale { get; set; }
	}
}
