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
	public class FeatureValueBaseMapper : ITypeConverter<FeatureValueBase, object>
	{

		public object Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureValueBase sourceValue = context.SourceValue as FeatureValueBase;
			if (sourceValue is FeatureStringValue) {
                FeatureStringValue typedValue = sourceValue as FeatureStringValue;
				return typedValue.Value;
			} else if (sourceValue is FeatureDoubleValue) {
                FeatureDoubleValue typedValue = sourceValue as FeatureDoubleValue;
				return typedValue.Value;
			} else {
				throw new NotSupportedException();
			}
		}
	}
}
