using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	public class AgsFindParams
	{
		public string SearchText { get; set; }
		public bool Contains { get; set; }
		public string[] SearchFields { get; set; }
		public AgsSpatialReference Sr { get; set; }
		public IDictionary<int, string> LayerDefs { get; set; }
		public int[] Layers { get; set; }
		public bool ReturnGeometry { get; set; }
		public double? MaxAllowableOffset { get; set; }
	}
}
