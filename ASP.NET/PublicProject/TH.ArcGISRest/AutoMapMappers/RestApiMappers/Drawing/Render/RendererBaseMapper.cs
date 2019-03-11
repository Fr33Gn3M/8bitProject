using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsRendererBase = TH.ArcGISRest.ApiImports.Renderers.RendererBase;
using DespRendererBase = TH.ArcGISRest.Description.Drawing.Render.RendererBase;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Render
{
	[ReflectionProfileMapper()]
	public class RendererBaseMapper : ITypeConverter<AgsRendererBase, DespRendererBase>
	{

		private Func<ResolutionContext, DespRendererBase>[] CreateMappers()
		{
            Func<ResolutionContext, DespRendererBase> cbrMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new ClassBreaksRendererMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, DespRendererBase> srMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new SimpleRendererMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, DespRendererBase> uvrMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new UniqueValueRendererMapper();
				return mapper.Convert(ctx);
			};
			var  ls = new List<Func<ResolutionContext, DespRendererBase>> {
				cbrMapper,
				srMapper,
				uvrMapper
			};
			return ls.ToArray();
		}

		public DespRendererBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			var  mappers = CreateMappers();
			foreach (var item in mappers) {
				var  result = item.Invoke(context);
				if ((result != null)) {
					return result;
				}
			}
			throw new NotSupportedException();
		}
	}
}
