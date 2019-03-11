using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsPictureFillSymbol = TH.ArcGISRest.ApiImports.Symbols.PictureFillSymbol;
using DespPictureFillSymbol = TH.ArcGISRest.Description.Drawing.Symbol.PictureFillSymbol;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class PictureFillSymbolMapper : ITypeConverter<DespPictureFillSymbol, AgsPictureFillSymbol>
	{

		public AgsPictureFillSymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespPictureFillSymbol)) {
				return null;
			}
            DespPictureFillSymbol sourceValue = context.SourceValue as DespPictureFillSymbol;
			var  agsValue = new AgsPictureFillSymbol();
			var _with1 = agsValue;
			_with1.Angle = sourceValue.Angle;
			_with1.Color = Mapper.Map<Color>(sourceValue.Color);
			_with1.ContentType = sourceValue.Image.ContentType;
			_with1.ImageData = System.Convert.ToBase64String(sourceValue.Image.Content);
			_with1.Height = sourceValue.Image.Height;
			_with1.Width = sourceValue.Image.Width;
			_with1.XOffset = sourceValue.XOffset;
			_with1.YOffset = sourceValue.YOffset;
			_with1.XScale = sourceValue.XScale;
			_with1.YScale = sourceValue.YScael;
			_with1.Type = SymbolType.esriPFS;
			return agsValue;
		}
	}
}
