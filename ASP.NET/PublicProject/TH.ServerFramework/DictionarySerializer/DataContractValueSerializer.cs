// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Runtime.Serialization;
using System.IO;
using System.Text;


namespace TH.ServerFramework.DictionarySerializer
{
    public class DataContractVaueSerializer : IValueSerializer
    {

        private readonly Type _valueType;

        public DataContractVaueSerializer(Type valueType)
        {
            _valueType = valueType;
        }

        private bool IsCollection()
        {
            var noCollectionTypes = new[] { typeof(string), typeof(byte[]) };
            if (noCollectionTypes.Contains(_valueType))
            {
                return false;
            }
            if (_valueType.IsArray)
            {
                return true;
            }
            else if (_valueType.IsSubclassOf(typeof(IEnumerable)))
            {
                return true;
            }
            return false;
        }

        public object Deserialize(string serializedValue)
        {
            if (serializedValue == null)
            {
                return null;
            }
            var serializer = new DataContractSerializer(_valueType);
            var valueBytes = Encoding.UTF8.GetBytes(serializedValue);
            using (var ms = new MemoryStream(valueBytes))
            {
                var obj = serializer.ReadObject(ms);
                return obj;
            }

        }

        public string Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }
            var serializer = new DataContractSerializer(_valueType);
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, value);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }

            }

        }
    }
}