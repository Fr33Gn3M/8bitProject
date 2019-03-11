using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class FontDecorationConverter : JsonConverter
	{

		private const string  LineThrough = "line-through";
		private const string Underline = "underline";

		private const string None = "none";
		public override bool CanConvert(Type objectType)
		{
			if (typeof(FontDecoration).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var  enumStr = serializer.Deserialize<string>(reader);
			switch (enumStr.ToLower()) {
				case "linethrough":
					return FontDecoration.LineThrough;
				case "underline":
					return FontDecoration.Underline;
				case "none":
					return FontDecoration.none;
				default:
					throw new NotSupportedException();
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
            FontDecoration fd = (FontDecoration)value;
			switch (fd) {
				case FontDecoration.LineThrough:
					serializer.Serialize(writer, LineThrough);
					break;
				case FontDecoration.Underline:
					serializer.Serialize(writer, Underline);
					break;
				case FontDecoration.none:
					serializer.Serialize(writer, None);
					break;
				default:
					throw new NotSupportedException();
			}
		}
	}
}
