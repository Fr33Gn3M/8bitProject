using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.Description.Table;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureFieldMapper : ITypeConverter<FeatureField, Field>
	{

		public Field Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureField sourceValue = context.SourceValue as FeatureField;
			var  targetValue = new Field();
			var _with1 = targetValue;
			_with1.AliasName = sourceValue.Alias;
			_with1.Domain = Mapper.Map<DomainBase>(sourceValue.Domain);
			_with1.Editable = sourceValue.Editable;
			_with1.Length = sourceValue.Length;
			_with1.Name = sourceValue.Name;
			_with1.Type = sourceValue.Type;
			return targetValue;
		}
	}
}
