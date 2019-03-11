using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsLod = TH.ArcGISRest.ApiImports.MapServices.Lod;
using DespLod = TH.ArcGISRest.Description.MapServer.LOD;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class LodMapper : ITypeConverter<AgsLod, DespLod>
	{

		public DespLod Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsLod sourceValue = context.SourceValue as AgsLod;
			var  targetValue = new DespLod();
			var _with1 = targetValue;
			_with1.Level = sourceValue.Level;
			_with1.Resolution = sourceValue.Resolution;
			_with1.Sacle = sourceValue.Scale;
			return targetValue;
		}
	}
}
