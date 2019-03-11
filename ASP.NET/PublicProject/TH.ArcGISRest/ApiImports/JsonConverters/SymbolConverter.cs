using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using TH.ArcGISRest.ApiImports.Symbols;
using Newtonsoft.Json.Linq;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class SymbolConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			if (objectType.IsAssignableFrom(typeof(ISymbol))) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) {
				return null;
			}
			if (reader.TokenType == JsonToken.StartArray) {
				var  color = serializer.Deserialize<Color>(reader);
				return color;
			} else {
				var  symbolJson = serializer.Deserialize<JObject>(reader);
				if (symbolJson == null || symbolJson.Type == JTokenType.Null) {
					return null;
				}
				var  symbolJsonStr = symbolJson.ToString();
				using (var sr = new StringReader(symbolJsonStr)) {

                    WithTypeSymbolBase typeSymbol = (WithTypeSymbolBase)serializer.Deserialize(sr, typeof(WithTypeSymbolBase));
					if (typeSymbol == null)
                    {
						return null;
					}
					using (var sr2 = new StringReader(symbolJsonStr)) {
						switch (typeSymbol.Type) {
							case SymbolType.esriPFS:
                                typeSymbol = (WithTypeSymbolBase)serializer.Deserialize(sr2, typeof(PictureFillSymbol));
								break;
							case SymbolType.esriPMS:
                                typeSymbol = (WithTypeSymbolBase)serializer.Deserialize(sr2, typeof(PictureMarkerSymbol));
								break;
							case SymbolType.esriSFS:
                                typeSymbol = (WithTypeSymbolBase)serializer.Deserialize(sr2, typeof(SimpleFillSymbol));
								break;
							case SymbolType.esriSLS:
                                typeSymbol = (WithTypeSymbolBase)serializer.Deserialize(sr2, typeof(SimpleLineSymbol));
								break;
							case SymbolType.esriSMS:
                                typeSymbol = (WithTypeSymbolBase)serializer.Deserialize(sr2, typeof(SimpleMarkerSymbol));
								break;
							default:
								return null;
						}
						return typeSymbol;
					}
				}
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
	}
}
