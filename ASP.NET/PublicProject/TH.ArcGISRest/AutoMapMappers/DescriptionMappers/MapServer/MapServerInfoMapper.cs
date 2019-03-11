using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.Description.MapServer;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports;
using AgsTileInfo = TH.ArcGISRest.ApiImports.MapServices.TileInfo;
using PGK.Extensions;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class MapServerInfoMapper : ITypeConverter<MapServerInfo, MapServiceInfo>
	{

		public MapServiceInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            MapServerInfo sourceValue = context.SourceValue as MapServerInfo;
			var  targetValue = new MapServiceInfo();
			var _with1 = targetValue;
			_with1.Capabilities = Mapper.Map<string>(sourceValue.Capabilities);
			_with1.CopyrightText = sourceValue.CopyrightText;
			_with1.Description = sourceValue.Description;
			//If Not IsNothing(sourceValue.DocumentInfo) Then
			//    .DocumentInfo = New DocumentInfo
			//    For Each kv In sourceValue.DocumentInfo
			//        .DocumentInfo.Add(kv.Key, kv.Value)
			//    Next
			//End If
			_with1.DocumentInfo = Mapper.Map<DocumentInfo>(sourceValue.DocumentInfo);
			_with1.FullExtent = Mapper.Map<AgsEnvelope>(sourceValue.FullExtent);
			_with1.InitialExtent = Mapper.Map<AgsEnvelope>(sourceValue.InitExtent);
			_with1.Layers = Mapper.Map<MapServiceLayerSummaryInfo[]>(sourceValue.LayerIndexes);
			_with1.MapName = sourceValue.MapName;
			_with1.ServiceDescription = sourceValue.ServiceDescription;
			_with1.SingleFusedMapCache = sourceValue.SingleFusedMapCache;
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			_with1.SupportedImageFormatTypes = sourceValue.SupportedImageFormatTypes.ToString("", "", "", ",");
			_with1.Tables = Mapper.Map<IdNamePair[]>(sourceValue.TableIndex);
			_with1.TileInfo = Mapper.Map<AgsTileInfo>(sourceValue.TileInfo);
			_with1.Units = Mapper.Map<string>(sourceValue.Units);
			return targetValue;
		}
	}
}
