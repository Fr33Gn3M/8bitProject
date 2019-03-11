using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.Symbols;
using TH.ArcGISRest.Description.Drawing.Symbol;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Symbol
{
	[ReflectionProfileMapper()]
	public class SymbolMapper : ITypeConverter<ISymbol, SymbolBase>
	{

		private Func<ResolutionContext, SymbolBase>[] CreateMappers()
		{
            Func<ResolutionContext, SymbolBase> pfsMapper = (ResolutionContext context) =>
			{
				var  mapper = new PictureFillSymbolMapper();
				return mapper.Convert(context);
			};
            Func<ResolutionContext, SymbolBase> pmsMapper = (ResolutionContext context) =>
			{
				var  mapper = new PictureMarkerSymbolMapper();
				return mapper.Convert(context);
			};
            Func<ResolutionContext, SymbolBase> sfsMapper = (ResolutionContext context) =>
			{
				var  mapper = new SimpleFillSymbolMapper();
				return mapper.Convert(context);
			};
            Func<ResolutionContext, SymbolBase> slsMapper = (ResolutionContext context) =>
			{
				var  mapper = new SimpleLineSymbolMapper();
				return mapper.Convert(context);
			};
            Func<ResolutionContext, SymbolBase> smsMapper = (ResolutionContext context) =>
			{
				var  mapper = new SimpleMarkerSymbolMapper();
				return mapper.Convert(context);
			};
            Func<ResolutionContext, SymbolBase> tsMapper = (ResolutionContext context) =>
			{
				var  mapper = new TextSymbolMapper();
				return mapper.Convert(context);
			};
			var  ls = new List<Func<ResolutionContext, SymbolBase>> {
				pfsMapper,
				pmsMapper,
				sfsMapper,
				slsMapper,
				smsMapper,
				tsMapper
			};
			return ls.ToArray();
		}

		public SymbolBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
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
