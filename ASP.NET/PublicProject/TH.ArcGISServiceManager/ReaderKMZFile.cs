using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TH.SharpKml.Model;

namespace TH.ArcGISServiceManager
{
   public class ReaderKMZFile
    {
       public static object GetPolygonFromKMZ(string path,ref string name)
       {
            //List<KmlPoint> points = Kml.ReaderKMZ(path, ref name);
            var polys = new PolygonClass();
            //foreach (var item in points)
            //{
            //    var shape = new ESRI.ArcGIS.Geometry.Point();
            //    shape.X = item.B;
            //    shape.Y = item.L;
            //    shape.Z = item.H;
            //    var pt = new PointClass();
            //    polys.AddPoint(shape);
            //}
            return polys;
       }

    }
}
