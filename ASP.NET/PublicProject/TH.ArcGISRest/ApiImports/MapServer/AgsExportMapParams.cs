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
	public class AgsExportMapParams
	{
		public AgsEnvelope Bbox { get; set; }
		public ImageSize Size { get; set; }
		public int? Dpi { get; set; }
		public AgsSpatialReference ImageSr { get; set; }
		public AgsSpatialReference BboxSr { get; set; }
		public EsriImageFormat Format { get; set; }
		public LayerDisplay Layers { get; set; }
		public bool Transparent { get; set; }
		public TimeInfo Time { get; set; }
		public object LayerTimeOptions { get; set; }
		public bool OutputAsImage { get; set; }
	}
}
