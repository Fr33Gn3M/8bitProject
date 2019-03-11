using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsTextSymbol = TH.ArcGISRest.ApiImports.Symbols.TextSymbol;
using DespTextSymbol = TH.ArcGISRest.Description.Drawing.Symbol.TextSymbol;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class TextSymbolMapper : ITypeConverter<DespTextSymbol, AgsTextSymbol>
	{

		public AgsTextSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespTextSymbol)) {
				return null;
			}
            DespTextSymbol sourceValue = context.SourceValue as DespTextSymbol;
			var  targetValue = new AgsTextSymbol();
			var _with1 = targetValue;
			_with1.Angle = sourceValue.Angle;
			_with1.BackgroundColor = Mapper.Map<Color>(sourceValue.BackgroundColor);
			_with1.BorderLineColor = Mapper.Map<Color>(sourceValue.BorderLineColor);
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Font = Mapper.Map<Font>(sourceValue.Font);
			_with1.HorizontalAlignment = sourceValue.HorizontalAlignment;
			_with1.Kerning = sourceValue.Kerning;
			_with1.RightToLeft = sourceValue.RightToLeft;
			_with1.Type = SymbolType.esriTS;
			_with1.VerticalAlignment = sourceValue.VerticalAlignment;
			_with1.XOffset = sourceValue.XOffset;
			_with1.YOffset = sourceValue.YOffset;
			return targetValue;
		}
	}
}
