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
    public class CollectionTypeValueSerializer : IValueSerializer
    {

        private const string DefaultSpiliter = ",";

        private readonly IValueSerializer _baseSerializer;
        private readonly string _spiliter;

        public CollectionTypeValueSerializer(IValueSerializer baseSerializer, string spiliter)
        {
            _baseSerializer = baseSerializer;
            _spiliter = spiliter;
        }

        public CollectionTypeValueSerializer(IValueSerializer baseSerializer)
            : this(baseSerializer, DefaultSpiliter)
        {
        }

        public object Deserialize(string serializedValue)
        {
            if (serializedValue == null)
            {
                return null;
            }
            var ls = new System.Collections.Generic.List<object>();
            var strValues = serializedValue.Split(_spiliter);
            foreach (var strValue in strValues)
            {
                var objValue = _baseSerializer.Deserialize(strValue.ToString());
                if (objValue != null)
                {
                    ls.Add(objValue);
                }
            }
            if (ls.Count == 0)
            {
                return new { };
            }
            else
            {
                var elemT = ls.First().GetType();
                var arr = Array.CreateInstance(elemT, ls.Count);
                for (var index = 0; index <= ls.Count - 1; index++)
                {
                    var a=ls[index];
                    arr.SetValue(a, index);
                }
                return arr;
            }
        }

        public string Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }
            IEnumerable collectionValue = value as IEnumerable;
            var lsStrValues = new List<string>();
            foreach (var aValue in collectionValue)
            {
                if (aValue == null)
                {
                    continue;
                }
                var strValue = _baseSerializer.Serialize(aValue);
                lsStrValues.Add(strValue);
            }
            var allValueString = lsStrValues.ToArray().ToString("", "", "", _spiliter);
            return (string)(allValueString);
        }
    }
}
