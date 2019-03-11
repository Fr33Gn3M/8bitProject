using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleMarkerSymbol = TH.ArcGISRest.ApiImports.Symbols.SimpleMarkerSymbol;
using DespSimpleMarkerSymbol = TH.ArcGISRest.Description.Drawing.Symbol.SimpleMarkerSymbol;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SimpleMarkerSymbolMapper : ITypeConverter<DespSimpleMarkerSymbol, AgsSimpleMarkerSymbol>
	{

		public AgsSimpleMarkerSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespSimpleMarkerSymbol)) {
				return null;
			}
            DespSimpleMarkerSymbol sourceValue = context.SourceValue as DespSimpleMarkerSymbol;
			var  targetValue = new AgsSimpleMarkerSymbol();
			var _with1 = targetValue;
			_with1.Angle = sourceValue.Angle;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Outline = Mapper.Map<SimpleLineSymbol>(sourceValue.Outline);
			_with1.Size = sourceValue.Size;
			_with1.Style = sourceValue.Style;
			_with1.Type = SymbolType.esriSMS;
			_with1.XOffset = sourceValue.XOffset;
			_with1.YOffset = sourceValue.YOffset;
			return targetValue;
		}
	}
}
