using ArcGIsLib.ApiImports.Geometry;
using ArcGIsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ArcGIsLib.Deserializers
{
    public class GeometryToWKT
    {
        #region Methods

        /// <summary>
        /// Converts a Geometry to its Well-known Text representation.
        /// </summary>
        /// <param name="geometry">A Geometry to write.</param>
        /// <returns>A &lt;Geometry Tagged Text&gt; string (see the OpenGIS Simple
        ///  Features Specification)</returns>
        public static string Write(AgsGeometryBase geometry)
        {
            StringWriter sw = new StringWriter();
            Write(geometry, sw);
            return sw.ToString();
        }

        /// <summary>
        /// Converts a Geometry to its Well-known Text representation.
        /// </summary>
        /// <param name="geometry">A geometry to process.</param>
        /// <param name="writer">Stream to write out the geometry's text representation.</param>
        /// <remarks>
        /// Geometry is written to the output stream as &lt;Gemoetry Tagged Text&gt; string (see the OpenGIS
        /// Simple Features Specification).
        /// </remarks>
        public static void Write(AgsGeometryBase geometry, StringWriter writer)
        {
            AppendGeometryTaggedText(geometry, writer);
        }

        /// <summary>
        /// Converts a Geometry to &lt;Geometry Tagged Text &gt; format, then Appends it to the writer.
        /// </summary>
        /// <param name="geometry">The Geometry to process.</param>
        /// <param name="writer">The output stream to Append to.</param>
        private static void AppendGeometryTaggedText(AgsGeometryBase geometry, StringWriter writer)
        {
            if (geometry == null)
                throw new NullReferenceException("Cannot write Well-Known Text: geometry was null");

            if (geometry is AgsPoint)
            {
                var point = geometry as AgsPoint;
                AppendPointTaggedText(new double[] { point.X, point.Y }, writer);
            }
            else if (geometry is AgsPolyline && ((AgsPolyline)geometry).Paths.Count() == 1)
                AppendLineStringTaggedText(geometry as AgsPolyline, writer);
            else if (geometry is AgsEnvelope)
                AppendEnvelope(geometry as AgsEnvelope, writer);
            else if (geometry is AgsPolygon && OnlyOneExteriorRing((AgsPolygon)geometry))
                AppendPolygonTaggedText(geometry as AgsPolygon, writer);
            else if (geometry is AgsMultipoint)
                AppendMultiPointTaggedText(geometry as AgsMultipoint, writer);
            else if (geometry is AgsPolyline)
                AppendMultiLineStringTaggedText(geometry as AgsPolyline, writer);
            else if (geometry is AgsPolygon)
                AppendMultiPolygonTaggedText(geometry as AgsPolygon, writer);
            //else if (geometry is GeometryCollection)
            //	AppendGeometryCollectionTaggedText(geometry as List<Geometry>, writer);
            else
                throw new NotSupportedException("Unsupported Geometry implementation:" + geometry.GetType().Name);
        }

        /// <summary>
        /// Checks whether the first ring is CW or CCW, and returns true if there is only one ring in this direction.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private static bool OnlyOneExteriorRing(AgsPolygon polygon)
        {
            bool exteriorCCW = false;
            if (polygon.Rings.Length > 0)
            {
                exteriorCCW = Algorithms.IsCCW(polygon.Rings[0]);
            }
            int count = 0;
            foreach (var ring in polygon.Rings)
            {
                if (Algorithms.IsCCW(ring) == exteriorCCW)
                    count++;
            }

            return count == 1;
        }

        private static void AppendEnvelope(AgsEnvelope envelope, StringWriter writer)
        {
            writer.Write(string.Format(CultureInfo.InvariantCulture, "POLYGON(({0} {1}, {2} {1}, {2} {3}, {0} {3}, {0} {1}))", envelope.XMin, envelope.YMin, envelope.XMax, envelope.YMax));
        }

        /// <summary>
        /// Converts a Coordinate to &lt;Point Tagged Text&gt; format,
        /// then Appends it to the writer.
        /// </summary>
        /// <param name="coordinate">the <code>Coordinate</code> to process</param>
        /// <param name="writer">the output writer to Append to</param>
        private static void AppendPointTaggedText(double[] coordinate, StringWriter writer)
        {
            writer.Write("POINT ");
            AppendPointText(coordinate, writer);
        }

        /// <summary>
        /// Converts a LineString to LineString tagged text format, 
        /// </summary>
        /// <param name="lineString">The LineString to process.</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendLineStringTaggedText(AgsPolyline lineString, StringWriter writer)
        {
            writer.Write("LINESTRING ");
            AppendLineStringText(lineString.Paths[0], writer);
        }

        /// <summary>
        ///  Converts a Polygon to &lt;Polygon Tagged Text&gt; format,
        ///  then Appends it to the writer.
        /// </summary>
        /// <param name="polygon">Th Polygon to process.</param>
        /// <param name="writer">The stream writer to Append to.</param>
        private static void AppendPolygonTaggedText(AgsPolygon polygon, StringWriter writer)
        {
            writer.Write("POLYGON ");
            AppendPolygonText(polygon, writer);
        }

        /// <summary>
        /// Converts a MultiPoint to &lt;MultiPoint Tagged Text&gt;
        /// format, then Appends it to the writer.
        /// </summary>
        /// <param name="multipoint">The MultiPoint to process.</param>
        /// <param name="writer">The output writer to Append to.</param>
        private static void AppendMultiPointTaggedText(AgsMultipoint multipoint, StringWriter writer)
        {
            writer.Write("MULTIPOINT ");
            AppendMultiPointText(multipoint, writer);
        }

        /// <summary>
        /// Converts a MultiLineString to &lt;MultiLineString Tagged
        /// Text&gt; format, then Appends it to the writer.
        /// </summary>
        /// <param name="multiLineString">The MultiLineString to process</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendMultiLineStringTaggedText(AgsPolyline multiLineString, StringWriter writer)
        {
            writer.Write("MULTILINESTRING ");
            AppendMultiLineStringText(multiLineString, writer);
        }

        /// <summary>
        /// Converts a MultiPolygon to &lt;MultiPolygon Tagged
        /// Text&gt; format, then Appends it to the writer.
        /// </summary>
        /// <param name="multiPolygon">The MultiPolygon to process</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendMultiPolygonTaggedText(AgsPolygon multiPolygon, StringWriter writer)
        {
            writer.Write("MULTIPOLYGON ");
            AppendMultiPolygonText(multiPolygon, writer);
        }

        /// <summary>
        /// Converts a GeometryCollection to &lt;GeometryCollection Tagged
        /// Text&gt; format, then Appends it to the writer.
        /// </summary>
        /// <param name="geometryCollection">The GeometryCollection to process</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendGeometryCollectionTaggedText(List<AgsGeometryBase> geometryCollection,
                                                                                 StringWriter writer)
        {
            writer.Write("GEOMETRYCOLLECTION ");
            AppendGeometryCollectionText(geometryCollection, writer);
        }


        /// <summary>
        /// Converts a Coordinate to Point Text format then Appends it to the writer.
        /// </summary>
        /// <param name="coordinate">The Coordinate to process.</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendPointText(double[] coordinate, StringWriter writer)
        {
            if (coordinate == null)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                AppendCoordinate(coordinate, writer);
                writer.Write(")");
            }
        }

        /// <summary>
        /// Converts a Coordinate to &lt;Point&gt; format, then Appends
        /// it to the writer. 
        /// </summary>
        /// <param name="coordinate">The Coordinate to process.</param>
        /// <param name="writer">The output writer to Append to.</param>
        private static void AppendCoordinate(double[] coordinate, StringWriter writer)
        {
            writer.Write(WriteNumber(coordinate[0]) + " " + WriteNumber(coordinate[1]));
            //for (uint i = 0; i < 2; i++) writer.Write(WriteNumber(coordinate[i]) + (i < 2 - 1 ? " " : ""));
        }

        /// <summary>
        /// Converts a double to a string, not in scientific notation.
        /// </summary>
        /// <param name="d">The double to convert.</param>
        /// <returns>The double as a string, not in scientific notation.</returns>
        private static string WriteNumber(double d)
        {
            // string str = d.ToString("f10");
            //str = Math.Round(d,10).ToString();
            // return str;
            return d.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts a LineString to &lt;LineString Text&gt; format, then
        /// Appends it to the writer.
        /// </summary>
        /// <param name="lineString">The LineString to process.</param>
        /// <param name="writer">The output stream to Append to.</param>
        private static void AppendLineStringText(double[][] lineString, StringWriter writer)
        {
            if (lineString == null || lineString.Length == 0)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < lineString.Length; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendCoordinate(lineString[i], writer);
                }
                writer.Write(")");
            }
        }

        /// <summary>
        /// Converts a Polygon to &lt;Polygon Text&gt; format, then
        /// Appends it to the writer.
        /// </summary>
        /// <param name="polygon">The Polygon to process.</param>
        /// <param name="writer"></param>
        private static void AppendPolygonText(AgsPolygon polygon, StringWriter writer)
        {
            if (polygon == null || polygon.Rings.Length == 0)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                AppendLineStringText(polygon.Rings[0], writer); //ExteriorRing
                for (int i = 1; i < polygon.Rings.Length; i++)
                {
                    writer.Write(", ");
                    AppendLineStringText(polygon.Rings[i], writer); //InteriorRings
                }
                writer.Write(")");
            }
        }

        /// <summary>
        /// Converts a MultiPoint to &lt;MultiPoint Text&gt; format, then
        /// Appends it to the writer.
        /// </summary>
        /// <param name="multiPoint">The MultiPoint to process.</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendMultiPointText(AgsMultipoint multiPoint, StringWriter writer)
        {
            if (multiPoint == null || multiPoint.Points.Length == 0)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < multiPoint.Points.Length; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendCoordinate(multiPoint.Points[i], writer);
                }
                writer.Write(")");
            }
        }

        /// <summary>
        /// Converts a MultiLineString to &lt;MultiLineString Text&gt;
        /// format, then Appends it to the writer.
        /// </summary>
        /// <param name="multiLineString">The MultiLineString to process.</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendMultiLineStringText(AgsPolyline multiLineString, StringWriter writer)
        {
            if (multiLineString == null || multiLineString.Paths.Length == 0)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < multiLineString.Paths.Length; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendLineStringText(multiLineString.Paths[i], writer);
                }
                writer.Write(")");
            }
        }

        /// <summary>
        /// Converts a MultiPolygon to &lt;MultiPolygon Text&gt; format, then Appends to it to the writer.
        /// </summary>
        /// <param name="multiPolygon">The MultiPolygon to process.</param>
        /// <param name="writer">The output stream to Append to.</param>
        private static void AppendMultiPolygonText(AgsPolygon multiPolygon, StringWriter writer)
        {
            if (multiPolygon == null || multiPolygon.Rings.Length == 0)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");

                bool outerRing = true;
                if (multiPolygon.Rings.Length > 0)
                {
                    outerRing = Algorithms.IsCCW(multiPolygon.Rings[0]);
                }
                for (int i = 0; i < multiPolygon.Rings.Length; i++)
                {
                    if (i > 0) writer.Write(", ");

                    var singlePolygon = new AgsPolygon();
                    var singlePolygonRings = new List<double[][]>();
                    singlePolygonRings.Add(multiPolygon.Rings[i]); // Add the outer ring

                    //Add any interior rings
                    for (int j = i + 1; j < multiPolygon.Rings.Length; j++)
                    {
                        // It is an interior ring if the clockwise direction is opposite of the first ring
                        if (Algorithms.IsCCW(multiPolygon.Rings[j]) == outerRing) break;
                        singlePolygonRings.Add(multiPolygon.Rings[j]);
                        i++;
                    }
                    singlePolygon.Rings = singlePolygonRings.ToArray();
                    AppendPolygonText(singlePolygon, writer);
                }
                writer.Write(")");
            }
        }

        /// <summary>
        /// Converts a GeometryCollection to &lt;GeometryCollection Text &gt; format, then Appends it to the writer.
        /// </summary>
        /// <param name="geometryCollection">The GeometryCollection to process.</param>
        /// <param name="writer">The output stream writer to Append to.</param>
        private static void AppendGeometryCollectionText(List<AgsGeometryBase> geometryCollection, StringWriter writer)
        {
            if (geometryCollection == null || geometryCollection.Count == 0)
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < geometryCollection.Count; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendGeometryTaggedText(geometryCollection[i], writer);
                }
                writer.Write(")");
            }
        }

        #endregion
    }
}
