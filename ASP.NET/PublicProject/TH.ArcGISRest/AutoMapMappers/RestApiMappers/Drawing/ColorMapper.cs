using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsColor = TH.ArcGISRest.ApiImports.Symbols.Color;
using DespColor = TH.ArcGISRest.Description.Drawing.Color;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class ColorMapper : ITypeConverter<AgsColor, DespColor>
	{

		public DespColor Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsColor sourceValue = context.SourceValue as AgsColor;
			var  targetValue = new DespColor();
			var _with1 = targetValue;
			_with1.Alpha = sourceValue.Alpha;
			_with1.Blue = sourceValue.Blue;
			_with1.Green = sourceValue.Green;
			_with1.Red = sourceValue.Red;
			return targetValue;
		}
	}
}
