using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsExportOptions = TH.ArcGISRest.ApiImports.ExportOptions;
using DespExportOptions = TH.ArcGISRest.Description.Time.ExportOptions;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Time
{
	[ReflectionProfileMapper()]
	public class ExportOptionsMapper : ITypeConverter<DespExportOptions, AgsExportOptions>
	{

		public AgsExportOptions Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespExportOptions sourceValue = context.SourceValue as DespExportOptions;
			var  targetValue = new AgsExportOptions();
			var _with1 = targetValue;
			_with1.TimeDataCumulative = sourceValue.TimeDataCumulative;
            _with1.TimeOffset = (int)sourceValue.TimeOffset;
			_with1.TimeOffsetUnits = sourceValue.TimeoffsetUnits.ToString();
			_with1.UseTime = sourceValue.UseTime;
			return targetValue;
		}
	}
}
