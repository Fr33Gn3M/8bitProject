using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsDomainBase = TH.ArcGISRest.ApiImports.Domains.DomainBase;
using DespDomainBase = TH.ArcGISRest.Description.Table.DomainBase;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class DomainBaseMapper : ITypeConverter<AgsDomainBase, DespDomainBase>
	{

		private Func<ResolutionContext, DespDomainBase>[] CreateMappers()
		{
            Func<ResolutionContext, DespDomainBase> cvdMapper = (ResolutionContext context) =>
			{
				var  mapper = new CodedValueDomainMapper();
				return mapper.Convert(context);
			};
            Func<ResolutionContext, DespDomainBase> rdMapper = (ResolutionContext context) =>
			{
				var  mapper = new RangeDomainMapper();
				return mapper.Convert(context);
			};
			var  lsMappers = new List<Func<ResolutionContext, DespDomainBase>> {
				cvdMapper,
				rdMapper
			};
			return lsMappers.ToArray();
		}

		public DespDomainBase Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsDomainBase sourceValue = context.SourceValue as AgsDomainBase;
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
