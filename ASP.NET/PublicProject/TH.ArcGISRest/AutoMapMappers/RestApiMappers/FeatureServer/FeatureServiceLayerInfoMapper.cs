using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureService;
using TH.ArcGISRest.Description.FeatureServer;
using AutoMapper;
using TH.ArcGISRest.Description.Table;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.Description.Drawing;
using TH.ArcGISRest.Description.Time;
using DespFeatureTypeInfo = TH.ArcGISRest.Description.Table.FeatureTypeInfo;
using DespFeatureTemplate = TH.ArcGISRest.Description.Table.FeatureTemplate;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.FeatureServer
{
	[ReflectionProfileMapper()]
	public class FeatureServiceLayerInfoMapper : ITypeConverter<FeatureServiceLayerInfo, FeatureLayerInfo>
	{

		public FeatureLayerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureServiceLayerInfo sourceValue = context.SourceValue as FeatureServiceLayerInfo;
			var  targetValue = new FeatureLayerInfo();
			var _with1 = targetValue;
			_with1.Id = sourceValue.Id;
			_with1.Name = sourceValue.Name;
			_with1.LayerType = sourceValue.LayerType;
			_with1.Description = sourceValue.Description;
			_with1.CopyrightText = sourceValue.CopyrightText;
			_with1.Relationships = Mapper.Map<RelationshipPair[]>(sourceValue.Relationships);
			_with1.GeometryType = sourceValue.GeometryType;
			_with1.MinScale = (int)sourceValue.MinScale;
            _with1.MaxScale = (int)sourceValue.MaxScale;
			_with1.Extent = Mapper.Map<Envelope>(sourceValue.Extent);
			_with1.DrawingInfo = Mapper.Map<DrawingInfo>(sourceValue.DrawingInfo);
			_with1.TimeInfo = Mapper.Map<TimeInfo>(sourceValue.TimeInfo);
			_with1.HasAttachements = sourceValue.HasAttachments;
			_with1.HtmlPopupType = sourceValue.HtmlPopupType;
			_with1.ObjectIdField = sourceValue.ObjectIdField;
			_with1.GlobalIdField = sourceValue.GlobalIdField;
			_with1.DisplayField = sourceValue.DisplayField;
			_with1.TypeIdType = sourceValue.TypeIdField;
			_with1.Fields = Mapper.Map<Field[]>(sourceValue.Fields);
			_with1.Types = Mapper.Map<DespFeatureTypeInfo[]>(sourceValue.Types);
			_with1.Templates = Mapper.Map<DespFeatureTemplate[]>(sourceValue.Templates);
			_with1.Capabilities = Mapper.Map<FeatureServiceCapability[]>(sourceValue.Capabilities);
			return targetValue;
		}
	}
}
