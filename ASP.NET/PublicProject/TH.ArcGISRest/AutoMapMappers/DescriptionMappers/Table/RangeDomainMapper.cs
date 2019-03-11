using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsRangeDomain = TH.ArcGISRest.ApiImports.Domains.RangeDomain;
using DespRangeDomain = TH.ArcGISRest.Description.Table.RangeDomain;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class RangeDomainMapper : ITypeConverter<DespRangeDomain, AgsRangeDomain>
	{

		public AgsRangeDomain Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespRangeDomain sourceValue = context.SourceValue as DespRangeDomain;
			var  targetValue = new AgsRangeDomain();
			var _with1 = targetValue;
			_with1.Name = sourceValue.Name;
			_with1.Range = sourceValue.Range;
			_with1.Type = DomainType.Range;
			return targetValue;
		}
	}
}
