using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;

namespace TH.DataBase.JsonConverters
{
	[Serializable()]
	public abstract class CollectionConverterBase<T> : JsonConverter
	{

		protected abstract JsonConverter CreateElemConverter();


		public override bool CanConvert(Type objectType)
		{
			if (typeof(T[]).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IList<T> ls = new List<T>();
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            //reader.Skip()'20120317 EndArrary
            if (reader.TokenType == JsonToken.StartArray)
            {
                var converter = CreateElemConverter();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndArray)
                    {
                        break; // TODO: might not be correct. Was : Exit Do
                    }
                    var elem = (T)converter.ReadJson(reader, objectType, existingValue, serializer);
                    ls.Add(elem);
                }
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Skip();
            }
            return ls.ToArray();
        }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}


    }
}
