using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework
{
    public abstract class CollectionMapperBase<TElem1, TElem2> : ITypeConverter<TElem1[], TElem2[]>
    {
        protected CollectionMapperBase()
        {
        }

        public TElem2[] Convert(ResolutionContext context)
        {
            if (context.IsSourceValueNull)
            {
                return null;
            }
            TElem1[] sourceValue = context.SourceValue as TElem1[];
            var lsTargetValue = new List<TElem2>();
            foreach (TElem1 item in sourceValue)
            {
                dynamic targetItem = Mapper.Map<TElem2>(item);
                if ((targetItem != null))
                {
                    lsTargetValue.Add(targetItem);
                }
            }
            return lsTargetValue.ToArray();
        }
    }

}
