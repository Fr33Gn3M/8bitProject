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
    public class PrimitiveTypeValueSerializer<T> : IValueSerializer
    {

        private static readonly Type[] _supportTypes = new Type[] {typeof(string),
			typeof(int),
			typeof(double),
			typeof(Single),
			typeof(bool),
			typeof(byte),
			typeof(decimal),
			typeof(long),
			typeof(DateTime)};

        public static Type[] GetPrimitiveTypes()
        {
            return _supportTypes;
        }

        private void EnsureTypeSupported()
        {
            var type = typeof(T);
            if (!_supportTypes.Contains(type))
            {
                throw (new NotSupportedException(type.FullName));
            }
        }

        public object Deserialize(string serializedValue)
        {
            var type = typeof(T);
            EnsureTypeSupported();
            var value = Convert.ChangeType(serializedValue, type);
            return value;
        }

        public string Serialize(object value)
        {
            var t = typeof(T);
            EnsureTypeSupported();
            var serializedValue = Convert.ChangeType(value, typeof(string));
            return (string)(serializedValue);
        }
    }
}