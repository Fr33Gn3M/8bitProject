using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.Description.Geometry;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Geometry
{
	[ReflectionProfileMapper()]
	public class PolylineMapper : ITypeConverter<Polyline, AgsPolyline>
	{

		public AgsPolyline Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            Polyline sourceValue = context.SourceValue as Polyline;
			var  targetValue = new AgsPolyline();
			var _with1 = targetValue;
			_with1.Paths = Mapper.Map<double[][][]>(sourceValue.Paths);
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			return targetValue;
		}
	}
}
