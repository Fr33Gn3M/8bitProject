using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	public class LayerDisplay
	{
		public LayersDisplayOption DisplayOption { get; set; }
		public int[] LayerIds { get; set; }
	}
}
