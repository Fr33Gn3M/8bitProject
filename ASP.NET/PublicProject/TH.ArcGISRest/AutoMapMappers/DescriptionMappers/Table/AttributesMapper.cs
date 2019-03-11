using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class AttributesMapper : ITypeConverter<IDictionary<string, FeatureValueBase>, IDictionary<string, object>>
	{

		public IDictionary<string, object> Convert(ResolutionContext context)
		{
			var  targetValue = new Dictionary<string, object>();
			if (context.IsSourceValueNull) {
				return targetValue;
			}
			IDictionary<string, FeatureValueBase> sourceValue = context.SourceValue as IDictionary<string, FeatureValueBase>;
			foreach (var kv in sourceValue) {
				var  key = kv.Key;
				var  value = Mapper.Map<object>(kv.Value);
				targetValue.Add(key, value);
			}
			return targetValue;
		}
	}
}
