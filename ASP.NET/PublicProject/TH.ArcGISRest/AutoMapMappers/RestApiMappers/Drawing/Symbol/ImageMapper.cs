using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Symbols;
using TH.ArcGISRest.Description.Drawing;
using AutoMapper;
using System.Net;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class ImageMapper : ITypeConverter<PictureMarkerSymbol, BinaryImage>
	{

		public BinaryImage Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            PictureMarkerSymbol sourceValue = context.SourceValue as PictureMarkerSymbol;
			var  targetValue = new BinaryImage();
			var _with1 = targetValue;
			_with1.ContentType = sourceValue.ContentType;
			_with1.Height =(int) sourceValue.Height;
            _with1.Width = (int)sourceValue.Width;
			if (!string.IsNullOrWhiteSpace(sourceValue.ImageData)) {
				_with1.Content = System.Convert.FromBase64String(sourceValue.ImageData);
			} else {
				using (var wc = new WebClient()) {
					var  bytes = wc.DownloadData(sourceValue.Url);
					_with1.Content = bytes;
				}
			}
			return targetValue;
		}
	}
}
