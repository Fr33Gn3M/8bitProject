using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class DocumentInfoMapper : ITypeConverter<DocumentInfo, IDictionary<string, string>>
	{

		public IDictionary<string, string> Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			DocumentInfo sourceValue = context.SourceValue as DocumentInfo;
			var  targetValue = new Dictionary<string, string>();
			foreach (var kv in sourceValue) {
				var  key = kv.Key;
				var  value = kv.Value;
				if (!(value is string)) {
					value = value.ToString();
				}
				targetValue.Add(key, value);
			}
			return targetValue;
		}
	}
}
