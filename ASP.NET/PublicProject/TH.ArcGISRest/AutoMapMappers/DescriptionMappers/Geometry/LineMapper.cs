using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Geometry;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Geometry
{
	[ReflectionProfileMapper()]
	public class LineMapper : ITypeConverter<Line, double[][]>
	{

		public double[][] Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            Line sourceValue = context.SourceValue as Line;
			if (sourceValue.Points == null) {
				return null;
			}
			var  result = Mapper.Map<double[][]>(sourceValue.Points);
			return result;
		}
	}
}
