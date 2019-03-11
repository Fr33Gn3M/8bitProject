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
	public class PointsMapper : ITypeConverter<double[][], Point[]>
	{

		public Point[] Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			double[][] sourceValue = context.SourceValue as double[][];
			var  lsTargetValue = new List<Point>();
			foreach (var ptArr in sourceValue) {
				var  pt = new Point {
					X = ptArr[0],
					Y = ptArr[1]
				};
				lsTargetValue.Add(pt);
			}
			return lsTargetValue.ToArray();
		}
	}
}
