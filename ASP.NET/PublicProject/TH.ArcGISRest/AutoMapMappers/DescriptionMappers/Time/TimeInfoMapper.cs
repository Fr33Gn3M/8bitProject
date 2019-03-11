using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsTimeInfo = TH.ArcGISRest.ApiImports.TimeInfo;
using DespTimeInfo = TH.ArcGISRest.Description.Time.TimeInfo;
using AutoMapper;
using AgsExportOptions = TH.ArcGISRest.ApiImports.ExportOptions;
using AgsTimeReferance = TH.ArcGISRest.ApiImports.TimeReference;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Time
{
	[ReflectionProfileMapper()]
	public class TimeInfoMapper : ITypeConverter<DespTimeInfo, AgsTimeInfo>
	{

		public AgsTimeInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespTimeInfo sourceValue = context.SourceValue as DespTimeInfo;
			var  targetValue = new AgsTimeInfo();
			var _with1 = targetValue;
			_with1.EndTimeField = sourceValue.EndTimeField;
			_with1.ExportOptions = Mapper.Map<AgsExportOptions>(sourceValue.ExportOptions);
			_with1.StartTimeField = sourceValue.StartTimeField;
			_with1.TimeExtent = Mapper.Map<object[]>(sourceValue.TimeExtent);
            _with1.TimeInterval = (int)sourceValue.TimeInterval;
			_with1.TimeReference = Mapper.Map<AgsTimeReferance>(sourceValue.TimeReference);
			_with1.TrackIdField = sourceValue.TrackIdField;
			return targetValue;
		}
	}
}
