using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Catelog;
using AutoMapper;
using TH.ArcGISRest.ApiImports;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class KeyValueNameTypePairMapper : ITypeConverter<KeyValuePair<string, ServiceType>, NameTypePair>
	{

		public NameTypePair Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            KeyValuePair<string, ServiceType> sourceValue = (KeyValuePair<string, ServiceType>)context.SourceValue;
			var  targetValue = new NameTypePair();
			var _with1 = targetValue;
			_with1.Name = sourceValue.Key;
			if (!(sourceValue.Value == ServiceType.Unknown)) {
				_with1.Type = sourceValue.Value.ToString();
			}
			return targetValue;
		}
	}
}
