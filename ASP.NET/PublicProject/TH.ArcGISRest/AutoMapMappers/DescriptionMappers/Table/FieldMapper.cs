using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.Description.Table;
using AutoMapper;
using AgsDomainBase = TH.ArcGISRest.ApiImports.Domains.DomainBase;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class FieldMapper : ITypeConverter<Field, FeatureField>
	{

		public FeatureField Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			Field sourceValue = context.SourceValue  as Field;
			var  targetValue = new FeatureField();
			var _with1 = targetValue;
			_with1.Alias = sourceValue.AliasName;
			_with1.Domain = Mapper.Map<AgsDomainBase>(sourceValue.Domain);
			_with1.Editable = sourceValue.Editable;
			_with1.Length = sourceValue.Length;
			_with1.Name = sourceValue.Name;
			_with1.Type = sourceValue.Type;
			return targetValue;
		}
	}
}
