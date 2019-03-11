using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsRangeDomain = TH.ArcGISRest.ApiImports.Domains.RangeDomain;
using DespRangeDomain = TH.ArcGISRest.Description.Table.RangeDomain;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class RangeDomainMapper : ITypeConverter<AgsRangeDomain, DespRangeDomain>
	{

		public DespRangeDomain Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsRangeDomain sourceValue = context.SourceValue as AgsRangeDomain;
			var  targetValue = new DespRangeDomain();
			var _with1 = targetValue;
			_with1.Name = sourceValue.Name;
			_with1.Range = sourceValue.Range;
			return targetValue;
		}
	}
}
