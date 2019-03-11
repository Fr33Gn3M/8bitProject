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
	public class AgsEnvelopeMapper : ITypeConverter<AgsEnvelope, Envelope>
	{

		public Envelope Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsEnvelope sourceValue = context.SourceValue as AgsEnvelope;
			var  targetValue = new Envelope();
			var _with1 = targetValue;
			_with1.SpatialReference = Mapper.Map<SpatialReference>(sourceValue.SpatialReference);
			_with1.XMin = sourceValue.XMin;
			_with1.YMin = sourceValue.YMin;
			_with1.XMax = sourceValue.XMax;
			_with1.YMax = sourceValue.YMax;
			return targetValue;
		}
	}
}
