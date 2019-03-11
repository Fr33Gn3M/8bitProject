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
	public class PointsMapper : ITypeConverter<Point[], double[][]>
	{

		public double[][] Convert(ResolutionContext context)
		{
			var  lsPoints = new List<double[]>();
			if (context.IsSourceValueNull) {
				return lsPoints.ToArray();
			}
			Point[] sourceValue = context.SourceValue as Point[];
            foreach (var item in sourceValue)
            {
				double[] ptArr = new double[2];
				ptArr[0] = item.X;
				ptArr[1] = item.Y;
				lsPoints.Add(ptArr);
			}
			return lsPoints.ToArray();
		}
	}
}
