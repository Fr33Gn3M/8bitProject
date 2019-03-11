using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.Description.MapServer;
using AgsLod = TH.ArcGISRest.ApiImports.MapServices.Lod;
using DespLod = TH.ArcGISRest.Description.MapServer.LOD;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class LODMapper : ITypeConverter<DespLod, AgsLod>
	{


		public AgsLod Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespLod sourceValue = context.SourceValue as DespLod;
			var  targetValue = new AgsLod();
			var _with1 = targetValue;
			_with1.Level = sourceValue.Level;
			_with1.Resolution = sourceValue.Resolution;
			_with1.Scale = sourceValue.Sacle;
			return targetValue;
		}
	}
}
