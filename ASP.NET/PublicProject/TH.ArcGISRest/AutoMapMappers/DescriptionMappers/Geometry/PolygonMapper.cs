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
	public class PolygonMapper : ITypeConverter<Polygon, AgsPolygon>
	{

		public AgsPolygon Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            Polygon sourceValue = context.SourceValue as Polygon;
			var  targetValue = new AgsPolygon();
			var _with1 = targetValue;
			_with1.Rings = Mapper.Map<double[][][]>(sourceValue.Rings);
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			return targetValue;
		}
	}
}
