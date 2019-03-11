using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsTileInfo = TH.ArcGISRest.ApiImports.MapServices.TileInfo;
using DespTileInfo = TH.ArcGISRest.Description.MapServer.TileInfo;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.Description.MapServer;
using AgsLod = TH.ArcGISRest.ApiImports.MapServices.Lod;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class TileInfoMapper : ITypeConverter<DespTileInfo, AgsTileInfo>
	{

		public AgsTileInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespTileInfo sourceValue = context.SourceValue as DespTileInfo;
			var  targetValue = new AgsTileInfo();
			var _with1 = targetValue;
			_with1.Cols = sourceValue.Columns;
            _with1.CompressionQuality = sourceValue.CompressionQuality == null ? null : sourceValue.CompressionQuality.ToString();
			_with1.DPI = sourceValue.DPI;
			_with1.Format = sourceValue.Format;
			_with1.LODs = Mapper.Map<AgsLod[]>(sourceValue.LODs);
			_with1.Origin = Mapper.Map<AgsPoint>(sourceValue.Orgin);
			_with1.Rows = sourceValue.Rows;
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			return targetValue;
		}
	}
}
