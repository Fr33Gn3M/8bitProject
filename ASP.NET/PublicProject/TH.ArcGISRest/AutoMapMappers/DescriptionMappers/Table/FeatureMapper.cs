using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsFeature = TH.ArcGISRest.ApiImports.FeatureBase.EsriFeature;
using DespFeature = TH.ArcGISRest.Description.Table.Feature;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureMapper : ITypeConverter<DespFeature, AgsFeature>
	{

		public AgsFeature Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespFeature sourceValue = context.SourceValue as DespFeature;
			var  targetValue = new AgsFeature();
			var _with1 = targetValue;
			_with1.Attributes = Mapper.Map<IDictionary<string, object>>(sourceValue.Attributes);
			_with1.Geometry = Mapper.Map<AgsGeometryBase>(sourceValue.Geometry);
			return targetValue;
		}
	}
}
