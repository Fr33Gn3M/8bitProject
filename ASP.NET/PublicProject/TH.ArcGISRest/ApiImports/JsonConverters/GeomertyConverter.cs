using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
namespace TH.ArcGISRest.ApiImports.JsonConverters
{
    [Serializable()]
    public class GeomertyConverter : JsonConverter
    {



        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsAssignableFrom(typeof(AgsGeometryBase)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string[] types = new string[] { "paths", "rings", "point", "points", "envelope" };
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (serializer.Context.Context != null)
            {
                EsriGeometryType type = (EsriGeometryType)serializer.Context.Context;
                if (reader.TokenType == JsonToken.None)
                {
                    return null;
                }
                switch (type)
                {
                    case EsriGeometryType.esriGeometryPoint:
                        var pt = serializer.Deserialize<AgsPoint>(reader);
                        return pt;
                    case EsriGeometryType.esriGeometryEnvelope:
                        var env = serializer.Deserialize<AgsEnvelope>(reader);
                        return env;
                    case EsriGeometryType.esriGeometryMultipoint:
                        var mp = serializer.Deserialize<AgsMultipoint>(reader);
                        return mp;
                    case EsriGeometryType.esriGeometryPolygon:
                        var polygon = serializer.Deserialize<AgsPolygon>(reader);
                        return polygon;
                    case EsriGeometryType.esriGeometryPolyline:
                        var polyline = serializer.Deserialize<AgsPolyline>(reader);
                        return polyline;
                    default:
                        return null;
                }
            }
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
            var currentKey = "point";
            foreach (var item in rendererJson)
            {
                if (types.Contains(item.Key.ToLower()))
                {
                    currentKey = item.Key;
                    break;
                }
            }
            AgsGeometryBase renderer = null;
            var renderJsonString = rendererJson.ToString();
            using (var sr = new StringReader(renderJsonString))
            {
                switch (currentKey)
                {
                    case "point":
                        renderer = (AgsPoint)serializer.Deserialize(sr, typeof(AgsPoint));
                        break;
                    case "envelope":
                        renderer = (AgsEnvelope)serializer.Deserialize(sr, typeof(AgsEnvelope));
                        break;
                    case "points":
                        renderer = (AgsMultipoint)serializer.Deserialize(sr, typeof(AgsMultipoint));
                        break;
                    case "rings":
                        renderer = (AgsPolygon)serializer.Deserialize(sr, typeof(AgsPolygon));
                        break;

                    case "paths":
                        renderer = (AgsPolyline)serializer.Deserialize(sr, typeof(AgsPolyline));
                        break;
                    default:
                        renderer = null;
                        break;
                }
                return renderer;
            }
         
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
