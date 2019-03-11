using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsFeatureTemplate = TH.ArcGISRest.ApiImports.FeatureService.FeatureTemplate;
using DespFeatureTemplate = TH.ArcGISRest.Description.Table.FeatureTemplate;
using AutoMapper;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureTemplateMapper : ITypeConverter<AgsFeatureTemplate, DespFeatureTemplate>
	{

		public DespFeatureTemplate Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsFeatureTemplate sourceValue = context.SourceValue as AgsFeatureTemplate;
			var  targetValue = new DespFeatureTemplate();
			var _with1 = targetValue;
			_with1.Description = sourceValue.Description;
			_with1.DrawingTool = sourceValue.DrawingTool;
			_with1.Name = sourceValue.Name;
			_with1.Prototype = Mapper.Map<Feature>(sourceValue.Prototype);
			return targetValue;
		}
	}
}
