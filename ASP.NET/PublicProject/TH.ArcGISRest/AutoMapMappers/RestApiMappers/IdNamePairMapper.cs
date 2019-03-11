using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsIdNamePair = TH.ArcGISRest.ApiImports.IdNamePair;
using DespIdNamePair = TH.ArcGISRest.Description.IdNamePair;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers
{
	[ReflectionProfileMapper()]
	public class IdNamePairMapper : ITypeConverter<AgsIdNamePair, DespIdNamePair>
	{

		public DespIdNamePair Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsIdNamePair sourceValue = context.SourceValue as AgsIdNamePair;
			var  targetValue = new DespIdNamePair();
			var _with1 = targetValue;
			_with1.ID = sourceValue.ID;
			_with1.Name = sourceValue.Name;
			return targetValue;
		}
	}
}
