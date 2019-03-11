using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsFeature = TH.ArcGISRest.ApiImports.FeatureBase.EsriFeature;
using DespFeature = TH.ArcGISRest.Description.Table.Feature;
using AutoMapper;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureMapper : ITypeConverter<AgsFeature, DespFeature>
	{

		public DespFeature Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsFeature sourceValue = context.SourceValue as AgsFeature;
			var  targetValue = new DespFeature();
			var _with1 = targetValue;
			_with1.Attributes = Mapper.Map<IDictionary<string, FeatureValueBase>>(sourceValue.Attributes);
			_with1.Geometry = Mapper.Map<GeometryBase>(sourceValue.Geometry);
			return targetValue;
		}
	}
}
