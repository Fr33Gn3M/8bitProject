using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.MapServices;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class DocumentInfoConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			if (typeof(DocumentInfo).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var  dic = serializer.Deserialize<IDictionary<string, string>>(reader);
			if (dic == null) {
				return null;
			}
			var  docInfo = DocumentInfo.Parse(dic);
			return docInfo;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null) {
				serializer.Serialize(writer, null);
			}
            DocumentInfo docInfo = (DocumentInfo)value;
			var  dic = new Dictionary<string, string>(docInfo);
			serializer.Serialize(writer, dic);
		}
	}
}
