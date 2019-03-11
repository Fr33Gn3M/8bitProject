// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports


namespace TH.ServerFramework.DictionarySerializer
{
    public class PrimitiveCollectionTypeValueSerializer<T> : IValueSerializer
    {

        private readonly IValueSerializer _baseSerializer;
        private readonly string _spliter;

        public PrimitiveCollectionTypeValueSerializer(string spliter)
        {
            _spliter = spliter;
            _baseSerializer = new CollectionTypeValueSerializer(new PrimitiveTypeValueSerializer<T>(), spliter);
        }

        public PrimitiveCollectionTypeValueSerializer()
            : this(",")
        {
        }

        public object Deserialize(string serializedValue)
        {
            if (serializedValue == null)
            {
                return null;
            }
            if (typeof(T).Equals(typeof(byte[])))
            {
                return Convert.FromBase64String(serializedValue);
            }
            return _baseSerializer.Deserialize(serializedValue);
        }

        public string Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }
            if (typeof(T).Equals(typeof(byte[])))
            {
                return Convert.ToBase64String((byte[])value);
            }
            return _baseSerializer.Serialize(value);
        }
    }
}