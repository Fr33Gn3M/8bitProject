using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsUniqueValueInfo = TH.ArcGISRest.ApiImports.Renderers.UniqueValueInfo;
using DespUniqueValueInfo = TH.ArcGISRest.Description.Drawing.Render.UniqueValueInfo;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class UniqueValueInfoMapper : ITypeConverter<DespUniqueValueInfo, AgsUniqueValueInfo>
	{

		public AgsUniqueValueInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespUniqueValueInfo sourceValue = context.SourceValue as DespUniqueValueInfo;
			var  targetValue = new AgsUniqueValueInfo();
			var _with1 = targetValue;
			_with1.Description = sourceValue.Description;
			_with1.Label = sourceValue.Label;
			_with1.Symbol = Mapper.Map<ISymbol>(sourceValue.Symbol);
			_with1.Value = sourceValue.Value;
			return targetValue;
		}
	}
}
