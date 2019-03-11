using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using TH.ArcGISRest.ApiImports.FeatureBase;
using Newtonsoft.Json.Linq;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class FeatureSetConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			if (typeof(FeatureSet).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var  featureSetJson = serializer.Deserialize<JObject>(reader);
			var  featureSetStr = featureSetJson.ToString();
			var  geoTypeToken = featureSetJson["geometryType"];
			var  hasRemoved = serializer.Converters.Remove(this);
			FeatureSet fs = null;
			if (geoTypeToken == null || geoTypeToken.Type == JTokenType.Null) {
				using (var sr = new StringReader(featureSetStr)) {
                    fs = (FeatureSet)serializer.Deserialize(sr, typeof(FeatureSet));
				}
			} else {
				var  geoTypeStr = geoTypeToken.Value<string>();
                EsriGeometryType geoType = (EsriGeometryType)Enum.Parse(typeof(EsriGeometryType), geoTypeStr);
				serializer.Context = new StreamingContext(StreamingContextStates.All, geoType);
				using (var sr = new StringReader(featureSetStr)) {
                    fs = (FeatureSet)serializer.Deserialize(sr, typeof(FeatureSet));
				}
			}
			if (hasRemoved) {
				serializer.Converters.Add(this);
			}
			return fs;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
	}
}
