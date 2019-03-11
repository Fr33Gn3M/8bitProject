using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.Description.MapServer;
using AutoMapper;
using TH.ArcGISRest.Description;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.Description.Time;
using TH.ArcGISRest.Description.Drawing;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class MapServiceLayerTableInfoMapper : ITypeConverter<MapServiceLayerTableInfo, MapLayerInfo>
	{

		public MapLayerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			MapServiceLayerTableInfo sourceValue = context.SourceValue as  MapServiceLayerTableInfo;
			var  targetValue = new MapLayerInfo();
			var _with1 = targetValue;
			_with1.Id = sourceValue.Id;
			_with1.Name = sourceValue.Name;
			_with1.Description = sourceValue.DefinitionExpression;
			_with1.LayerType = sourceValue.LayerType;
			_with1.DefinitionExpression = sourceValue.DefinitionExpression;
			_with1.GeometryType = sourceValue.GeometryType;
			_with1.CopyrightText = sourceValue.CopyrightText;
			_with1.ParentLayeerGroup = Mapper.Map<IdNamePair>(sourceValue.ParentLayer);
			_with1.SubLayers = Mapper.Map<IdNamePair[]>(sourceValue.SubLayers);
			_with1.MinSacle = (int)sourceValue.MinScale;
            _with1.MaxSacle = (int)sourceValue.MaxScale;
			_with1.Extent = Mapper.Map<Envelope>(sourceValue.Extent);
			_with1.TimeInfo = Mapper.Map<TimeInfo>(sourceValue.TimeInfo);
			_with1.DrawingInfo = Mapper.Map<DrawingInfo>(sourceValue.DrawingInfo);
			_with1.HasAttachments = sourceValue.HasAttachments;
			_with1.HtmlPopupType = sourceValue.HtmlPopupType;
			_with1.DisplayField = sourceValue.DisplayField;
			_with1.Fields = Mapper.Map<Field[]>(sourceValue.Fields);
			_with1.Types = Mapper.Map<FeatureTypeInfo[]>(sourceValue.Types);
			_with1.Relationships = Mapper.Map<RelationshipPair[]>(sourceValue.Relationships);
			_with1.TypeIdField = sourceValue.TypeIdField;
			return targetValue;
		}
	}
}
