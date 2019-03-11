using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleFillSymbol = TH.ArcGISRest.ApiImports.Symbols.SimpleFillSymbol;
using DespSimpleFillSymbol = TH.ArcGISRest.Description.Drawing.Symbol.SimpleFillSymbol;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing;
using TH.ArcGISRest.Description.Drawing.Symbol;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SimpleFillSymbolMapper : ITypeConverter<AgsSimpleFillSymbol, DespSimpleFillSymbol>
	{

		public DespSimpleFillSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is AgsSimpleFillSymbol)) {
				return null;
			}
            AgsSimpleFillSymbol sourceValue = context.SourceValue as AgsSimpleFillSymbol;
			var  targetValue = new DespSimpleFillSymbol();
			var _with1 = targetValue;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Outline = Mapper.Map<SimpleLineSymbol>(sourceValue.Outline);
			_with1.Style = sourceValue.Style;
			return targetValue;
		}
	}
}
