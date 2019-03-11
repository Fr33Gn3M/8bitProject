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
	public class AgsPolygonMapper : ITypeConverter<AgsPolygon, Polygon>
	{

		public Polygon Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsPolygon sourceValue = context.SourceValue as AgsPolygon;
			var  targetValue = new Polygon();
			var _with1 = targetValue;
			_with1.Rings = Mapper.Map<Line[]>(sourceValue.Rings);
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			return targetValue;
		}
	}
}
