namespace TH.ServerFramework.Utility
{
    using TH.ServerFramework.Configuration;
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static  class CollectionExtensions
    {
        public static void AddRangeEx<T>(this IList<T> source, IEnumerable<T> values)
        {
            if ((values == null))
            {
                return;
            }
            foreach (var value in values)
            {
                source.Add(value);
            }
        }

        public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> values)
        {
            if ((values == null))
            {
                return;
            }
            foreach (var value in values)
            {
                source.Add(value);
            }
        }

        public static void ForEach<T>(this IEnumerable source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }
            foreach (T elem in source)
            {
                action.Invoke(elem);
            }
        }

        public static IDictionary<string, string> ToDictionary(this KeyValueConfigurationCollection source)
        {
            if (source == null)
            {
                return null;
            }
            dynamic dic = new Dictionary<string, string>();
            foreach (KeyValueConfigurationElement elem in source)
            {
                dic.Add(elem.Key, elem.Value);
            }
            return dic;
        }

        public static IList<T> ToList<T>(this IEnumerable source)
        {
            if (source is IList<T>)
            {
                return source as IList<T>;
            }
            var ls = new List<T>();
            foreach (var item in source)
            {
                ls.Add((T)item);
            }
            return ls;
        }

        private static bool NullableEquals(object value1, object value2)
        {
            if (value1 == null)
            {
                if (value2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return value1.Equals(value2);
            }
        }

        public static bool ContainsAll<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> other)
        {
            if (other == null)
            {
                return true;
            }
            dynamic nHit = 0;
            foreach (var kv in other)
            {
                if (!source.ContainsKey(kv.Key))
                {
                    continue;
                }
                dynamic sourceValue = source[kv.Key];
                if (!NullableEquals(sourceValue, kv.Value))
                {
                    return false;
                }
                nHit += 1;
            }
            return other.Count == nHit;
        }

        public static KeyValueConfigurationCollection ToKeyValueConfigurationCollection(this IDictionary<string, string> source)
        {
            if (source == null || source.Count == 0)
            {
                return null;
            }
            var keyValues = new WritableKeyValueConfigurationCollection();
            foreach (var kv in source)
            {
                keyValues.Add((string)kv.Key, (string)kv.Value);
            }
            return keyValues;
        }
    }

}

