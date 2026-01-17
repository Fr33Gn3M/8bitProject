using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace FD.DataBase.JsonConverters
{
    [Serializable()]
    public class QueryFilterConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsAssignableFrom(typeof(QueryFilterBase)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string[] types = new string[] { "Geometry", "Filters", "Value" };
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            var rendererJson = serializer.Deserialize<JObject>(reader);
            if (rendererJson.Type == JTokenType.Null)
            {
                return null;
            }
            var list = new List<KeyValuePair<string, JToken>>();
            var currentKey = "Value";
            foreach (var item in rendererJson)
            {
                if (types.Contains(item.Key))
                {
                    currentKey = item.Key;
                    break;
                }
            }
            QueryFilterBase filter = null;
            var renderJsonString = rendererJson.ToString();
            using (var sr = new StringReader(renderJsonString))
            {
                switch (currentKey)
                {
                    case "Value":
                        filter = (QueryFilter)serializer.Deserialize(sr, typeof(QueryFilter));
                        break;
                    case "Geometry":
                        filter = (SpatialQueryFilter)serializer.Deserialize(sr, typeof(SpatialQueryFilter));
                        break;
                    case "Filters":
                        filter = (AndOrQueryFilter)serializer.Deserialize(sr, typeof(AndOrQueryFilter));
                        break;
                }
                return filter;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
