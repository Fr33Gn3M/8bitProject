using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsCodedValueDomain = TH.ArcGISRest.ApiImports.Domains.CodedValueDomain;
using DespCodeDValueDomain = TH.ArcGISRest.Description.Table.CodedValueDomain;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Domains;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class CodedValueDomainMapper : ITypeConverter<DespCodeDValueDomain, AgsCodedValueDomain>
	{

		public AgsCodedValueDomain Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespCodeDValueDomain sourceValue = context.SourceValue as DespCodeDValueDomain;
			var  targetValue = new AgsCodedValueDomain();
			var _with1 = targetValue;
			_with1.CodeValues = Mapper.Map<CodeValue[]>(sourceValue.CodeValues);
			_with1.Name = sourceValue.Name;
			_with1.Type = DomainType.CodedValue;
			return targetValue;
		}
	}
}
