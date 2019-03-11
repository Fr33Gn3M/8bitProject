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
	public class AgsPointMapper : ITypeConverter<AgsPoint, Point>
	{

		public Point Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsPoint sourceValue = context.SourceValue as AgsPoint;
			var  targetValue = new Point();
			var _with1 = targetValue;
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			_with1.X = sourceValue.X;
			_with1.Y = sourceValue.Y;
			return targetValue;
		}
	}
}
