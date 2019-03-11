// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.GeometryService;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.ApiImports.Geometry;
using PH.ServerFramework.WebClientPoint;
using TH.ArcGISRest;


namespace TH.ArcGISRest.Client
{
	internal class GeometryServiceClient : IGeometryServiceClient
	{
		
		private readonly Uri _baseUrl;
		private readonly string _profileName;
		
		public GeometryServiceClient(Uri baseUrl, string profileName = null)
		{
			_profileName = profileName;
			_baseUrl = baseUrl;
		}
		
		private string GetProfileName()
		{
			return _profileName;
		}
		
		public AreasAndLengthsResponse AreasAndLengths(AgsPolygon[] polygons, AgsSpatialReference sr, EsriSRUnitType? lengthUnit, object areaUnit)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(AreasAndLengthsParameterNames.Polygons, polygons);
				if (sr != null)
				{
					restClient.Params.Add(AreasAndLengthsParameterNames.Sr, sr);
				}
				if (lengthUnit != null)
				{
					restClient.Params.Add(AreasAndLengthsParameterNames.LengthUnit, lengthUnit.ToString());
				}
				if (areaUnit != null)
				{
					restClient.Params.Add(AreasAndLengthsParameterNames.AreaUnit, areaUnit.ToString());
				}
				var result = restClient.Request<AreasAndLengthsResponse>(ServiceResources.AreasAndLengths);
				return result;
			}
			
		}
		
		public AgsGeometryCollection AutoComplete(AgsPolygon[] polygons, AgsPolyline[] polylines, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(AutoCompleteParameterNames.Polygons, polygons);
				restClient.Params.Add(AutoCompleteParameterNames.Polylines, polylines);
				if (sr != null)
				{
					restClient.Params.Add(AutoCompleteParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.AutoComplete);
				return result;
			}
			
		}
		
		public AgsGeometryCollection BufferGeometries(AgsGeometryCollection geometries, AgsSpatialReference inSR, AgsSpatialReference outSR, AgsSpatialReference bufferSR, double distance, EsriSRUnitType? unit, bool unionResults,bool geodesic)
		{
            using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
            {
                restClient.Params.Add(BufferParameterNames.Geodesic, geodesic);
                restClient.Params.Add(BufferParameterNames.UnionResults, unionResults);
                restClient.Params.Add(BufferParameterNames.Geometries, geometries);
                restClient.Params.Add(BufferParameterNames.Distances, distance);
                if (inSR != null)
                {
                    restClient.Params.Add(BufferParameterNames.InSR, inSR);
                }
                if (outSR != null)
                {
                    restClient.Params.Add(BufferParameterNames.OutSR, outSR);
                }
                if (bufferSR != null)
                {
                    restClient.Params.Add(BufferParameterNames.BufferSR, bufferSR);
                }
                if (unit != null)
                    restClient.Params.Add(BufferParameterNames.Unit, (int)unit);

                var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Buffer);
                return result;
            }
			
		}
		
		public AgsTypedGeometry ConvexHull(AgsGeometryCollection geometries, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(GeometryCollectionSrParameterNames.Geometries, geometries);
				if (sr != null)
				{
					restClient.Params.Add(GeometryCollectionSrParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsTypedGeometry>(ServiceResources.ConvexHull);
				return result;
			}
			
		}
		
		public CutResponse Cut(AgsPolyline cutter, AgsGeometryCollection target, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(CutParameterNames.Cutter, cutter);
				restClient.Params.Add(CutParameterNames.Target, target);
				if (sr != null)
				{
					restClient.Params.Add(CutParameterNames.Sr, sr);
				}
				var result = restClient.Request<CutResponse>(ServiceResources.Cut);
				return result;
			}
			
		}
		
		public AgsGeometryCollection Densify(AgsGeometryCollection geometries, AgsSpatialReference sr, double maxSegmentLength, bool geodesic, EsriSRUnitType? lengthUnit)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(DensifyParameterNames.Geometries, geometries);
				restClient.Params.Add(DensifyParameterNames.MaxSegmentLength, maxSegmentLength);
				restClient.Params.Add(DensifyParameterNames.Geodesic, geodesic);
				if (lengthUnit != null)
				{
					restClient.Params.Add(DensifyParameterNames.LengthUnit, lengthUnit.ToString());
				}
				if (sr != null)
				{
					restClient.Params.Add(DensifyParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Densify);
				return result;
			}
			
		}
		
		public AgsGeometryCollection Difference(AgsGeometryCollection geometries, AgsTypedGeometry geometry, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(DiffIntersectParameterNames.Geometries, geometries);
				restClient.Params.Add(DiffIntersectParameterNames.Geometry, geometry);
				if (sr != null)
				{
					restClient.Params.Add(DiffIntersectParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Difference);
				return result;
			}
			
		}
		
		public DistanceResponse Distance(AgsTypedGeometry geometry1, AgsTypedGeometry geometry2, AgsSpatialReference sr, EsriSRUnitType? distanceUnit, bool geodesic)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(DistanceParameterNames.Geometry1, geometry1);
				restClient.Params.Add(DistanceParameterNames.Geometry2, geometry2);
				restClient.Params.Add(DistanceParameterNames.Geodesic, geodesic);
				if (sr != null)
				{
					restClient.Params.Add(DistanceParameterNames.Sr, sr);
				}
				if (distanceUnit != null)
				{
					restClient.Params.Add(DistanceParameterNames.DistanceUnit, distanceUnit.ToString());
				}
				var result = restClient.Request<DistanceResponse>(ServiceResources.Distance);
				return result;
			}
			
		}
		
		public AgsGeometryCollection Generalize(AgsGeometryCollection geometries, AgsSpatialReference sr, double maxDeviation, EsriSRUnitType? deviationUnit)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(GeneralizeParameterNames.Geometries, geometries);
				restClient.Params.Add(GeneralizeParameterNames.MaxDeviation, maxDeviation);
				if (deviationUnit != null)
				{
					restClient.Params.Add(GeneralizeParameterNames.DeviationUnit, deviationUnit.ToString());
				}
				if (sr != null)
				{
					restClient.Params.Add(GeneralizeParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Generalize);
				return result;
			}
			
		}
		
		public AgsGeometryCollection Intersect(AgsGeometryCollection geometries, AgsTypedGeometry geometry, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(DiffIntersectParameterNames.Geometries, geometries);
				restClient.Params.Add(DiffIntersectParameterNames.Geometry, geometry);
				if (sr != null)
				{
					restClient.Params.Add(DiffIntersectParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Intersect);
				return result;
			}
			
		}
		
		public LabelPointsResponse LabelPoints(AgsPolygon[] polygons, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(LabelPointsParameterNames.Polygons, polygons);
				if (sr != null)
				{
					restClient.Params.Add(LabelPointsParameterNames.Sr, sr);
				}
				var result = restClient.Request<LabelPointsResponse>(ServiceResources.LabelPoints);
				return result;
			}
			
		}
		
		public LengthsResponse Lengths(AgsPolyline[] polylines, AgsSpatialReference sr, EsriSRUnitType? lengthUnit, bool geodesic)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(LengthsParameterNames.Polylines, polylines);
				restClient.Params.Add(LengthsParameterNames.Geodesic, geodesic);
				if (sr != null)
				{
					restClient.Params.Add(LengthsParameterNames.Sr, sr);
				}
				if (lengthUnit != null)
				{
					restClient.Params.Add(LengthsParameterNames.LengthUnit, lengthUnit.ToString());
				}
				var result = restClient.Request<LengthsResponse>(ServiceResources.Lengths);
				return result;
			}
			
		}
		
		public AgsGeometryCollection Offset(AgsGeometryCollection geometries, AgsSpatialReference sr, double offsetDistance, EsriSRUnitType? offsetUnit, EsriOffsetHow offsetHow, double? bevelRatio, bool simplifyResult)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(OffsetParameterNames.Geometries, geometries);
				if (sr != null)
				{
					restClient.Params.Add(OffsetParameterNames.Sr, sr);
				}
				restClient.Params.Add(OffsetParameterNames.OffsetDistance, offsetDistance);
				if (offsetUnit != null)
				{
					restClient.Params.Add(OffsetParameterNames.OffsetUnit, offsetUnit.ToString());
				}
				restClient.Params.Add(OffsetParameterNames.OffsetHow, offsetHow);
				restClient.Params.Add(OffsetParameterNames.BevelRatio, bevelRatio);
				restClient.Params.Add(OffsetParameterNames.SimplifyResult, simplifyResult);
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Offset);
				return result;
			}
			
		}
		
		public AgsGeometryCollection ProjectGeometries(AgsGeometryCollection geometries, AgsSpatialReference inSR, AgsSpatialReference outSR)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(ProjectParameterNames.Geometries, geometries);
				restClient.Params.Add(ProjectParameterNames.InSR, inSR);
				restClient.Params.Add(ProjectParameterNames.OutSR, outSR);
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Project);
				return result;
			}
			
		}
		
		public RelationsResponse Relation(AgsGeometryCollection geometries1, AgsGeometryCollection geometries2, AgsSpatialReference sr, EsriSpatialRelationship srRelation, string relationParam)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(RelationParameterNames.Geometries1, geometries1);
				restClient.Params.Add(RelationParameterNames.Geometries2, geometries2);
				if (sr != null)
				{
					restClient.Params.Add(RelationParameterNames.Sr, sr);
				}
				if (srRelation != EsriSpatialRelationship.esriSpatialRelWithin)
				{
					restClient.Params.Add(RelationParameterNames.Relation, srRelation);
				}
				if (!string.IsNullOrEmpty(relationParam))
				{
					restClient.Params.Add(RelationParameterNames.RelationParam, relationParam);
				}
				var result = restClient.Request<RelationsResponse>(ServiceResources.Relation);
				return result;
			}
			
		}
		
		public AgsTypedGeometry Reshape(AgsTypedGeometry target, AgsPolyline reshaper, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(ReshapeParameterNames.Target, target);
				restClient.Params.Add(ReshapeParameterNames.Reshaper, reshaper);
				if (sr != null)
				{
					restClient.Params.Add(ReshapeParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsTypedGeometry>(ServiceResources.Reshape);
				return result;
			}
			
		}
		
		public AgsGeometryCollection SimplifyGeometries(AgsGeometryCollection geometries, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(GeometryCollectionSrParameterNames.Geometries, geometries);
				if (sr != null)
				{
					restClient.Params.Add(GeometryCollectionSrParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.Simplify);
				return result;
			}
			
		}
		
		public AgsGeometryCollection TrimExtend(AgsPolyline[] polylines, AgsPolyline trimExtendTo, AgsSpatialReference sr, int extendHow)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(TrimExtendParameterNames.Polylines, polylines);
				restClient.Params.Add(TrimExtendParameterNames.TrimExtendTo, trimExtendTo);
				if (sr != null)
				{
					restClient.Params.Add(TrimExtendParameterNames.Sr, sr);
				}
				restClient.Params.Add(TrimExtendParameterNames.ExtendHow, extendHow);
				var result = restClient.Request<AgsGeometryCollection>(ServiceResources.TrimExtend);
				return result;
			}
			
		}
		
		public AgsTypedGeometry Union(AgsGeometryCollection geometries, AgsSpatialReference sr)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(GeometryCollectionSrParameterNames.Geometries, geometries);
				if (sr != null)
				{
					restClient.Params.Add(GeometryCollectionSrParameterNames.Sr, sr);
				}
				var result = restClient.Request<AgsTypedGeometry>(ServiceResources.Union);
				return result;
			}
			
		}
		
		public void Close()
		{
		}
		
		public void Open()
		{
		}
		
		public System.Uri Via
		{
			get
			{
				return _baseUrl;
			}
			set
			{
				throw (new NotSupportedException());
			}
		}
		
#region IDisposable Support
		private bool disposedValue; // To detect redundant calls
		
		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}
				
				// TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
				// TODO: set large fields to null.
			}
			this.disposedValue = true;
		}
		
		// TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
		//Protected Overrides Sub Finalize()
		//    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		//    Dispose(False)
		//    MyBase.Finalize()
		//End Sub
		
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
#endregion
		
		private class ServiceResources
		{
			public const string Project = "project";
			public const string Simplify = "simplify";
			public const string Buffer = "buffer";
			public const string AreasAndLengths = "areasAndLengths";
			public const string Lengths = "lengths";
			public const string Relation = "relation";
			public const string LabelPoints = "labelPoints";
			public const string Distance = "distance";
			public const string Densify = "densify";
			public const string Generalize = "generalize";
			public const string ConvexHull = "convexHull";
			public const string Offset = "offset";
			public const string TrimExtend = "trimExtend";
			public const string AutoComplete = "autoComplete";
			public const string Cut = "cut";
			public const string Difference = "difference";
			public const string Intersect = "intersect";
			public const string Reshape = "reshape";
			public const string Union = "union";
		}
		
		private class AreasAndLengthsParameterNames
		{
			public const string Polygons = "polygons";
			public const string Sr = "sr";
			public const string LengthUnit = "lengthUnit";
			public const string AreaUnit = "AreaUnit";
		}
		
		private class AutoCompleteParameterNames
		{
			public const string Polygons = "polygons";
			public const string Polylines = "polylines";
			public const string Sr = "sr";
		}
		
		private class BufferParameterNames
		{
			public const string Geometries = "geometries";
			public const string InSR = "inSR";
			public const string OutSR = "outSR";
			public const string BufferSR = "bufferSR";
			public const string Distances = "distances";
			public const string Unit = "unit";
            public const string UnionResults = "unionResults";
            public const string Geodesic = "geodesic";
        }
		
		private class GeometryCollectionSrParameterNames
		{
			public const string Geometries = "geometries";
			public const string Sr = "sr";
		}
		
		private class CutParameterNames
		{
			public const string Cutter = "cutter";
			public const string Target = "target";
			public const string Sr = "sr";
		}
		
		private class DensifyParameterNames
		{
			public const string Geometries = "geometries";
			public const string Sr = "sr";
			public const string MaxSegmentLength = "maxSegmentLength";
			public const string Geodesic = "geodesic";
			public const string LengthUnit = "lengthUnit";
		}
		
		private class DiffIntersectParameterNames
		{
			public const string Geometries = "geometries";
			public const string Geometry = "geometry";
			public const string Sr = "sr";
		}
		
		private class DistanceParameterNames
		{
			public const string Geometry1 = "geometry1";
			public const string Geometry2 = "geometry2";
			public const string Sr = "sr";
			public const string DistanceUnit = "distanceUnit";
			public const string Geodesic = "geodesic";
		}
		
		private class GeneralizeParameterNames
		{
			public const string Geometries = "geometries";
			public const string Sr = "sr";
			public const string MaxDeviation = "maxDeviation";
			public const string DeviationUnit = "deviationUnit";
		}
		
		private class LabelPointsParameterNames
		{
			public const string Polygons = "polygons";
			public const string Sr = "sr";
		}
		
		private class LengthsParameterNames
		{
			public const string Polylines = "polylines";
			public const string Sr = "sr";
			public const string LengthUnit = "lengthUnit";
			public const string Geodesic = "geodesic";
		}
		
		private class OffsetParameterNames
		{
			public const string Geometries = "geometries";
			public const string Sr = "sr";
			public const string OffsetDistance = "offsetDistance";
			public const string OffsetUnit = "offsetUnit";
			public const string OffsetHow = "offsetHow";
			public const string BevelRatio = "bevelRatio";
			public const string SimplifyResult = "simplifyResult";
		}
		
		private class ProjectParameterNames
		{
			public const string Geometries = "geometries";
			public const string InSR = "inSR";
			public const string OutSR = "OutSR";
		}
		
		private class RelationParameterNames
		{
			public const string Geometries1 = "geometries1";
			public const string Geometries2 = "geometries2";
			public const string Sr = "sr";
			public const string Relation = "relation";
			public const string RelationParam = "relationParam";
		}
		
		private class ReshapeParameterNames
		{
			public const string Target = "target";
			public const string Reshaper = "reshpaer";
			public const string Sr = "sr";
		}
		
		private class TrimExtendParameterNames
		{
			public const string Polylines = "polylines";
			public const string TrimExtendTo = "trimExtendTo";
			public const string Sr = "sr";
			public const string ExtendHow = "extendHow";
		}
		
		public TimeSpan Timeout {get; set;}
	}
	
}
