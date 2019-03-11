using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureBase;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
	[Serializable()]
	public class AgsApplyEditsParams
	{
		public IList<EsriFeature> Adds { get; set; }
		public IList<EsriFeature> Updates { get; set; }
		public int[] Deletes { get; set; }
	}
}
