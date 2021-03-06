using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace DataBase.JsonConverters
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
