using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.Description.MapServer;
using DespTileInfo = TH.ArcGISRest.Description.MapServer.TileInfo;
using AutoMapper;
using TH.ArcGISRest.Description;
using TH.ArcGISRest.Description.Geometry;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class MapServiceInfoMapper : ITypeConverter<MapServiceInfo, MapServerInfo>
	{

		public MapServerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            MapServiceInfo sourceValue = context.SourceValue as MapServiceInfo;
			var  targetValue = new MapServerInfo();
			var _with1 = targetValue;
			_with1.MapName = sourceValue.MapName;
			_with1.TileInfo = Mapper.Map<DespTileInfo>(sourceValue.TileInfo);
			_with1.Description = sourceValue.Description;
			_with1.ServiceDescription = sourceValue.ServiceDescription;
			_with1.LayerIndexes = Mapper.Map<MapLayerIndex[]>(sourceValue.Layers);
			_with1.TableIndex = Mapper.Map<IdNamePair[]>(sourceValue.Tables);
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			_with1.SingleFusedMapCache = sourceValue.SingleFusedMapCache;
			_with1.InitExtent = Mapper.Map<Envelope>(sourceValue.InitialExtent);
			_with1.FullExtent = Mapper.Map<Envelope>(sourceValue.FullExtent);
            _with1.Units = (EsriUnit)Enum.Parse(typeof(EsriUnit), sourceValue.Units);
			_with1.SupportedImageFormatTypes = sourceValue.SupportedImageFormatTypes.Split(",");
			_with1.Capabilities = Mapper.Map<MapServiceCapability>(sourceValue.Capabilities);
			_with1.CopyrightText = sourceValue.CopyrightText;
			_with1.DocumentInfo = Mapper.Map<IDictionary<string, string>>(sourceValue.DocumentInfo);
			return targetValue;
		}
	}


}
