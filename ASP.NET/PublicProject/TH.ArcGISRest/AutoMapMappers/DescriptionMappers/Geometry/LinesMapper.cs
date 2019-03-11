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
	public class LinesMapper : ITypeConverter<Line[], double[][][]>
	{

		public double[][][] Convert(ResolutionContext context)
		{
			var  lsTargetValue = new List<double[][]>();
			if (context.IsSourceValueNull) {
				return lsTargetValue.ToArray();
			}
			Line[] sourceValue = context.SourceValue as Line[] ;
            foreach (var item in sourceValue)
            {
				var  targetItem = Mapper.Map<double[][]>(item);
				if ((targetItem != null)) {
					lsTargetValue.Add(targetItem);
				}
			}
			return lsTargetValue.ToArray();
		}
	}
}
