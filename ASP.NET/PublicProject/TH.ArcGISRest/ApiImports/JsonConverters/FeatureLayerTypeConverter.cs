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
	public class FeatureLayerTypeConverter : JsonConverter
	{

		private const string FeatureLayer = "Feature Layer";

		private const string Table = "Table";

		public override bool CanConvert(System.Type objectType)
		{
			if (typeof(FeatureLayerType).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			var  typeString = serializer.Deserialize<string>(reader);
			switch (typeString.ToLower()) {
				case "featurelayer":
					return FeatureLayerType.FeatureLayer;
                case "feature layer":
                    return FeatureLayerType.FeatureLayer;
				case "table":
					return FeatureLayerType.Table;
				default:
					throw new NotSupportedException();
			}
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
            FeatureLayerType flt = (FeatureLayerType)value;
			switch (flt) {
				case FeatureLayerType.FeatureLayer:
					serializer.Serialize(writer, FeatureLayer);
					break;
				case FeatureLayerType.Table:
					serializer.Serialize(writer, Table);
					break;
				default:
					throw new NotSupportedException();
			}
		}
	}
}
