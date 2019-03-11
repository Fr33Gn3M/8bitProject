using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsClassBreaksRenderer = TH.ArcGISRest.ApiImports.Renderers.ClassBreaksRenderer;
using DespClassBreaksRenderer = TH.ArcGISRest.Description.Drawing.Render.ClassBreaksRenderer;
using TH.ArcGISRest.ApiImports.Renderers;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class ClassBreaksRendererMapper : ITypeConverter<DespClassBreaksRenderer, AgsClassBreaksRenderer>
	{

		public AgsClassBreaksRenderer Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is DespClassBreaksRenderer)) {
				return null;
			}
            DespClassBreaksRenderer sourceValue = context.SourceValue as DespClassBreaksRenderer;
			var  targetValue = new AgsClassBreaksRenderer();
			var _with1 = targetValue;
			_with1.ClassBreakInfos = Mapper.Map<ClassBreakInfo[]>(sourceValue.ClassBreakInfos);
			_with1.Field = sourceValue.Field;
			_with1.MinValue = sourceValue.MinValue;
			_with1.Type = RendererType.classBreaks;
			return targetValue;
		}
	}
}
