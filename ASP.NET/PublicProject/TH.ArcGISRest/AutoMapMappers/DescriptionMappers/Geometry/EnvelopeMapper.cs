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
	public class EnvelopeMapper : ITypeConverter<Envelope, AgsEnvelope>
	{

		public AgsEnvelope Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            Envelope sourceValue = context.SourceValue as Envelope;
			var  targetValue = new AgsEnvelope();
			var _with1 = targetValue;
			_with1.SpatialReference = Mapper.Map<AgsSpatialReference>(sourceValue.SpatialReference);
			_with1.XMax = sourceValue.XMax;
			_with1.XMin = sourceValue.XMin;
			_with1.YMax = sourceValue.YMax;
			_with1.YMin = sourceValue.YMin;
			return targetValue;
		}
	}
}
