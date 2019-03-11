using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Catelog;
using AutoMapper;
using TH.ArcGISRest.ApiImports;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Catelog
{
	[ReflectionProfileMapper()]
	public class NameTypePairMapper : ITypeConverter<NameTypePair, KeyValuePair<string, ServiceType>>
	{

		public KeyValuePair<string, ServiceType> Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
                return default(KeyValuePair<string, ServiceType>);
			}
            NameTypePair sourceVlaue = context.SourceValue as NameTypePair;
			KeyValuePair<string, ServiceType> targetValue;
			var  key = sourceVlaue.Name;
			var  type = Mapper.Map<ServiceType>(sourceVlaue.Type);
			targetValue = new KeyValuePair<string, ServiceType>(key, type);
			return targetValue;
		}
	}
}
