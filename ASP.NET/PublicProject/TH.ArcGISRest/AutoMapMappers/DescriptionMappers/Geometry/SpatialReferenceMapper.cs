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
	public class SpatialReferenceMapper : ITypeConverter<SpatialReference, AgsSpatialReference>
	{

		public AgsSpatialReference Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            SpatialReference sourceValue = context.SourceValue as SpatialReference;
			var  targetValue = new AgsSpatialReference();
			var _with1 = targetValue;
			_with1.WKID = sourceValue.Wkid;
			_with1.WKT = sourceValue.Wkt;
			return targetValue;
		}
	}
}
