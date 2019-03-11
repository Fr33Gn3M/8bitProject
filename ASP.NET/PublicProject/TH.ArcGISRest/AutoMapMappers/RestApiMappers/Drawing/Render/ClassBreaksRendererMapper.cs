using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsClassBreaksRenderer = TH.ArcGISRest.ApiImports.Renderers.ClassBreaksRenderer;
using DespClassBreaksRenderer = TH.ArcGISRest.Description.Drawing.Render.ClassBreaksRenderer;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Render;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class ClassBreaksRendererMapper : ITypeConverter<AgsClassBreaksRenderer, DespClassBreaksRenderer>
	{

		public DespClassBreaksRenderer Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull || !(context.SourceValue is AgsClassBreaksRenderer)) {
				return null;
			}
            AgsClassBreaksRenderer sourceValue = context.SourceValue as AgsClassBreaksRenderer;
			var  targetValue = new DespClassBreaksRenderer();
			var _with1 = targetValue;
			_with1.ClassBreakInfos = Mapper.Map<ClassBreakInfo[]>(sourceValue.ClassBreakInfos);
			_with1.Field = sourceValue.Field;
			_with1.MinValue = sourceValue.MinValue;
			return targetValue;
		}
	}
}
