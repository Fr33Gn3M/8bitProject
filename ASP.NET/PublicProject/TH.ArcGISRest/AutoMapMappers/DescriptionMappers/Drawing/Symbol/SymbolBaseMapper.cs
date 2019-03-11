using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Drawing.Symbol;
using TH.ArcGISRest.ApiImports.Symbols;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SymbolBaseMapper : ITypeConverter<SymbolBase, ISymbol>
	{

		private Func<ResolutionContext, ISymbol>[] CreateMappers()
		{
            Func<ResolutionContext, ISymbol> csMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new ColorSymbolMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, ISymbol> pfsMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new PictureFillSymbolMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, ISymbol> pmsMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new PictureMarkerSymbolMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, ISymbol> sfsMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new SimpleFillSymbolMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, ISymbol> slsMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new SimpleLineSymbolMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, ISymbol> smsMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new SimpleMarkerSymbolMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, ISymbol> tsMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new TextSymbolMapper();
				return mapper.Convert(ctx);
			};
			var  lsMappers = new List<Func<ResolutionContext, ISymbol>> {
				csMapper,
				pfsMapper,
				pmsMapper,
				sfsMapper,
				slsMapper,
				smsMapper,
				tsMapper
			};
			return lsMappers.ToArray();
		}

		public ISymbol Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			SymbolBase sourceValue = context.SourceValue as SymbolBase;
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
