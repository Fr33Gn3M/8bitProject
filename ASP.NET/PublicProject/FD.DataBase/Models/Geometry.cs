using Microsoft.SqlServer.Types;
using System.Runtime.Serialization;
namespace FD.DataBase
{
    [DataContract]
    [KnownType(typeof(FDArcGeometry))]
    public class FDGeometry
    {
        public FDGeometry()
        {
            SRID = 0;
        }
        [DataMember]
        public string WKT { get; set; }
        [DataMember]
        public int SRID { get; set; }
        public static FDGeometry ToFDGeometry(object value)
        {
            if (value.GetType() == typeof(SqlGeometry))
            {
                var geom = value as SqlGeometry;
                if (geom.IsNull == true)
                    return null;
                var geo = new FDGeometry() { WKT = geom.ToString(), SRID = geom.STSrid.Value };
                return geo;
            }
            else
                return value as FDGeometry;
        }

        public static FDGeometry ToFDGeometry(string wkt, int srid)
        {
            var geo = new FDGeometry() { WKT = wkt, SRID = srid };
            return geo;
        }
    }

    [DataContract]
    public class FDArcGeometry : FDGeometry
    {
        [DataMember]
        public double[] ArcAngles { get; set; }
    }

    public enum FDGeometryType
    {
        Null,
        Point,
        Polyline,
        Polygon
    }

}
