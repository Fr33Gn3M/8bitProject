using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.ApiImports.Renderers
{
	[Serializable()]
	[JsonObject()]
	public class SimpleRenderer : RendererBase
	{

		[JsonProperty("symbol")]
		[JsonConverter(typeof(SymbolConverter))]
		public ISymbol Symbol { get; set; }
		[JsonProperty("label")]
		public string Label { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("uniqueValueInfos")]
		public UniqueValueInfo[] UniqueValueInfos { get; set; }

		public static SimpleRenderer CreateDefaultRenderer(EsriGeometryType geoType)
		{
			var  simpleRenderer = new SimpleRenderer();
			var _with1 = simpleRenderer;
			switch (geoType) {
				case EsriGeometryType.esriGeometryPoint:
					_with1.Symbol = CreatePointSymbol();
					break;
				case EsriGeometryType.esriGeometryPolygon:
					_with1.Symbol = CreatePolygonSymbol();
					break;
				case EsriGeometryType.esriGeometryPolyline:
					_with1.Symbol = CreateLineSymbol();
					break;
			}
			return simpleRenderer;
		}

		private static SimpleLineSymbol CreateLineSymbol()
		{
			var  symbol = new SimpleLineSymbol();
			var _with2 = symbol;
			_with2.Color = new Color { Red = 255 };
			_with2.Width = 2;
			_with2.Style = SimpleLineSymbolStyle.esriSLSSolid;
			return symbol;
		}

		private static SimpleFillSymbol CreatePolygonSymbol()
		{
			var  symbol = new SimpleFillSymbol();
			var _with3 = symbol;
			_with3.Color = new Color { Green = 255 };
			_with3.Outline = CreateLineSymbol();
			_with3.Style = SimpleFillSymbolStyle.esriSFSSolid;
			return symbol;
		}

		private static SimpleMarkerSymbol CreatePointSymbol()
		{
			var  symbol = new SimpleMarkerSymbol();
			var _with4 = symbol;
			_with4.Color = new Color {
				Red = 255,
				Green = 255,
				Blue = 255
			};
			_with4.Outline = CreateLineSymbol();
			_with4.Size = 10;
			_with4.Style = SimpleMarkerSymbolStyle.esriSMSCircle;
			return symbol;
		}
	}
}
