using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsPictureMarkerSymbol = TH.ArcGISRest.ApiImports.Symbols.PictureMarkerSymbol;
using DespPictureMarkerSymbol = TH.ArcGISRest.Description.Drawing.Symbol.PictureMarkerSymbol;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class PictureMarkerSymbolMapper : ITypeConverter<AgsPictureMarkerSymbol, DespPictureMarkerSymbol>
	{

		public DespPictureMarkerSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is AgsPictureMarkerSymbol)) {
				return null;
			}
            AgsPictureMarkerSymbol sourceValue = context.SourceValue as AgsPictureMarkerSymbol;
			var  targetValue = new DespPictureMarkerSymbol();
			var _with1 = targetValue;
			_with1.Angle = sourceValue.Angle;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Image = Mapper.Map<BinaryImage>(sourceValue);
			_with1.XOffset = sourceValue.XOffset;
			_with1.YOffset = sourceValue.YOffset;
			return targetValue;
		}
	}
}
