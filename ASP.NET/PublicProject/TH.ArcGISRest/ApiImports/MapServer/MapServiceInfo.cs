using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	[JsonObject("MapServiceInfo")]
	public class MapServiceInfo
	{
		public MapServiceInfo()
		{
			MapName = string.Empty;
			CopyrightText = string.Empty;
			Description = string.Empty;
            Tables = new IdNamePair[]{
				
			};
		}
		[JsonProperty("serviceDescription", Order = 0)]
		public string ServiceDescription { get; set; }
		[JsonProperty("mapName", Order = 1)]
		public string MapName { get; set; }
		[JsonProperty("description", Order = 2)]
		public string Description { get; set; }
		[JsonProperty("layers", Order = 3)]
		public MapServiceLayerSummaryInfo[] Layers { get; set; }
		[JsonProperty("tables", Order = 4)]
		public IdNamePair[] Tables { get; set; }
		[JsonProperty("spatialReference", Order = 5)]
		public AgsSpatialReference SpatialReference { get; set; }
		[JsonProperty("singleFusedMapCache", Order = 6)]
		public bool SingleFusedMapCache { get; set; }
		[JsonProperty("tileInfo", Order = 7)]
		public TileInfo TileInfo { get; set; }
		[JsonProperty("initialExtent", Order = 8)]
		public AgsEnvelope InitialExtent { get; set; }
		[JsonProperty("fullExtent", Order = 9)]
		public AgsEnvelope FullExtent { get; set; }
		[JsonProperty("units", Order = 10)]
		public string Units { get; set; }
		[JsonProperty("supportedImageFormatTypes", Order = 11)]
		public string SupportedImageFormatTypes { get; set; }
		[JsonConverter(typeof(DocumentInfoConverter))]
		[JsonProperty("documentInfo", Order = 12)]
		public DocumentInfo DocumentInfo { get; set; }
		[JsonProperty("capabilities")]
		public string Capabilities { get; set; }
		[JsonProperty("copyrightText", Order = 13)]
		public string CopyrightText { get; set; }
	}
}
