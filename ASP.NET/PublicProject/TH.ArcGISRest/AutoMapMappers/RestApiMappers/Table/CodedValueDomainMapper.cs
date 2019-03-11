using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsCodedValueDomain = TH.ArcGISRest.ApiImports.Domains.CodedValueDomain;
using DespCodedValueDomain = TH.ArcGISRest.Description.Table.CodedValueDomain;
using AutoMapper;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class CodedValueDomainMapper : ITypeConverter<AgsCodedValueDomain, DespCodedValueDomain>
	{

		public Description.Table.CodedValueDomain Convert(AutoMapper.ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsCodedValueDomain sourceValue = context.SourceValue as AgsCodedValueDomain;
			var  targetValue = new DespCodedValueDomain();
			var _with1 = targetValue;
			_with1.CodeValues = Mapper.Map<CodeValue[]>(sourceValue.CodeValues);
			_with1.Name = sourceValue.Name;
			return targetValue;
		}
	}
}
