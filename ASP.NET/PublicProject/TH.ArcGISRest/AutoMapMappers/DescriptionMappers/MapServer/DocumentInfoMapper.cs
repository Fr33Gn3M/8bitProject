using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.MapServices;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class DocumentInfoMapper : ITypeConverter<IDictionary<string, string>, DocumentInfo>
	{

		public DocumentInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			IDictionary<string, string> sourceValue = context.SourceValue as IDictionary<string, string>;
			var  sourceValue2 = new Dictionary<string, string>();
            foreach (var item in sourceValue)
            {
				sourceValue2.Add(item.Key, item.Value);
			}
            var targetValue = (DocumentInfo)DocumentInfo.Parse(sourceValue2);
			return targetValue;
		}
	}
}
