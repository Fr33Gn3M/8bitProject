using System.Runtime.Serialization;
using Microsoft.SqlServer.Types;
namespace Sys.DataBase
{
    [DataContract]
    [KnownType(typeof(THArcGeometry))]
    public class THGeometry
    {
        public THGeometry()
        {
            SRID = 4490;
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
            {
                var geo = value as THGeometry;
                return geo;
            }
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
