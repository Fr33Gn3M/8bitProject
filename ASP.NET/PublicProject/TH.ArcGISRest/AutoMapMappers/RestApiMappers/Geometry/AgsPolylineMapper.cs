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
	public class AgsPolylineMapper : ITypeConverter<AgsPolyline, Polyline>
	{

		public Polyline Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsPolyline sourceValue = context.SourceValue as AgsPolyline;
			var  targetValue = new Polyline();
			var _with1 = targetValue;
			_with1.Paths = Mapper.Map<Line[]>(sourceValue.Paths);
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			return targetValue;
		}
	}
}
