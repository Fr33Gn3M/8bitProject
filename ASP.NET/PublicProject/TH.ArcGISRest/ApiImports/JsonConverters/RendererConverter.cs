using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using TH.ArcGISRest.ApiImports.Renderers;
using Newtonsoft.Json.Linq;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class RendererConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			if (objectType.IsAssignableFrom(typeof(RendererBase))) {
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
			var  rendererJson = serializer.Deserialize<JObject>(reader);
			if (rendererJson.Type == JTokenType.Null) {
				return null;
			}
			var  renderJsonString = rendererJson.ToString();
			using (var sr = new StringReader(renderJsonString)) {
                RendererBase renderer = (RendererBase)serializer.Deserialize(sr, typeof(RendererBase));
				if (reader == null) {
					return null;
				}
				using (var sr2 = new StringReader(renderJsonString)) {
					switch (renderer.Type) {
						case RendererType.classBreaks:
							renderer = (RendererBase) serializer.Deserialize(sr2, typeof(ClassBreaksRenderer));
							break;
						case RendererType.simple:
                            renderer = (RendererBase)serializer.Deserialize(sr2, typeof(SimpleRenderer));
							break;
						case RendererType.uniqueValue:
                            renderer = (RendererBase)serializer.Deserialize(sr2, typeof(UniqueValueRenderer));
							break;
						default:
							return null;
					}
					return renderer;
				}
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
	}
}
