// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using ArcGIsLib.ApiImports.Geometry;
using ArcGIsLib.ApiImports.JsonConverters;

namespace ArcGIsLib.Deserializers
{

    public class SimpleGeometryResolver
    {
        private SimpleGeometryResolver()
        {
            throw (new NotSupportedException());
        }
        public static AgsGeometryBase ParseGeometry(EsriGeometryType? geoType, string strContent)
        {
            if (string.IsNullOrEmpty(strContent))
            {
                return null;
            }
            if (strContent.StartsWith("{")) //json
            {
                if (geoType == null)
                {
                    throw (new ArgumentNullException("geoType"));
                }
                var settings = new JsonSerializerSettings();
                settings.Converters = new List<JsonConverter>() { new GeomertyConverter() };
                //settings.Context = new StreamingContext(StreamingContextStates.All, geoType);
                var geo = JsonConvert.DeserializeObject<AgsGeometryBase>(strContent, settings);
                geo.SpatialReference.WKID = 0;
                return geo;
            }
            var parts = strContent.Split(',');
            if (parts.Length == 2) //point
            {
                var x = double.Parse(parts[0]);
                var y = double.Parse(parts[1]);
                AgsGeometryBase pt = new AgsPoint { X = x, Y = y };
                return pt;
            }
            else if (parts.Length == 4) //envelope
            {
                var xmin = double.Parse(parts[0]);
                var ymin = double.Parse(parts[1]);
                var xmax = double.Parse(parts[2]);
                var ymax = double.Parse(parts[3]);
                AgsGeometryBase env = new AgsEnvelope { XMin = xmin, YMin = ymin, XMax = xmax, YMax = ymax };
                return env;
            }
            else
            {
                return null;
            }
        }
    }
}