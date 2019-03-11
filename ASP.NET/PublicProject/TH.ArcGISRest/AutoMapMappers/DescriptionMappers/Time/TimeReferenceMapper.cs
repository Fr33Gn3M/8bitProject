using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsTimeReference = TH.ArcGISRest.ApiImports.TimeReference;
using DespTimeReference = TH.ArcGISRest.Description.Time.TimeReference;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Time
{
	[ReflectionProfileMapper()]
	public class TimeReferenceMapper : ITypeConverter<DespTimeReference, AgsTimeReference>
	{

		public AgsTimeReference Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespTimeReference sourceValue = context.SourceValue as DespTimeReference;
			var  targetValue = new AgsTimeReference();
			var _with1 = targetValue;
			_with1.RespectsDaylightSaving = sourceValue.RespectsDaylightSaving;
			_with1.TimeZone = sourceValue.TimeZone;
			return targetValue;
		}
	}
}
