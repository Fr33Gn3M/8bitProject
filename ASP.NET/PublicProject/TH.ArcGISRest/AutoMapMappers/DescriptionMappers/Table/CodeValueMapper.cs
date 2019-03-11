using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsCodeValue = TH.ArcGISRest.ApiImports.Domains.CodeValue;
using DespCodeValue = TH.ArcGISRest.Description.Table.CodeValue;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class CodeValueMapper : ITypeConverter<DespCodeValue, AgsCodeValue>
	{

		public AgsCodeValue Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespCodeValue sourceValue = context.SourceValue as DespCodeValue;
			var  targetValue = new AgsCodeValue();
			var _with1 = targetValue;
			_with1.Code = sourceValue.Code;
			_with1.Name = sourceValue.Name;
			return targetValue;
		}
	}
}
