using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsDrawingInfo = TH.ArcGISRest.ApiImports.Renderers.DrawingInfo;
using DespDrawingInfo = TH.ArcGISRest.Description.Drawing.DrawingInfo;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Label;
using TH.ArcGISRest.Description.Drawing.Render;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class DrawingInfoMapper : ITypeConverter<AgsDrawingInfo, DespDrawingInfo>
	{

		public DespDrawingInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsDrawingInfo sourceValue = context.SourceValue as AgsDrawingInfo;
			var  targetValue = new DespDrawingInfo();
			var _with1 = targetValue;
			_with1.LabelingInfo = Mapper.Map<LabelClass[]>(sourceValue.LabelingInfo);
			_with1.Renderer = Mapper.Map<RendererBase>(sourceValue.Renderer);
			_with1.Transparency = sourceValue.Transparency;
			return targetValue;
		}
	}
}
