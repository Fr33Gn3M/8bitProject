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
	public class MapLayerTypeConverter : JsonConverter
	{

		private const string  FeatureLayer = "Feature Layer";
		private const string Table = "Table";
		private const string GroupLayer = "Group Layer";

        private const string RasterLayer = "Raster Layer";
		public override bool CanConvert(Type objectType)
		{
			if (typeof(MapLayerType).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var typeString = serializer.Deserialize<string>(reader);
            var str=typeString.Remove(" ").ToLower();
            switch (str)
            {
				case "featurelayer":
					return MapLayerType.FeatureLayer;
				case "要素图层":
					return MapLayerType.FeatureLayer;
				case "table":
					return MapLayerType.Table;
				case "grouplayer":
					return MapLayerType.GroupLayer;
				case "rasterlayer":
					return MapLayerType.RasterLayer;
				default:
					throw new NotSupportedException();
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
            MapLayerType flt = (MapLayerType)value;
			switch (flt) {
				case MapLayerType.FeatureLayer:
					serializer.Serialize(writer, FeatureLayer);
					break;
				case MapLayerType.Table:
					serializer.Serialize(writer, Table);
					break;
				case MapLayerType.GroupLayer:
					serializer.Serialize(writer, GroupLayer);
					break;
				case MapLayerType.RasterLayer:
					serializer.Serialize(writer, RasterLayer);
					break;
			}
		}
	}
}
