using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsUniqueValueInfo = TH.ArcGISRest.ApiImports.Renderers.UniqueValueInfo;
using DespUniqueValueInfo = TH.ArcGISRest.Description.Drawing.Render.UniqueValueInfo;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Symbol;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class UniqueValueInfoMapper : ITypeConverter<AgsUniqueValueInfo, DespUniqueValueInfo>
	{

		public DespUniqueValueInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsUniqueValueInfo sourceValue = context.SourceValue as AgsUniqueValueInfo;
			var  targetValue = new DespUniqueValueInfo();
			var _with1 = targetValue;
			_with1.Description = sourceValue.Description;
			_with1.Label = sourceValue.Label;
			_with1.Symbol = Mapper.Map<SymbolBase>(sourceValue.Symbol);
			_with1.Value = sourceValue.Value;
			return targetValue;
		}
	}
}
