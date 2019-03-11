using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Table;
using AutoMapper;
using System.Linq;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class ObjectFeatureBaseValueMapper : ITypeConverter<object, FeatureValueBase>
	{

		private static readonly Type[] _numberTypes = {
			typeof(long),
			typeof(int),
			typeof(double)
		};
		public FeatureValueBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			var  sourceValue = context.SourceValue;
			FeatureValueBase targetValue = null;
			if (_numberTypes.Contains(sourceValue.GetType())) {
				targetValue = new FeatureDoubleValue { Value = (double)sourceValue };

			} else if (sourceValue is string) {
				targetValue = new FeatureStringValue { Value = sourceValue.ToString() };
			} else {
				throw new NotSupportedException();
			}
			return targetValue;
		}
	}
}
