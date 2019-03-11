using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleLineSymbol = TH.ArcGISRest.ApiImports.Symbols.SimpleLineSymbol;
using DespSimpleLineSymbol = TH.ArcGISRest.Description.Drawing.Symbol.SimpleLineSymbol;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SimpleLineSymbolMapper : ITypeConverter<AgsSimpleLineSymbol, DespSimpleLineSymbol>
	{

		public DespSimpleLineSymbol Convert(ResolutionContext context)
		{
            if (context.IsSourceValueNull || !(context.SourceValue is AgsSimpleLineSymbol))
            {
				return null;
			}
            AgsSimpleLineSymbol sourceValue = context.SourceValue as AgsSimpleLineSymbol;
			var  targetValue = new DespSimpleLineSymbol();
			var _with1 = targetValue;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Style = sourceValue.Style;
			_with1.Width = sourceValue.Width;
			return targetValue;
		}
	}
}
