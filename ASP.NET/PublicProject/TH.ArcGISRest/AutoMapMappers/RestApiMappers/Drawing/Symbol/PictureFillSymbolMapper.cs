using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsPictureFillSymbol = TH.ArcGISRest.ApiImports.Symbols.PictureFillSymbol;
using DespPictureFillSymbol = TH.ArcGISRest.Description.Drawing.Symbol.PictureFillSymbol;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class PictureFillSymbolMapper : ITypeConverter<AgsPictureFillSymbol, DespPictureFillSymbol>
	{

		public DespPictureFillSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is AgsPictureFillSymbol)) {
				return null;
			}
            AgsPictureFillSymbol sourceValue = context.SourceValue as AgsPictureFillSymbol;
			var  targetValue = new DespPictureFillSymbol();
			var _with1 = targetValue;
			_with1.Angle = sourceValue.Angle;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.Image = Mapper.Map<BinaryImage>(sourceValue);
			_with1.XOffset = sourceValue.XOffset;
			_with1.XScale = sourceValue.XScale;
			_with1.YOffset = sourceValue.YOffset;
			_with1.YScael = sourceValue.YScale;
			return targetValue;
		}
	}
}
