using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers
{
	public abstract class CollectionMapperBase<TElem1, TElem2> : ITypeConverter<TElem1[], TElem2[]>
	{


		protected CollectionMapperBase()
		{
		}

		public TElem2[] Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
			TElem1[] sourceValue = context.SourceValue as TElem1[];
			var  lsTargetValue = new List<TElem2>();
			foreach (var item in sourceValue) {
				var  targetItem = Mapper.Map<TElem2>(item);
				if ((targetItem != null)) {
					lsTargetValue.Add(targetItem);
				}
			}
			return lsTargetValue.ToArray();
		}
	}
}
