using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.Description.MapServer;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class MapServiceLayerSummaryInfoMapper : ITypeConverter<MapServiceLayerSummaryInfo, MapLayerIndex>
	{

		public MapLayerIndex Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            MapServiceLayerSummaryInfo sourceValue = context.SourceValue as MapServiceLayerSummaryInfo;
			var  targetValue = new MapLayerIndex();
			var _with1 = targetValue;
			_with1.DefaultVisibility = sourceValue.DefaultVisibility;
			_with1.ID = sourceValue.ID;
			_with1.Name = sourceValue.Name;
			_with1.ParentLayerId = sourceValue.ParentLayerId;
			_with1.SubLayerIds = sourceValue.SubLayerIds;
			return targetValue;
		}
	}
}
