using Newtonsoft.Json;
using System;

namespace FD.DataBase.JsonConverters
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
