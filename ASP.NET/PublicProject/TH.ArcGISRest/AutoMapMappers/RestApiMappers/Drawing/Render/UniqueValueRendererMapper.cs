using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsUniqueValueRenderer = TH.ArcGISRest.ApiImports.Renderers.UniqueValueRenderer;
using DespUniqueValueRenderer = TH.ArcGISRest.Description.Drawing.Render.UniqueValueRenderer;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Symbol;
using TH.ArcGISRest.Description.Drawing.Render;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class UniqueValueRendererMapper : ITypeConverter<AgsUniqueValueRenderer, DespUniqueValueRenderer>
	{

		public DespUniqueValueRenderer Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is AgsUniqueValueRenderer)) {
				return null;
			}
            AgsUniqueValueRenderer sourceValue = context.SourceValue as AgsUniqueValueRenderer;
			var  targetValue = new DespUniqueValueRenderer();
			var _with1 = targetValue;
			_with1.DefaultLabel = sourceValue.DefaultLabel;
			_with1.DefaultSymbol = Mapper.Map<SymbolBase>(sourceValue.DefaultSymbol);
			_with1.Field1 = sourceValue.Field1;
			_with1.Field2 = sourceValue.Field2;
			_with1.Field3 = sourceValue.Field3;
			_with1.FieldDelimiter = sourceValue.FieldDelimiter;
			_with1.UniqueValueInfos = Mapper.Map<UniqueValueInfo[]>(sourceValue.UniqueValueInfos);
			return targetValue;
		}
	}
}
