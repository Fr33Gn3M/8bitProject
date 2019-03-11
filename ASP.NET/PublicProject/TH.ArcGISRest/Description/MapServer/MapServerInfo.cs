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
	public class MapServerInfo
	{
		[DataMember()]
		public string MapName { get; set; }
		[DataMember()]
		public TileInfo TileInfo { get; set; }
		[DataMember()]
		public string Description { get; set; }
		[DataMember()]
		public string ServiceDescription { get; set; }
		[DataMember()]
		public MapLayerIndex[] LayerIndexes { get; set; }
		[DataMember()]
		public IdNamePair[] TableIndex { get; set; }
		[DataMember()]
		public SpatialReference SpatialReference { get; set; }
		[DataMember()]
		public bool SingleFusedMapCache { get; set; }
		[DataMember()]
		public Envelope InitExtent { get; set; }
		[DataMember()]
		public Envelope FullExtent { get; set; }
		[DataMember()]
		public EsriUnit Units { get; set; }
		[DataMember()]
		public string[] SupportedImageFormatTypes { get; set; }
		[DataMember()]
		public MapServiceCapability Capabilities { get; set; }
		[DataMember()]
		public string CopyrightText { get; set; }
		[DataMember()]
		public IDictionary<string, string> DocumentInfo { get; set; }
	}
}
