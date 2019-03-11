using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using AgsTimeInfo = TH.ArcGISRest.ApiImports.TimeInfo;
using DespTimeInfo = TH.ArcGISRest.Description.Time.TimeInfo;
using TH.ArcGISRest.Description.Time;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Time
{
	[ReflectionProfileMapper()]
	public class TimeInfoMapper : ITypeConverter<AgsTimeInfo, DespTimeInfo>
	{

		public DespTimeInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsTimeInfo sourceValue = context.SourceValue as AgsTimeInfo;
			var  targetValue = new DespTimeInfo();
			var _with1 = targetValue;
			_with1.EndTimeField = sourceValue.EndTimeField;
			_with1.ExportOptions = Mapper.Map<ExportOptions>(sourceValue.ExportOptions);
			_with1.StartTimeField = sourceValue.StartTimeField;
			_with1.TimeExtent = Mapper.Map<TimeExtent>(sourceValue.TimeExtent);
			_with1.TimeInterval = sourceValue.TimeInterval;
			_with1.TimeReference = Mapper.Map<TimeReference>(sourceValue.TimeReference);
			_with1.TrackIdField = sourceValue.TrackIdField;
			return targetValue;
		}
	}
}
