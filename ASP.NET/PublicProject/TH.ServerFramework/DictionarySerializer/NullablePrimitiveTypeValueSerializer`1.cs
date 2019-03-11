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
    public class NullablePrimitiveTypeValueSerializer<T> : IValueSerializer where T : struct
    {

        private readonly IValueSerializer _baseSerializer;

        public NullablePrimitiveTypeValueSerializer()
        {
            _baseSerializer = new PrimitiveTypeValueSerializer<T>();
        }

        public object Deserialize(string serializedValue)
        {
            if (serializedValue == null)
            {
                return null;
            }
            return _baseSerializer.Deserialize(serializedValue);
        }

        public string Serialize(object value)
        {
            Nullable<T> nullableValue = (T)value;
            if ((nullableValue == null) || !nullableValue.HasValue)
            {
                return null;
            }
            return _baseSerializer.Serialize(nullableValue.Value);
        }
    }
}