using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using TH.ArcGISRest.Description.MapServer;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.ApiImports.Renderers;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.ApiImports.FeatureService;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class MapLayerInfoMapper : ITypeConverter<MapLayerInfo, MapServiceLayerTableInfo>
	{

		public MapServiceLayerTableInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            MapLayerInfo sourceValue = context.SourceValue as MapLayerInfo;
			var  targetValue = new MapServiceLayerTableInfo();
			var _with1 = targetValue;
			_with1.Id = sourceValue.Id;
			_with1.Name = sourceValue.Name;
			_with1.Description = sourceValue.Description;
			_with1.LayerType = sourceValue.LayerType;
			_with1.DefinitionExpression = sourceValue.DefinitionExpression;
			_with1.GeometryType = sourceValue.GeometryType;
			_with1.CopyrightText = sourceValue.CopyrightText;
			_with1.ParentLayer = Mapper.Map<IdNamePair>(sourceValue.ParentLayeerGroup);
			_with1.SubLayers = Mapper.Map<IdNamePair[]>(sourceValue.SubLayers);
			_with1.MaxScale = sourceValue.MaxSacle;
			_with1.MinScale = sourceValue.MinSacle;
			_with1.Extent = Mapper.Map<AgsEnvelope>(sourceValue.Extent);
			_with1.TimeInfo = Mapper.Map<TimeInfo>(sourceValue.TimeInfo);
			_with1.DrawingInfo = Mapper.Map<DrawingInfo>(sourceValue.DrawingInfo);
			_with1.HasAttachments = sourceValue.HasAttachments;
			_with1.HtmlPopupType = sourceValue.HtmlPopupType;
			_with1.DisplayField = sourceValue.DisplayField;
			_with1.Fields = Mapper.Map<FeatureField[]>(sourceValue.Fields);
			_with1.Types = Mapper.Map<FeatureTypeInfo[]>(sourceValue.Types);
			_with1.Relationships = Mapper.Map<FeatureRelationshipPair[]>(sourceValue.Relationships);
			_with1.TypeIdField = sourceValue.TypeIdField;
			return targetValue;
		}
	}
}
