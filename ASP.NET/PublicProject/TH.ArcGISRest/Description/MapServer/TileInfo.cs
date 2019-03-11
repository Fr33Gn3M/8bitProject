using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using TH.ArcGISRest.Description.Geometry;

namespace TH.ArcGISRest.Description.MapServer
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class TileInfo
	{
		[DataMember()]
		public int Rows { get; set; }
		[DataMember()]
		public int Columns { get; set; }
		[DataMember()]
		public int DPI { get; set; }
		[DataMember()]
		public string Format { get; set; }
		[DataMember()]
		public double? CompressionQuality { get; set; }
		[DataMember()]
		public SpatialReference SpatialReference { get; set; }
		[DataMember()]
		public Point Orgin { get; set; }
		[DataMember()]
		public LOD[] LODs { get; set; }
	}
}
