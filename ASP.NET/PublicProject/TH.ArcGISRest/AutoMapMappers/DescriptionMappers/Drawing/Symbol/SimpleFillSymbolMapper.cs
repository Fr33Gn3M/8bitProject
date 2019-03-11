using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleFillSymbol = TH.ArcGISRest.ApiImports.Symbols.SimpleFillSymbol;
using DespSimpleFillSymbol = TH.ArcGISRest.Description.Drawing.Symbol.SimpleFillSymbol;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SimpleFillSymbolMapper : ITypeConverter<DespSimpleFillSymbol, AgsSimpleFillSymbol>
	{

		public AgsSimpleFillSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespSimpleFillSymbol)) {
				return null;
			}
            DespSimpleFillSymbol sourceValue = context.SourceValue as DespSimpleFillSymbol;
			var  targetValue = new AgsSimpleFillSymbol();
			var _with1 = targetValue;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Outline = Mapper.Map<SimpleLineSymbol>(sourceValue.Outline);
			_with1.Style = sourceValue.Style;
			_with1.Type = SymbolType.esriSFS;
			return targetValue;
		}
	}
}
