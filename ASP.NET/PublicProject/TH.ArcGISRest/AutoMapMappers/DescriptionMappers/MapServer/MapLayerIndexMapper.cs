using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.MapServer;
using TH.ArcGISRest.ApiImports.MapServices;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class MapLayerIndexMapper : ITypeConverter<MapLayerIndex, MapServiceLayerSummaryInfo>
	{

		public MapServiceLayerSummaryInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            MapLayerIndex sourceValue = context.SourceValue as MapLayerIndex; 
			var  targetValue = new MapServiceLayerSummaryInfo();
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
