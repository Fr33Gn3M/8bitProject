using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsExportOptions = TH.ArcGISRest.ApiImports.ExportOptions;
using DespExportOptions = TH.ArcGISRest.Description.Time.ExportOptions;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Time
{
	[ReflectionProfileMapper()]
	public class ExportOptionsMapper : ITypeConverter<AgsExportOptions, DespExportOptions>
	{

		public DespExportOptions Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsExportOptions sourceValue = context.SourceValue as AgsExportOptions;
			var  targetValue = new DespExportOptions();
			var _with1 = targetValue;
			_with1.TimeDataCumulative = sourceValue.TimeDataCumulative;
            _with1.TimeOffset = sourceValue.TimeOffset == null ? 0 : sourceValue.TimeOffset.Value;
			_with1.UseTime = sourceValue.UseTime;
			return targetValue;
		}
	}
}
