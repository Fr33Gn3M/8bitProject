using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.SqlServer.Types;
namespace DAL
{
    [DataContract]
    [KnownType(typeof(THArcGeometry))]
    public class THGeometry
    {
        public THGeometry()
        {
            SRID = 0;
        }
        [DataMember]
        public string WKT { get; set; }
        [DataMember]
        public int SRID { get; set; }
        public static THGeometry ToTHGeometry(object value)
        {
            if (value.GetType() == typeof(SqlGeometry))
            {
                var geom = value as SqlGeometry;
                if (geom.IsNull == true)
                    return null;
                var geo = new THGeometry() { WKT = geom.ToString(), SRID = geom.STSrid.Value };
                return geo;
            }
            else
                return value as THGeometry;
        }

        public static THGeometry ToTHGeometry(string wkt, int srid)
        {
            var geo = new THGeometry() { WKT = wkt, SRID = srid };
            return geo;
        }
    }

    [DataContract]
    public class THArcGeometry : THGeometry
    {
        [DataMember]
        public double[] ArcAngles { get; set; }
    }

    public enum THGeometryType
    {
        Null,
        Point,
        Polyline,
        Polygon
    }

}
