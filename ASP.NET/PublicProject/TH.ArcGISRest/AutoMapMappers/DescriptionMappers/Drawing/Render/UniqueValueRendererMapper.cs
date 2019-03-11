using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using AgsUniqueValueRenderer = TH.ArcGISRest.ApiImports.Renderers.UniqueValueRenderer;
using DespUniqueValueRenderer = TH.ArcGISRest.Description.Drawing.Render.UniqueValueRenderer;
using TH.ArcGISRest.ApiImports.Symbols;
using TH.ArcGISRest.ApiImports.Renderers;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class UniqueValueRendererMapper : ITypeConverter<DespUniqueValueRenderer, AgsUniqueValueRenderer>
	{

		public AgsUniqueValueRenderer Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespUniqueValueRenderer)) {
				return null;
			}
            DespUniqueValueRenderer sourceValue = context.SourceValue as DespUniqueValueRenderer;
			var  targetValue = new AgsUniqueValueRenderer();
			var _with1 = targetValue;
			_with1.DefaultLabel = sourceValue.DefaultLabel;
			_with1.DefaultSymbol = Mapper.Map<ISymbol>(sourceValue.DefaultSymbol);
			_with1.Field1 = sourceValue.Field1;
			_with1.Field2 = sourceValue.Field2;
			_with1.Field3 = sourceValue.Field3;
			_with1.FieldDelimiter = sourceValue.FieldDelimiter;
			_with1.Type = RendererType.uniqueValue;
			_with1.UniqueValueInfos = Mapper.Map<UniqueValueInfo[]>(sourceValue.UniqueValueInfos);
			return targetValue;
		}
	}
}
