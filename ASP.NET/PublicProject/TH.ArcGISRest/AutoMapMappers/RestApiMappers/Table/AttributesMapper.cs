using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Table;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class AttributesMapper : ITypeConverter<IDictionary<string, object>, IDictionary<string, FeatureValueBase>>
	{


		public IDictionary<string, FeatureValueBase> Convert(ResolutionContext context)
		{
			var  targetValue = new Dictionary<string, FeatureValueBase>();
			if (context.IsSourceValueNull) {
				return targetValue;
			}
            IDictionary<string, object> sourceValue = context.SourceValue as IDictionary<string, object>;
			foreach (var kv in sourceValue) {
				var  key = kv.Key;
				var  value = Mapper.Map<FeatureValueBase>(kv.Value);
				targetValue.Add(key, value);
			}
			return targetValue;
		}
	}
}
