using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.ApiImports.Geometry;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Geometry
{
	[ReflectionProfileMapper()]
	public class GeometryBaseMapper : ITypeConverter<GeometryBase, AgsGeometryBase>
	{


		public AgsGeometryBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            GeometryBase sourceValue = context.SourceValue as GeometryBase;
			AgsGeometryBase targetValue = null;
			if (sourceValue is Point) {
				targetValue = Mapper.Map<AgsPoint>(sourceValue);
			} else if (sourceValue is Multipoint) {
				targetValue = Mapper.Map<AgsMultipoint>(sourceValue);
			} else if (sourceValue is Polyline) {
				targetValue = Mapper.Map<AgsPolyline>(sourceValue);
			} else if (sourceValue is Polygon) {
				targetValue = Mapper.Map<AgsPolygon>(sourceValue);
			} else if (sourceValue is Envelope) {
				targetValue = Mapper.Map<AgsEnvelope>(sourceValue);
			} else {
				throw new NotSupportedException();
			}
			return targetValue;
		}
	}
}
