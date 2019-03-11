using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsDrawingInfo = TH.ArcGISRest.ApiImports.Renderers.DrawingInfo;
using AutoMapper;
using DespDrawingInfo = TH.ArcGISRest.Description.Drawing.DrawingInfo;
using TH.ArcGISRest.ApiImports.Renderers;
using TH.ArcGISRest.ApiImports.Symbols;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class DrawingInfoMapper : ITypeConverter<DespDrawingInfo, AgsDrawingInfo>
	{

		public AgsDrawingInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespDrawingInfo sourceValue = context.SourceValue as DespDrawingInfo;
			var  targetValue = new AgsDrawingInfo();
			var _with1 = targetValue;
			_with1.LabelingInfo = Mapper.Map<LabelClass[]>(sourceValue.LabelingInfo);
			_with1.Renderer = Mapper.Map<RendererBase>(sourceValue.Renderer);
			_with1.Transparency = sourceValue.Transparency;
			return targetValue;
		}
	}
}
