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
	public class AgsQueryMapLayerParams
	{
		public AgsQueryMapLayerParams()
		{
			this.ReturnGeometry = true;
			this.GeometryType = EsriGeometryType.esriGeometryEnvelope;
			this.SpatialRel = EsriSpatialRelationship.esriSpatialRelWithin;
		}
		public string Text { get; set; }
		public AgsGeometryBase Geometry { get; set; }
		public EsriGeometryType GeometryType { get; set; }
		public AgsSpatialReference InSR { get; set; }
		public EsriSpatialRelationship SpatialRel { get; set; }
		public string RelationParam { get; set; }
		public string Where { get; set; }
		public int[] ObjectIds { get; set; }
		public object Time { get; set; }
		public FieldsFilter OutFields { get; set; }
		public bool ReturnGeometry { get; set; }
		public double? MaxAllowableOffset { get; set; }
		public AgsSpatialReference OutSR { get; set; }
		public bool ReturnIdsOnly { get; set; }
	}
}
