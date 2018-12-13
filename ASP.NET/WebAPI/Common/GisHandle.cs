using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using DAL;
using ArcGIsLib;
using ArcGIsLib.ApiImports.Geometry;
using ArcGIsLib.Deserializers;
using ArcGIsLib.ApiImports.JsonConverters;

namespace Common
{
    public class GisHandle
    {
        public static void GetGeometres(Dictionary<string, object>[] dic)
        {
            if (dic == null || dic.Length == 0 || !dic[0].ContainsKey("SHAPE"))
                return;
            int i = 0;
            try
            {
                foreach (var item in dic)
                {
                    if (i == 243)
                    {
                    }
                    var geom = THGeometry.ToTHGeometry(item["SHAPE"]);
                    if (geom == null || geom.WKT == "Null")
                    {
                        item["SHAPE"] = null;
                        continue;
                    }
                    var geomtry = GeometryFromWKT.Parse(geom.WKT);
                    geomtry.SpatialReference = new AgsSpatialReference() { WKID = 0 };
                    geomtry.GeoType = GetEsriGeometryStr(geomtry);
                    var settings = new JsonSerializerSettings();
                    settings.Converters = new List<JsonConverter>() { new GeomertyConverter() };
                    var geo = JsonConvert.SerializeObject(geomtry, settings);
                    item["SHAPE"] = geomtry;
                    i++;
                }
            }
            catch (Exception)
            {
            }
        }


        public static void UpdateGeometry(Dictionary<string, object>[] dic)
        {
            if (dic == null || dic.Length == 0 || !dic[0].ContainsKey("SHAPE"))
                return;
            foreach (var item in dic)
            {
                if (item["SHAPE"] == null)
                    continue;
                var geom = GetAgsGeometry(item["SHAPE"].ToString());
                var wktItem = GeometryToWKT.Write(geom);
                var g = THGeometry.ToTHGeometry(wktItem, geom.SpatialReference.WKID);
                item["SHAPE"] = g;
            }
            
        }
        public static AgsGeometryBase GetAgsGeometry(string strGeometryParams)
        {
            var rendererJson = JsonConvert.DeserializeObject<JObject>(strGeometryParams);
            var type = rendererJson["type"].ToString();
            var esriType = GetEsriGeometry(type);
            var geom = SimpleGeometryResolver.ParseGeometry(esriType, strGeometryParams);
            return geom;
        }
        private static string GetEsriGeometryStr(AgsGeometryBase agsGeom)
        {
            var type = agsGeom.GetType().Name;
            var esriType = "polygon";
            switch (type)
            {
                case "AgsPolygon":
                    esriType = "polygon";
                    break;
                case "AgsPolyline":
                    esriType = "polyline";
                    break;
                case "AgsPoint":
                    esriType = "point";
                    break;
            }
            return esriType;
        }
        private static EsriGeometryType GetEsriGeometry(string type)
        {
            var esriType = EsriGeometryType.esriGeometryPolygon;
            switch (type)
            {
                case "polygon":
                    esriType = EsriGeometryType.esriGeometryPolygon;
                    break;
                case "polyline":
                    esriType = EsriGeometryType.esriGeometryPolyline;
                    break;
                case "point":
                    esriType = EsriGeometryType.esriGeometryPoint;
                    break;
            }
            return esriType;
        }
    }
}
