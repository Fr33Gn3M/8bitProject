using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleLineSymbol = TH.ArcGISRest.ApiImports.Symbols.SimpleLineSymbol;
using DespSimpleLineSymbol = TH.ArcGISRest.Description.Drawing.Symbol.SimpleLineSymbol;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SimpleLineSymbolMapper : ITypeConverter<DespSimpleLineSymbol, AgsSimpleLineSymbol>
	{

		public AgsSimpleLineSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespSimpleLineSymbol)) {
				return null;
			}
            DespSimpleLineSymbol sourceValue = context.SourceValue as DespSimpleLineSymbol;
			var  targetValue = new AgsSimpleLineSymbol();
			var _with1 = targetValue;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Style = sourceValue.Style;
			_with1.Type = SymbolType.esriSLS;
			_with1.Width = sourceValue.Width;
			return targetValue;
		}
	}
}
