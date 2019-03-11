using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Geometry;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Geometry
{
	[ReflectionProfileMapper()]
	public class LineMapper : ITypeConverter<double[][], Line>
	{

		public Line Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            double[][] sourceValue = context.SourceValue as double[][];
			var  targetValue = new Line();
			targetValue.Points = Mapper.Map<Point[]>(sourceValue);
			return targetValue;
		}
	}
}
