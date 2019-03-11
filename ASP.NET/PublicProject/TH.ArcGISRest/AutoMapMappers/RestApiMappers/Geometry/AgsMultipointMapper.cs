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
	public class AgsMultipointMapper : ITypeConverter<AgsMultipoint, Multipoint>
	{

		public Multipoint Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsMultipoint sourceValue = context.SourceValue as AgsMultipoint;
			var  targetValue = new Multipoint();
			var _with1 = targetValue;
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			_with1.Points = Mapper.Map<Point[]>(sourceValue.Points);
			return targetValue;
		}
	}
}
