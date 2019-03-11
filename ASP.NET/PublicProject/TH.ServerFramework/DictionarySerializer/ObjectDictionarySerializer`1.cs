// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Fasterflect;
using System.Reflection;


namespace TH.ServerFramework.DictionarySerializer
{
    public class ObjectDictionarySerializer<T>
    {
        private ObjectDictionarySerializer()
        {
            throw (new NotSupportedException());
        }

        private static PropertyInfo[] GetPropertyInfos()
        {
            var instanceType = typeof(T);
            var propInfos = (from e in instanceType.Properties(Flags.InstancePublic) where e.HasAttribute<KeyValuePropertyAttribute>() select e).ToArray();
            return propInfos;
        }

        public static IDictionary<string, string> Serialize(T instance)
        {
            var dic = new Dictionary<string, string>();
            Serialize(instance, dic);
            return dic;
        }

        public static void Serialize(T instance, System.Collections.Generic.IDictionary<string, string> dic)
        {
            var propInfos = GetPropertyInfos();
            foreach (var propInfo in propInfos)
            {
                var attribute = propInfo.Attribute<KeyValuePropertyAttribute>();
                var key = KeyValuePropertyAttribute.GetKey(propInfo, attribute);
                if (!propInfo.CanRead)
                {
                    continue;
                }
                var value = KeyValuePropertyAttribute.SerializeValue(propInfo, propInfo.GetValue(instance, null), attribute);
                dic.Add(key, value);
            }
        }

        public static T Deserialize(System.Collections.Generic.IDictionary<string, string> dictionary)
        {
            var instance = Activator.CreateInstance<T>();
            Deserialize(instance, dictionary);
            return instance;
        }

        public static void Deserialize(T instance, System.Collections.Generic.IDictionary<string, string> dictionary)
        {
            Deserialize(() => instance, dictionary);
        }

        public static void Deserialize(Func<T> instanceFactory, System.Collections.Generic.IDictionary<string, string> dictionary)
        {
            var instance = instanceFactory.Invoke();
            var propInfos = GetPropertyInfos();
            foreach (var propInfo in propInfos)
            {
                var attribute = propInfo.Attribute<KeyValuePropertyAttribute>();
                var key = KeyValuePropertyAttribute.GetKey(propInfo, attribute);
                if (!dictionary.ContainsKey(key) || !propInfo.CanWrite)
                {
                    continue;
                }
                var propValue = KeyValuePropertyAttribute.DeserializeValue(propInfo, dictionary[key], attribute);
                propInfo.SetValue(instance, propValue, null);
            }
        }
    }
}