using System;
using Newtonsoft.Json;

namespace Sys.DataBase.JsonConverters
{
    [Serializable()]
    public class QueryFilterCollectionConverter : CollectionConverterBase<QueryFilterBase>
    {

        protected override JsonConverter CreateElemConverter()
        {
            return new QueryFilterConverter();
        }
    }
}
