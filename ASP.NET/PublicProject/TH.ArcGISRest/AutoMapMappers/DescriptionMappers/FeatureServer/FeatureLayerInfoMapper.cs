using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Renderers;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.Description.FeatureServer;
using TH.ArcGISRest.ApiImports.FeatureService;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.FeatureServer
{
	[ReflectionProfileMapper()]
	public class FeatureLayerInfoMapper : ITypeConverter<FeatureLayerInfo, FeatureServiceLayerInfo>
	{

		public FeatureServiceLayerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureLayerInfo sourceValue = context.SourceValue as FeatureLayerInfo;
			var  targetValue = new FeatureServiceLayerInfo();
			var _with1 = targetValue;
			_with1.Id = sourceValue.Id;
			_with1.Name = sourceValue.Name;
			_with1.LayerType = sourceValue.LayerType;
			_with1.Description = sourceValue.Description;
			_with1.CopyrightText = sourceValue.CopyrightText;
			_with1.Relationships = Mapper.Map<FeatureRelationshipPair[]>(sourceValue.Relationships);
			_with1.GeometryType = sourceValue.GeometryType;
			_with1.MaxScale = sourceValue.MaxScale;
			_with1.MinScale = sourceValue.MinScale;
			_with1.Extent = Mapper.Map<AgsEnvelope>(sourceValue.Extent);
			_with1.DrawingInfo = Mapper.Map<DrawingInfo>(sourceValue.DrawingInfo);
			_with1.TimeInfo = Mapper.Map<TimeInfo>(sourceValue.TimeInfo);
			_with1.HasAttachments = sourceValue.HasAttachements;
			_with1.HtmlPopupType = sourceValue.HtmlPopupType;
			_with1.ObjectIdField = sourceValue.ObjectIdField;
			_with1.GlobalIdField = sourceValue.GlobalIdField;
			_with1.DisplayField = sourceValue.DisplayField;
			_with1.TypeIdField = sourceValue.TypeIdType;
			_with1.Fields = Mapper.Map<FeatureField[]>(sourceValue.Fields);
			_with1.Types = Mapper.Map<FeatureTypeInfo[]>(sourceValue.Types);
			_with1.Templates = Mapper.Map<FeatureTemplate[]>(sourceValue.Templates);
			_with1.Capabilities = Mapper.Map<string>(sourceValue.Capabilities);
			return targetValue;
		}
	}
}
