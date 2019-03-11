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
	public class MultipointMapper : ITypeConverter<Multipoint, AgsMultipoint>
	{

		public AgsMultipoint Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            Multipoint sourceValue = context.SourceValue as Multipoint;
			var  targetValue = new AgsMultipoint();
			var _with1 = targetValue;
			_with1.Points = Mapper.Map<double[][]>(sourceValue.Points);
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			return targetValue;
		}
	}
}
