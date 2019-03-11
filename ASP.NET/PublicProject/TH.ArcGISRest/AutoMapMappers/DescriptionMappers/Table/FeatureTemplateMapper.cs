using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespFeatureTemplate = TH.ArcGISRest.Description.Table.FeatureTemplate;
using AgsFeatureTemplate = TH.ArcGISRest.ApiImports.FeatureService.FeatureTemplate;
using AutoMapper;
using TH.ArcGISRest.ApiImports.FeatureBase;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureTemplateMapper : ITypeConverter<DespFeatureTemplate, AgsFeatureTemplate>
	{

		public AgsFeatureTemplate Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespFeatureTemplate sourceValue = context.SourceValue as DespFeatureTemplate;
			var  targetValue = new AgsFeatureTemplate();
			var _with1 = targetValue;
			_with1.Name = sourceValue.Name;
			_with1.Description = sourceValue.Description;
			_with1.DrawingTool = sourceValue.DrawingTool;
			_with1.Prototype = Mapper.Map<EsriFeature>(sourceValue.Prototype);
			return targetValue;
		}
	}
}
