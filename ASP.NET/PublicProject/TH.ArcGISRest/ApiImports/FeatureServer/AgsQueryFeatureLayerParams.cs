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
	public class AgsQueryFeatureLayerParams
	{
		public AgsQueryFeatureLayerParams()
		{
			OutFields = new FieldsFilter();
		}
		public int[] ObjectIds { get; set; }
		public string Where { get; set; }
		public AgsGeometryBase Geometry { get; set; }
		public EsriGeometryType GeometryType { get; set; }
		public AgsSpatialReference InSR { get; set; }
		public EsriSpatialRelationship SpatialRel { get; set; }
		public string RelationParam { get; set; }
		public TimeInfo Time { get; set; }
		public FieldsFilter OutFields { get; set; }
		public bool ReturnGeometry { get; set; }
		public AgsSpatialReference OutSR { get; set; }
		public bool ReturnIdsOnly { get; set; }
		public bool ReturnCountsOnly { get; set; }
	}
}
