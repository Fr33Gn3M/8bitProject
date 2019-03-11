using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Symbols;
using TH.ArcGISRest.Description.Drawing.Symbol;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class ColorSymbolMapper : ITypeConverter<ColorSymbol, Color>
	{

		public Color Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is ColorSymbol)) {
				return null;
			}
            ColorSymbol sourceValue = context.SourceValue as ColorSymbol;
			var  targetValue = Mapper.Map<Color>(sourceValue.Color);
			return targetValue;
		}
	}
}
