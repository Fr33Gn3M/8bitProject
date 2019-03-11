using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class NullableStringEnumConverter : StringEnumConverter
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value== null||string.IsNullOrEmpty(reader.Value.ToString())) {
				return null;
			}
			return base.ReadJson(reader, objectType, existingValue, serializer);
		}
	}
}
