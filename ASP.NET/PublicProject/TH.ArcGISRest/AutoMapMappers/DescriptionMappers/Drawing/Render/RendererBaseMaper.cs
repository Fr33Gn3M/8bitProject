using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespRendererBase = TH.ArcGISRest.Description.Drawing.Render.RendererBase;
using AgsRendererBase = TH.ArcGISRest.ApiImports.Renderers.RendererBase;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class RendererBaseMaper : ITypeConverter<DespRendererBase, AgsRendererBase>
	{

		private Func<ResolutionContext, AgsRendererBase>[] CreateMappers()
		{
            Func<ResolutionContext, AgsRendererBase> cbrMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new ClassBreaksRendererMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, AgsRendererBase> srMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new SimpleRendererMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, AgsRendererBase> uvrMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new UniqueValueRendererMapper();
				return mapper.Convert(ctx);
			};
			var  lsMappers = new List<Func<ResolutionContext, AgsRendererBase>> {
				cbrMapper,
				srMapper,
				uvrMapper
			};
			return lsMappers.ToArray();
		}

		public AgsRendererBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			DespRendererBase sourceValue = context.SourceValue as DespRendererBase;
			var  mappers = CreateMappers();
            foreach (var item in mappers)
            {
				var  result = item.Invoke(context);
				if ((result != null)) {
					return result;
				}
			}
			throw new NotSupportedException();
		}
	}
}
