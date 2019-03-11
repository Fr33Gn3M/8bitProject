// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.ApiImports.GeometryService;
using TH.ArcGISRest;


namespace TH.ArcGISRest.Client
{
	public interface IGeometryServiceClient : ICommunicationObject
	{
		AgsGeometryCollection ProjectGeometries(AgsGeometryCollection geometries, AgsSpatialReference inSR, AgsSpatialReference outSR);
		AgsGeometryCollection SimplifyGeometries(AgsGeometryCollection geometries, AgsSpatialReference sr);
        AgsGeometryCollection BufferGeometries(AgsGeometryCollection geometries, AgsSpatialReference inSR, AgsSpatialReference outSR, AgsSpatialReference bufferSR, double distance, EsriSRUnitType? unit, bool unionResults, bool geodesic);
		AreasAndLengthsResponse AreasAndLengths(AgsPolygon[] polygons, AgsSpatialReference sr, EsriSRUnitType? lengthUnit, object areaUnit);
		LengthsResponse Lengths(AgsPolyline[] polylines, AgsSpatialReference sr, EsriSRUnitType? lengthUnit, bool geodesic);
		RelationsResponse Relation(AgsGeometryCollection geometries1, AgsGeometryCollection geometries2, AgsSpatialReference sr, EsriSpatialRelationship srRelation, string relationParam);
		LabelPointsResponse LabelPoints(AgsPolygon[] polygons, AgsSpatialReference sr);
		DistanceResponse Distance(AgsTypedGeometry geometry1, AgsTypedGeometry geometry2, AgsSpatialReference sr, EsriSRUnitType? distanceUnit, bool geodesic);
		AgsGeometryCollection Densify(AgsGeometryCollection geometries, AgsSpatialReference sr, double maxSegmentLength, bool geodesic, EsriSRUnitType? lengthUnit);
		AgsGeometryCollection Generalize(AgsGeometryCollection geometries, AgsSpatialReference sr, double maxDeviation, EsriSRUnitType? deviationUnit);
		AgsTypedGeometry ConvexHull(AgsGeometryCollection geometries, AgsSpatialReference sr);
		AgsGeometryCollection Offset(AgsGeometryCollection geometries, AgsSpatialReference sr, double offsetDistance, EsriSRUnitType? offsetUnit, EsriOffsetHow offsetHow, double? bevelRatio, bool simplifyResult);
		AgsGeometryCollection TrimExtend(AgsPolyline[] polylines, AgsPolyline trimExtendTo, AgsSpatialReference sr, int extendHow);
		AgsGeometryCollection AutoComplete(AgsPolygon[] polygons, AgsPolyline[] polylines, AgsSpatialReference sr);
		CutResponse Cut(AgsPolyline cutter, AgsGeometryCollection target, AgsSpatialReference sr);
		AgsGeometryCollection Difference(AgsGeometryCollection geometries, AgsTypedGeometry geometry, AgsSpatialReference sr);
		AgsGeometryCollection Intersect(AgsGeometryCollection geometries, AgsTypedGeometry geometry, AgsSpatialReference sr);
		AgsTypedGeometry Reshape(AgsTypedGeometry target, AgsPolyline reshaper, AgsSpatialReference sr);
		AgsTypedGeometry Union(AgsGeometryCollection geometries, AgsSpatialReference sr);
	}
	
}
