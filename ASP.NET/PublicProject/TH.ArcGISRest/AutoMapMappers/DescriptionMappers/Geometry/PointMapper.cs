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
	public class PointMapper : ITypeConverter<Point, AgsPoint>
	{

		public AgsPoint Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            Point sourceValue = context.SourceValue as Point;
			var  targetValue = new AgsPoint();
			var _with1 = targetValue;
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			_with1.X = sourceValue.X;
			_with1.Y = sourceValue.Y;
			return targetValue;
		}
	}
}
