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
	public class AgsSpatialReferenceMapper : ITypeConverter<AgsSpatialReference, SpatialReference>
	{

		public SpatialReference Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsSpatialReference sourceValue = context.SourceValue as AgsSpatialReference;
			var  targetValue = new SpatialReference();
			var _with1 = targetValue;
			_with1.Wkid = sourceValue.WKID;
			_with1.Wkt = sourceValue.WKT;
			return targetValue;
		}
	}
}
