using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsTileInfo = TH.ArcGISRest.ApiImports.MapServices.TileInfo;
using DespTileInfo = TH.ArcGISRest.Description.MapServer.TileInfo;
using AutoMapper;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.Description.MapServer;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class TileInfoMapper : ITypeConverter<AgsTileInfo, DespTileInfo>
	{

		public DespTileInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsTileInfo sourceValue = context.SourceValue as AgsTileInfo;
			var  targetValue = new DespTileInfo();
			var _with1 = targetValue;
			_with1.Rows = sourceValue.Rows;
			_with1.Columns = sourceValue.Cols;
			_with1.DPI = sourceValue.DPI;
			_with1.Format = sourceValue.Format;
            _with1.CompressionQuality = string.IsNullOrEmpty(sourceValue.CompressionQuality) == true ? 0 : double.Parse(sourceValue.CompressionQuality);
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			_with1.Orgin = Mapper.Map<Point>(sourceValue.Origin);
			_with1.LODs = Mapper.Map<LOD[]>(sourceValue.LODs);
			return targetValue;
		}
	}
}
