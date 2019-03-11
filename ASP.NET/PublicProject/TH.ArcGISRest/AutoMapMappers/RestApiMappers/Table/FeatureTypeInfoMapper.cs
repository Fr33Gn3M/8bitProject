using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsFeatureTypeInfo = TH.ArcGISRest.ApiImports.FeatureService.FeatureTypeInfo;
using DespFeatureTypeInfo = TH.ArcGISRest.Description.Table.FeatureTypeInfo;
using AutoMapper;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureTypeInfoMapper : ITypeConverter<AgsFeatureTypeInfo, DespFeatureTypeInfo>
	{

		public DespFeatureTypeInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsFeatureTypeInfo sourceValue = context.SourceValue as AgsFeatureTypeInfo;
			var  targetValue = new DespFeatureTypeInfo();
			var _with1 = targetValue;
			_with1.Domains = Mapper.Map<DomainBase[]>(sourceValue.Domains);
			_with1.Id = sourceValue.Id;
			_with1.Name = sourceValue.Name;
			_with1.Templates = Mapper.Map<FeatureTemplate[]>(sourceValue.Templates);
			return targetValue;
		}
	}
}
