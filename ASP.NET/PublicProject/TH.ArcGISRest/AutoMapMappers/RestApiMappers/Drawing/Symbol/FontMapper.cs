using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsFont = TH.ArcGISRest.ApiImports.Symbols.Font;
using DespFont = TH.ArcGISRest.Description.Drawing.Symbol.Font;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class FontMapper : ITypeConverter<AgsFont, DespFont>
	{

		public DespFont Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsFont sourceValue = context.SourceValue as AgsFont;
			var  targetValue = new DespFont();
			var _with1 = targetValue;
			_with1.Decoration = sourceValue.Decoration;
			_with1.Family = sourceValue.Family;
			_with1.Size = sourceValue.Size;
			_with1.Style = sourceValue.Style;
			_with1.Weight = sourceValue.Weight;
			return targetValue;
		}
	}
}
