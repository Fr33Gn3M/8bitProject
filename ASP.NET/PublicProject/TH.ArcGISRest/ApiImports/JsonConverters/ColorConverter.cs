using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class ColorConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			if (typeof(Color).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var  colorArrary = serializer.Deserialize<int[]>(reader);
			if (colorArrary == null || colorArrary.Length != 4) {
				return null;
			}
			var  color = new Color(colorArrary);
			return color;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Color color = value as Color;
			var colorArrary =new int[] {
				color.Red,
				color.Green,
				color.Blue,
				color.Alpha
			};
			serializer.Serialize(writer, colorArrary);
		}
	}
}
