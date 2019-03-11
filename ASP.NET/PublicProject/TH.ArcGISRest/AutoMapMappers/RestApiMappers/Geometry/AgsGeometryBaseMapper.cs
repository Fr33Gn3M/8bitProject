using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.Description.Geometry;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Geometry
{
	[ReflectionProfileMapper()]
	public class AgsGeometryBaseMapper : ITypeConverter<AgsGeometryBase, GeometryBase>
	{

		public GeometryBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			AgsGeometryBase sourceValue = context.SourceValue as  AgsGeometryBase;
			GeometryBase targetValue = null;
			if (sourceValue is AgsEnvelope) {
				targetValue = Mapper.Map<Envelope>(sourceValue);
			} else if (sourceValue is AgsPoint) {
				targetValue = Mapper.Map<Point>(sourceValue);
			} else if (sourceValue is AgsMultipoint) {
				targetValue = Mapper.Map<Multipoint>(sourceValue);
			} else if (sourceValue is AgsPolyline) {
				targetValue = Mapper.Map<Polyline>(sourceValue);
			} else if (sourceValue is AgsPolygon) {
				targetValue = Mapper.Map<Polygon>(sourceValue);
			} else {
				throw new NotSupportedException();
			}
			return targetValue;
		}
	}
}
