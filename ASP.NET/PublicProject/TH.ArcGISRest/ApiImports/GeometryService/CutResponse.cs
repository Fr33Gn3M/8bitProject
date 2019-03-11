using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.ApiImports.GeometryService
{
	[Serializable()]
	[JsonObject()]
	public class CutResponse : AgsGeometryCollection
	{
		[JsonProperty("cutIndexes")]
		public int[] CutIndexes { get; set; }
	}
}
