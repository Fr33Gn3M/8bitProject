using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ArcGISServiceManager
{
    public class CoordTransferHelper
    {

        public static void GetPointXY(object geo,ref double x,ref double y)
        {
            x = (geo as Point).X;
            y = (geo as Point).Y;
        }

        public static object GetPoint(double x, double y)
        {
            var shape = new ESRI.ArcGIS.Geometry.Point();
            shape.X = x;
            shape.Y = y;
            shape.Z = 0;
            return shape;
        }

        public static object ConvectGeometry(object geo, string fromCoordSystemType, string toCoordSystemType, bool FromCoordTramIsJW = false)
        {
            object geom = null;
            //var tt = GetCopyGeometry(geo);
            var systemType = (CoordTransfer.CoordinateSystemType)Enum.Parse(typeof(CoordTransfer.CoordinateSystemType), fromCoordSystemType);
            var systemType1 = (CoordTransfer.CoordinateSystemType)Enum.Parse(typeof(CoordTransfer.CoordinateSystemType), toCoordSystemType);
            geom = ConvectGeometry(geo as IGeometry, systemType, systemType1, FromCoordTramIsJW);
            return geom;
        }

        public static bool ConvectGeometry(out object geom, object geo, string fromCoordSystemType, string toCoordSystemType, bool FromCoordTramIsJW = false)
        {
            //var tt = GetCopyGeometry(geo);
            var systemType = (CoordTransfer.CoordinateSystemType)Enum.Parse(typeof(CoordTransfer.CoordinateSystemType), fromCoordSystemType);
            var systemType1 = (CoordTransfer.CoordinateSystemType)Enum.Parse(typeof(CoordTransfer.CoordinateSystemType), toCoordSystemType);
            geom = ConvectGeometry(geo as IGeometry, systemType, systemType1, FromCoordTramIsJW);
            if (geom == null)
                return false;
            return true;
        }

        public static IGeometry GetCopyGeometry(object obj)
        {
            var t = obj as IGeometry;
            if (t == null)
                return null;
            return t;
        }

        internal static IGeometry ConvectGeometry(IGeometry geo, CoordTransfer.CoordinateSystemType fromCoordSystemType, CoordTransfer.CoordinateSystemType toCoordSystemType, bool FromCoordTramIsJW = false)
        {
            IGeometry geom = null;
            var coord = new CoordTransfer.ReCoordTrans(fromCoordSystemType, toCoordSystemType);
            switch (geo.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    {
                        var obj = geo as Point;
                        var Ptt = ConvectPoint(coord, obj, FromCoordTramIsJW);
                        geom = Ptt;
                        break;
                    }
                case esriGeometryType.esriGeometryPolyline:
                    {
                        var obj = (IPolyline)geo;
                        geom = ConvectPolyline(coord, obj, FromCoordTramIsJW) as IGeometry;
                        break;
                    }
                case esriGeometryType.esriGeometryMultipoint:
                    {
                        var obj = geo as Multipoint;
                        geom = ConvectMultipoint(coord, obj, FromCoordTramIsJW) as IGeometry;
                        break;
                    }
                case esriGeometryType.esriGeometryPolygon:
                    {
                        var obj = geo as IPolygon;
                        geom = ConvectPolygon(coord, obj, FromCoordTramIsJW) as IGeometry;
                        break;
                    }
            }
            return geom;
        }

        internal static IPolyline ConvectPolyline(CoordTransfer.ReCoordTrans coord, IPolyline pPolyline, bool FromCoordTramIsJW = false)
        {
            bool isadd = false;
            IGeometryCollection pPolygonGeoCol = new PolylineClass();
            PolylineClass pPolygonGeoCol2 = new PolylineClass();
            object missing = Type.Missing;
            if ((pPolyline != null) && (!pPolyline.IsEmpty))
            {
                IGeometryCollection pPolylineGeoCol = pPolyline as IGeometryCollection;
                for (int i = 0; i < pPolylineGeoCol.GeometryCount; i++)
                {
                    ISegmentCollection pSegCol = new PathClass();
                    ISegmentCollection pPolylineSegCol = pPolylineGeoCol.get_Geometry(i) as ISegmentCollection;
                    var obj = pPolylineGeoCol.get_Geometry(i);
                    var type = obj.GeometryType.ToString();
                    for (int j = 0; j < pPolylineSegCol.SegmentCount; j++)
                    {
                        ISegment pSegment = null;
                        pSegment = pPolylineSegCol.get_Segment(j);
                        ISegment segment = null;
                        switch (pSegment.GeometryType)
                        {
                            case esriGeometryType.esriGeometryLine:
                                {
                                    var pSet = pSegment as Line;
                                    var line = new LineClass();
                                    var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                    var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                    line.PutCoords(fromPt, toPt);
                                    segment = line as ISegment;
                                }
                                break;
                            case esriGeometryType.esriGeometryCircularArc:
                                {
                                    var pSet = pSegment as CircularArc;
                                    if (pSet.IsLine == true)
                                    {
                                        var line = new LineClass();
                                        var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                        var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                        line.PutCoords(fromPt, toPt);
                                        segment = line as ISegment;
                                    }
                                    else
                                    {
                                        var circular = new CircularArcClass();
                                        var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                        var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                        var center = ConvectPoint(coord, pSet.CenterPoint as Point, FromCoordTramIsJW);
                                        var arcCounterClorckwise = esriArcOrientation.esriArcClockwise;
                                        if (pSet.IsMinor == true)
                                            arcCounterClorckwise = esriArcOrientation.esriArcMinor;
                                        if (pSet.IsCounterClockwise == true)
                                            arcCounterClorckwise = esriArcOrientation.esriArcCounterClockwise;
                                        else
                                            arcCounterClorckwise = esriArcOrientation.esriArcClockwise;
                                        circular.PutCoords(center, fromPt, toPt, arcCounterClorckwise);
                                        segment = circular as ISegment;
                                        isadd = true;
                                    }
                                }
                                break;
                            case esriGeometryType.esriGeometryEllipticArc:
                                {
                                    var pSet = pSegment as EllipticArc;
                                    double semiMajor = 0, semiMinor = 0, minorMajorRatio = 0, rotationAngle=0;
                                    pSet.GetAxes(ref  semiMajor, ref  semiMinor, ref minorMajorRatio);
                                    bool isCCW = false, minor = false;
                                    pSet.QueryCoords(false, pSet.CenterPoint, pSet.FromPoint, pSet.ToPoint, ref  rotationAngle, ref  minorMajorRatio, ref  isCCW, ref  minor);
                                    var circular = new EllipticArc();
                                    var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                    var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                    var center = ConvectPoint(coord, pSet.CenterPoint as Point, FromCoordTramIsJW);
                                    var arcCounterClorckwise = esriArcOrientation.esriArcClockwise;
                                    if (pSet.IsMinor == true)
                                        arcCounterClorckwise = esriArcOrientation.esriArcMinor;
                                    if (pSet.IsCounterClockwise == true)
                                        arcCounterClorckwise = esriArcOrientation.esriArcCounterClockwise;
                                    else
                                        arcCounterClorckwise = esriArcOrientation.esriArcClockwise;
                                    circular.PutCoords(false, center, fromPt, toPt, rotationAngle, minorMajorRatio, arcCounterClorckwise);
                                    segment = circular as ISegment;
                                    isadd = true;
                                }
                                break;
                            default:
                                break;

                        }
                        if (segment != null)
                            pSegCol.AddSegment(segment, ref missing, ref missing);
                    }
                    pPolygonGeoCol.AddGeometry(pSegCol as IGeometry, ref missing, ref missing);
                    pPolygonGeoCol2.AddGeometry(pSegCol as IGeometry, ref missing, ref missing);
                    pPolygonGeoCol2.Simplify();
                    if (pPolygonGeoCol2.GeometryCount == 0)
                        return null;
                    //if (isadd == false)
                    //    return null;
                }
            }
            return pPolygonGeoCol as IPolyline;
        }

        public static double Distance(Point p1, Point p2, bool abs=true)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            if (abs)
            {
                dx = Math.Abs(dx);
                dy = Math.Abs(dy);
            }
            if (dx == 0)
                return dy;
            if (dy == 0)
                return dx;
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }


        internal static IPolygon ConvectPolygon(CoordTransfer.ReCoordTrans coord, IPolygon pPolyline, bool FromCoordTramIsJW = false)
        {
            IGeometryCollection pPolygonGeoCol = new PolygonClass();
            PolygonClass pPolygonGeoCol2 = new PolygonClass();
            object missing = Type.Missing;
            if ((pPolyline != null) && (!pPolyline.IsEmpty))
            {
                IGeometryCollection pPolylineGeoCol = pPolyline as IGeometryCollection;
                for (int i = 0; i < pPolylineGeoCol.GeometryCount; i++)
                {
                    ISegmentCollection pSegCol = new RingClass();
                    ISegmentCollection pPolylineSegCol = pPolylineGeoCol.get_Geometry(i) as ISegmentCollection;
                    var obj = pPolylineGeoCol.get_Geometry(i);
                    var type = obj.GeometryType.ToString();
                    for (int j = 0; j < pPolylineSegCol.SegmentCount; j++)
                    {
                        ISegment pSegment = null;
                        pSegment = pPolylineSegCol.get_Segment(j);
                        ISegment segment = null;
                        switch (pSegment.GeometryType)
                        {
                            case esriGeometryType.esriGeometryLine:
                                {
                                    var pSet = pSegment as Line;
                                    var line = new LineClass();
                                    var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                    var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                    line.PutCoords(fromPt, toPt);
                                    segment = line as ISegment;
                                }
                                break;
                            case esriGeometryType.esriGeometryCircularArc:
                                {
                                    var pSet = pSegment as CircularArc;
                                    if (pSet.IsLine == true)
                                    {
                                        var line = new LineClass();
                                        var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                        var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                        line.PutCoords(fromPt, toPt);
                                        segment = line as ISegment;
                                    }
                                    else
                                    {
                                        var circular = new CircularArcClass();
                                        var fromPt = ConvectPoint(coord, pSet.FromPoint as Point, FromCoordTramIsJW);
                                        var toPt = ConvectPoint(coord, pSet.ToPoint as Point, FromCoordTramIsJW);
                                        var center = ConvectPoint(coord, pSet.CenterPoint as Point, FromCoordTramIsJW);
                                        var arcCounterClorckwise = esriArcOrientation.esriArcClockwise;
                                        if (pSet.IsMinor == true)
                                            arcCounterClorckwise = esriArcOrientation.esriArcMinor;
                                        if (pSet.IsCounterClockwise == true)
                                            arcCounterClorckwise = esriArcOrientation.esriArcCounterClockwise;
                                        else
                                            arcCounterClorckwise = esriArcOrientation.esriArcClockwise;
                              
                                        circular.PutCoords(center, fromPt, toPt, arcCounterClorckwise);
                                        segment = circular as ISegment;
                                    }
                                }
                                break;
                            default:
                                break;

                        }
                        pSegCol.AddSegment(segment, ref missing, ref missing);
                    }
                    pPolygonGeoCol.AddGeometry(pSegCol as IGeometry, ref missing, ref missing);
                    pPolygonGeoCol2.AddGeometry(pSegCol as IGeometry, ref missing, ref missing);
                    pPolygonGeoCol2.Simplify();
                    if (pPolygonGeoCol2.GeometryCount == 0)
                        return null;
                }
            }
            return pPolygonGeoCol as IPolygon;
        }

        private static Point ConvectPoint(CoordTransfer.ReCoordTrans coord, Point pt, bool FromCoordTramIsJW = false)
        {
            var point1 = pt;
            if (FromCoordTramIsJW == true)
                point1 = lonLat2WebMercator(pt);
            var point = coord.TransCoord(new CoordTransfer.Point(point1.X, point1.Y, 0));
            var shape = new ESRI.ArcGIS.Geometry.Point();
            shape.X = point.X;
            shape.Y = point.Y;
            shape.Z = point.Z;
            return shape;
        }

        private static Multipoint ConvectMultipoint(CoordTransfer.ReCoordTrans coord, Multipoint pl, bool FromCoordTramIsJW = false)
        {
            Multipoint line = new Multipoint();
            for (int i = 0; i < pl.PointCount; i++)
            {
                var pt = pl.get_Point(i) as Point;
                var point = ConvectPoint(coord, pt, FromCoordTramIsJW);
                line.AddPoint(point);
            }
            return line;
        }


        private static Point lonLat2WebMercator(Point lonLat)
        {
            double x = lonLat.X * 20037508.34 / 180;
            double y = Math.Log(Math.Tan((90 + lonLat.Y) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
            Point mercator = new Point();
            mercator.X = x;
            mercator.Y = y;
            return mercator;
        }

        ////Web墨卡托转经纬度
        //private static Point WebMercator2lonLat(Point mercator)
        //{
        //    double x = mercator.X / 20037508.34 * 180;
        //    double y = mercator.Y / 20037508.34 * 180;
        //    y = 180 / Math.PI * (2 * Math.Atan(Math.Exp(y * Math.PI / 180)) - Math.PI / 2);
        //    Point lonLat = new Point(x, y);
        //    return lonLat;
        //}


    }
}
