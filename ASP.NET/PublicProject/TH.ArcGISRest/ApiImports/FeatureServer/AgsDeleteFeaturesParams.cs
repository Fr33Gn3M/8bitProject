using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
	[Serializable()]
	public class AgsDeleteFeaturesParams
	{
		public int[] ObjectIds { get; set; }
		public string Where { get; set; }
		public AgsGeometryBase Geometry { get; set; }
		public EsriGeometryType GeometryType { get; set; }
		public AgsSpatialReference InSr { get; set; }
		public EsriSpatialRelationship SpatialRel { get; set; }
	}
}
