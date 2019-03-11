using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Time;
using AutoMapper;
using System.Linq;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Time
{
	[ReflectionProfileMapper()]
	public class TimeExtentMapper : ITypeConverter<TimeExtent, object[]>
	{

		public object[] Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            TimeExtent sourceValue = context.SourceValue as TimeExtent;
			object[] targetValue = new object[2];
			if ((sourceValue.StartTime != null)) {
				targetValue[0] = sourceValue.StartTime.Value.Ticks;
			}
			if ((sourceValue.EndTime != null)) {
				targetValue[1] = sourceValue.EndTime.Value.Ticks;
			}
			if (targetValue.All(p => (p == null))) {
				return null;
			} else {
				return targetValue;
			}
		}
	}
}
