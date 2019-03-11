using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Time;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Time
{
	[ReflectionProfileMapper()]
	public class TimeExtentMapper : ITypeConverter<object[], TimeExtent>
	{

		public TimeExtent Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			object[] sourceValue = context.SourceValue as object[];
            long? startTime = (long?)sourceValue[0];
            long? endTime = (long?)sourceValue[1];
			if ((startTime == null) && (endTime == null)) {
				return null;
			}
			var  targetValue = new TimeExtent();
			var _with1 = targetValue;
			if ((startTime != null)) {
				_with1.StartTime = new System.DateTime(startTime.Value);
			}
			if ((endTime != null)) {
				_with1.EndTime = new System.DateTime(endTime.Value);
			}
			return targetValue;
		}
	}
}
