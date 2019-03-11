using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsSimpleRenderer = TH.ArcGISRest.ApiImports.Renderers.SimpleRenderer;
using DespSimpleRenderer = TH.ArcGISRest.Description.Drawing.Render.SimpleRenderer;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Symbol;
using TH.ArcGISRest.Description.Drawing.Render;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class SimpleRendererMapper : ITypeConverter<AgsSimpleRenderer, DespSimpleRenderer>
	{

		public DespSimpleRenderer Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is AgsSimpleRenderer)) {
				return null;
			}
            AgsSimpleRenderer sourceValue = context.SourceValue as AgsSimpleRenderer;
			var  targetValue = new DespSimpleRenderer();
			var _with1 = targetValue;
			_with1.Description = sourceValue.Description;
			_with1.Label = sourceValue.Label;
			_with1.Symbol = Mapper.Map<SymbolBase>(sourceValue.Symbol);
			_with1.UniqueValueInfos = Mapper.Map<UniqueValueInfo[]>(sourceValue.UniqueValueInfos);
			return targetValue;
		}
	}
}
