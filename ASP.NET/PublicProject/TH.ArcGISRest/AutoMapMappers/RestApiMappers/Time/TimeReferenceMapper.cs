using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsTimeReference = TH.ArcGISRest.ApiImports.TimeReference;
using DespTimeReference = TH.ArcGISRest.Description.Time.TimeReference;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Time
{
	[ReflectionProfileMapper()]
	public class TimeReferenceMapper : ITypeConverter<AgsTimeReference, DespTimeReference>
	{

		public DespTimeReference Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsTimeReference sourceValue = context.SourceValue as AgsTimeReference;
			var  targetValue = new DespTimeReference();
			var _with1 = targetValue;
			_with1.RespectsDaylightSaving = sourceValue.RespectsDaylightSaving;
			_with1.TimeZone = sourceValue.TimeZone;
			return targetValue;
		}
	}
}
