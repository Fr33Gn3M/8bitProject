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
	public class AgsIdentifyParams
	{
		public AgsIdentifyParams()
		{
			this.ReturnGeometry = true;
			this.GeometryType = EsriGeometryType.esriGeometryEnvelope;
		}
		public AgsGeometryBase Geometry { get; set; }
		public EsriGeometryType GeometryType { get; set; }
		public AgsSpatialReference SR { get; set; }
		public IDictionary<int, string> LayerDefs { get; set; }
		public double? Tolerance { get; set; }
		public LayerIdentifyOperation Layers { get; set; }
		public AgsEnvelope MapExtent { get; set; }
		public ImageDisplay ImageDisplay { get; set; }
		public object Time { get; set; }
		public bool ReturnGeometry { get; set; }
		public double? MaxAllowableOffset { get; set; }
	}

}
