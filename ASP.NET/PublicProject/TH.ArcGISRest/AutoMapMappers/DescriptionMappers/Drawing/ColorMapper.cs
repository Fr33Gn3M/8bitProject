using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsColor = TH.ArcGISRest.ApiImports.Symbols.Color;
using DespColor = TH.ArcGISRest.Description.Drawing.Color;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class ColorMapper : ITypeConverter<DespColor, AgsColor>
	{

		public AgsColor Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespColor sourceValue = context.SourceValue as DespColor;
			var  targetValue = new AgsColor();
			var _with1 = targetValue;
			_with1.Alpha = sourceValue.Alpha;
			_with1.Blue = sourceValue.Blue;
			_with1.Green = sourceValue.Green;
			_with1.Red = sourceValue.Red;
			return targetValue;
		}
	}
}
