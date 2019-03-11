using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsPictureMarkerSymbol = TH.ArcGISRest.ApiImports.Symbols.PictureMarkerSymbol;
using DespPictureMarkerSymbol = TH.ArcGISRest.Description.Drawing.Symbol.PictureMarkerSymbol;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class PictureMarkerSymbolMapper : ITypeConverter<DespPictureMarkerSymbol, AgsPictureMarkerSymbol>
	{

		public AgsPictureMarkerSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespPictureMarkerSymbol)) {
				return null;
			}
            DespPictureMarkerSymbol sourceValue = context.SourceValue as DespPictureMarkerSymbol;
			var  targetValue = new AgsPictureMarkerSymbol();
			var _with1 = targetValue;
			_with1.Angle = sourceValue.Angle;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.ContentType = sourceValue.Image.ContentType;
			_with1.Height = sourceValue.Image.Height;
			_with1.ImageData = System.Convert.ToBase64String(sourceValue.Image.Content);
			_with1.Type = SymbolType.esriPMS;
			_with1.Width = sourceValue.Image.Width;
			_with1.XOffset = sourceValue.XOffset;
			_with1.YOffset = sourceValue.YOffset;
			return targetValue;
		}
	}
}
