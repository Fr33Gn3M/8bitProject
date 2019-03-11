using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsClassBreakInfo = TH.ArcGISRest.ApiImports.Renderers.ClassBreakInfo;
using DespClassBreakInfo = TH.ArcGISRest.Description.Drawing.Render.ClassBreakInfo;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Symbol;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class ClassBreakInfoMapper : ITypeConverter<AgsClassBreakInfo, DespClassBreakInfo>
	{

		public DespClassBreakInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsClassBreakInfo sourceValue = context.SourceValue as AgsClassBreakInfo;
			var  targetValue = new DespClassBreakInfo();
			var _with1 = targetValue;
			_with1.ClassMaxValue = sourceValue.ClassMaxValue;
			_with1.Description = sourceValue.Description;
			_with1.Label = sourceValue.Label;
			_with1.Symbol = Mapper.Map<SymbolBase>(sourceValue.Symbol);
			return targetValue;
		}
	}
}
