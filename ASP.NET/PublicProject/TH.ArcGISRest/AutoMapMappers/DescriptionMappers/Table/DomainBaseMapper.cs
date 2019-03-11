using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespDomainBase = TH.ArcGISRest.Description.Table.DomainBase;
using AgsDomainBase = TH.ArcGISRest.ApiImports.Domains.DomainBase;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class DomainBaseMapper : ITypeConverter<DespDomainBase, AgsDomainBase>
	{

		private Func<ResolutionContext, AgsDomainBase>[] CreateMappers()
		{
            Func<ResolutionContext, AgsDomainBase> cvdMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new CodedValueDomainMapper();
				return mapper.Convert(ctx);
			};
            Func<ResolutionContext, AgsDomainBase> rdMapper = (ResolutionContext ctx) =>
			{
				var  mapper = new RangeDomainMapper();
				return mapper.Convert(ctx);
			};
			var  lsMappers = new List<Func<ResolutionContext, AgsDomainBase>> {
				cvdMapper,
				rdMapper
			};
			return lsMappers.ToArray();
		}

		public AgsDomainBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			DespDomainBase sourceValue = context.SourceValue as DespDomainBase;
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
