using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsClassBreakInfo = TH.ArcGISRest.ApiImports.Renderers.ClassBreakInfo;
using DespClassBreakInfo = TH.ArcGISRest.Description.Drawing.Render.ClassBreakInfo;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class ClassBreakInfoMapper : ITypeConverter<DespClassBreakInfo, AgsClassBreakInfo>
	{

		public AgsClassBreakInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespClassBreakInfo sourceValue = context.SourceValue as DespClassBreakInfo;
			var  targetValue = new AgsClassBreakInfo();
			var _with1 = targetValue;
			_with1.ClassMaxValue = sourceValue.ClassMaxValue;
			_with1.Description = sourceValue.Description;
			_with1.Label = sourceValue.Label;
			_with1.Symbol = Mapper.Map<ISymbol>(sourceValue.Symbol);
			return targetValue;
		}
	}
}
