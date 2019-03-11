using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.Symbols
{
	[Serializable()]
	[JsonObject()]
	public class PictureMarkerSymbol : WithTypeSymbolBase, IBase64Image
	{
		[JsonProperty("url")]
		public string Url { get; set; }
		[JsonProperty("imageData")]
		public string ImageData { get; set; }
		string IBase64Image.Content {
			get { return ImageData; }
			set { ImageData = value; }
		}
		[JsonProperty("contentType")]
		public string ContentType { get; set; }
		[JsonProperty("color")]
		[JsonConverter(typeof(ColorConverter))]
		public Color Color { get; set; }
		[JsonProperty("width")]
		public double Width { get; set; }
		[JsonProperty("height")]
		public double Height { get; set; }
		[JsonProperty("angle")]
		public double Angle { get; set; }
		[JsonProperty("xoffset")]
		public double XOffset { get; set; }
		[JsonProperty("yoffset")]
		public double YOffset { get; set; }
	}
}
