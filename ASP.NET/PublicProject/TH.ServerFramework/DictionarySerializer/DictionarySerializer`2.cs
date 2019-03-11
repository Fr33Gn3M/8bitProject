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
    public class DictionarySerializer<TKey, TValue> : IValueSerializer
    {

        private readonly IValueSerializer _keySerializer;
        private readonly IValueSerializer _valueSerializer;
        private readonly string _itemSpiliter;
        private readonly string _keyValueSpilter;

        public DictionarySerializer(IValueSerializer keySerializer = null, IValueSerializer valueSerializer = null, string itemSpilter = ",", string keyValueSpilter = "|")
        {
            _keySerializer = keySerializer;
            _valueSerializer = valueSerializer;
            _itemSpiliter = itemSpilter;
            _keyValueSpilter = keyValueSpilter;
        }

        public object Deserialize(string serializedValue)
        {
            if (serializedValue == null)
            {
                return null;
            }
            var collSerializre = new CollectionTypeValueSerializer(new KeyValueSerializer(_keySerializer, _valueSerializer, _keyValueSpilter), _itemSpiliter);
            IEnumerable kvArr = collSerializre.Deserialize(serializedValue) as IEnumerable;
            var dic = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> item in kvArr)
            {
                dic.Add(item.Key, item.Value);
            }
            return dic;
        }

        public string Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }
            var dicValue = value as System.Collections.Generic.IDictionary<TKey, TValue>;
            var keyValueArr = dicValue.ToArray();
            var collSerializer = new CollectionTypeValueSerializer(new KeyValueSerializer(_keySerializer, _valueSerializer, _keyValueSpilter), _itemSpiliter);
            return collSerializer.Serialize(keyValueArr);
        }

        private class KeyValueSerializer : IValueSerializer
        {

            private readonly IValueSerializer _keySerializer;
            private readonly IValueSerializer _valueSerializer;
            private readonly string _spiliter;

            public KeyValueSerializer(IValueSerializer keySerializer, IValueSerializer valueSerializer, string spilter = ",")
            {
                _keySerializer = keySerializer;
                _valueSerializer = valueSerializer;
                _spiliter = spilter;
                if (_keySerializer == null)
                {
                    _keySerializer = new PrimitiveTypeValueSerializer<TKey>();
                }
                if (_valueSerializer == null)
                {
                    _valueSerializer = new PrimitiveTypeValueSerializer<TKey>();
                }
            }

            public object Deserialize(string serializedValue)
            {
                if (serializedValue == null)
                {
                    return null;
                }
                var parts = serializedValue.Split(_spiliter);
                var key = _keySerializer.Deserialize(parts[0]);
                object value = null;
                if (parts.Length == 2)
                {
                    value = _keySerializer.Deserialize(parts[1]);
                }
                var kvValue = new KeyValuePair<TKey, TValue>((TKey)key, (TValue)value);
                return kvValue;
            }

            public string Serialize(object value)
            {
                if (value == null)
                {
                    return null;
                }
                var kvValue = (KeyValuePair<TKey, TValue>)value;
                var aKey = _keySerializer.Serialize(kvValue.Key);
                var aValue = _keySerializer.Serialize(kvValue.Value);
                if (aValue == null)
                {
                    aValue = string.Empty;
                }
                var strValue = string.Format("{0}{1}{2}", aKey, _spiliter, aValue);
                return System.Convert.ToString(strValue);
            }
        }
    }
}
