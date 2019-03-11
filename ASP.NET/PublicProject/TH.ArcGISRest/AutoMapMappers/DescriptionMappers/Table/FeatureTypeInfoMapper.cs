using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespFeatureTypeInfo = TH.ArcGISRest.Description.Table.FeatureTypeInfo;
using AgsFeatureTypeInfo = TH.ArcGISRest.ApiImports.FeatureService.FeatureTypeInfo;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Domains;
using TH.ArcGISRest.ApiImports.FeatureService;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureTypeInfoMapper : ITypeConverter<DespFeatureTypeInfo, AgsFeatureTypeInfo>
	{

		public AgsFeatureTypeInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespFeatureTypeInfo sourceValue = context.SourceValue as DespFeatureTypeInfo;
			var  targetValue = new AgsFeatureTypeInfo();
			var _with1 = targetValue;
			_with1.Id = sourceValue.Id;
			_with1.Name = sourceValue.Name;
			_with1.Domains = Mapper.Map<DomainBase[]>(sourceValue.Domains);
			_with1.Templates = Mapper.Map<FeatureTemplate[]>(sourceValue.Templates);
			return targetValue;
		}
	}
}
