using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleRenderer = TH.ArcGISRest.ApiImports.Renderers.SimpleRenderer;
using DespSimpleRenderer = TH.ArcGISRest.Description.Drawing.Render.SimpleRenderer;
using AutoMapper;
using TH.ArcGISRest.ApiImports.Symbols;
using TH.ArcGISRest.ApiImports.Renderers;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class SimpleRendererMapper : ITypeConverter<DespSimpleRenderer, AgsSimpleRenderer>
	{

		public AgsSimpleRenderer Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespSimpleRenderer)) {
				return null;
			}
            DespSimpleRenderer sourceValue = context.SourceValue as DespSimpleRenderer;
			var  targetValue = new AgsSimpleRenderer();
			var _with1 = targetValue;
			_with1.Description = sourceValue.Description;
			_with1.Label = sourceValue.Label;
			_with1.Symbol = Mapper.Map<ISymbol>(sourceValue.Symbol);
			_with1.Type = RendererType.simple;
			_with1.UniqueValueInfos = Mapper.Map<UniqueValueInfo[]>(sourceValue.UniqueValueInfos);
			return targetValue;
		}
	}
}
