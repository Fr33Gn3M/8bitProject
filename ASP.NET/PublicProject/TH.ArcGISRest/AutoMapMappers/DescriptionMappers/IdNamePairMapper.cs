using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespIdNamePair = TH.ArcGISRest.Description.IdNamePair;
using AgsIdNamePair = TH.ArcGISRest.ApiImports.IdNamePair;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers
{
	[ReflectionProfileMapper()]
	public class IdNamePairMapper : ITypeConverter<DespIdNamePair, AgsIdNamePair>
	{

		public AgsIdNamePair Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespIdNamePair sourceValue = context.SourceValue as DespIdNamePair;
			var  targetValue = new AgsIdNamePair();
			var _with1 = targetValue;
			_with1.ID = sourceValue.ID;
			_with1.Name = sourceValue.Name;
			return targetValue;
		}
	}
}
