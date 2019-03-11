namespace TH.ServerFramework.WebClientPoint
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class NameValueDictionary<TValue> : IDictionary<string, TValue>
    {
        private readonly IDictionary<string, TValue> _innerDic;
        public NameValueDictionary()
        {
            _innerDic = new Dictionary<string, TValue>();
        }

        public NameValueDictionary(IDictionary<string, TValue> dic)
            : this()
        {
            foreach (var kv in dic)
            {
                Add(kv.Key, kv.Value);
            }
        }

        public void Add(KeyValuePair<string, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _innerDic.Clear();
        }

        public bool Contains(KeyValuePair<string, TValue> item)
        {
            foreach (var kv in _innerDic)
            {
                if (string.Equals(item.Key, kv.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    return kv.Value.Equals(item.Value);
                }
            }
            return false;
        }

        public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get { return _innerDic.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, TValue> item)
        {
            return Remove(item.Key);
        }

        public void Add(string key, TValue value)
        {
            if (value == null)
            {
                Remove(key);
                return;
            }
            foreach (var kv in _innerDic)
            {
                if (string.Equals(key, kv.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    _innerDic[key] = value;
                    return;
                }
            }
            _innerDic.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _innerDic.Keys.Contains(key, StringComparer.OrdinalIgnoreCase);
        }

        public TValue this[string key]
        {
            get
            {
                foreach (var kv in _innerDic)
                {
                    if (string.Equals(kv.Key, key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return kv.Value;
                    }
                }
                return default(TValue);
            }
            set
            {
                if (value == null)
                {
                    Remove(key);
                    return;
                }
                if (!ContainsKey(key))
                {
                    Add(key, value);
                }
                else
                {
                    foreach (var kv in _innerDic)
                    {
                        if (string.Equals(kv.Key, key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            _innerDic[key] = value;
                            return;
                        }
                    }
                }
            }
        }

        public ICollection<string> Keys
        {
            get { return (from e in _innerDic.Keys select e.ToLower()).ToList(); }
        }

        public bool Remove(string key)
        {
            string foundKey = null;
            foreach (var dicKey in _innerDic.Keys)
            {
                if (string.Equals(dicKey, key, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundKey = dicKey;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (foundKey == null)
            {
                return false;
            }
            _innerDic.Remove(foundKey);
            return true;
        }

        public bool TryGetValue(string key, ref TValue value)
        {
            foreach (var kv in _innerDic)
            {
                if (string.Equals(key, kv.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    value = kv.Value;
                    return true;
                }
            }
            return false;
        }

        public ICollection<TValue> Values
        {
            get { return _innerDic.Values; }
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            return _innerDic.GetEnumerator();
        }

        public System.Collections.IEnumerator GetEnumerator1()
        {
            return _innerDic.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        void IDictionary<string, TValue>.Add(string key, TValue value)
        {
            _innerDic.Add(key, value);
        }

        bool IDictionary<string, TValue>.ContainsKey(string key)
        {
            return _innerDic.ContainsKey(key);
        }

        ICollection<string> IDictionary<string, TValue>.Keys
        {
            get {return  _innerDic.Keys; }
        }

        bool IDictionary<string, TValue>.Remove(string key)
        {
            return _innerDic.Remove(key);
        }

        bool IDictionary<string, TValue>.TryGetValue(string key, out TValue value)
        {
            return _innerDic.TryGetValue(key, out value);
        }

        ICollection<TValue> IDictionary<string, TValue>.Values
        {
            get { return _innerDic.Values; }
        }

        TValue IDictionary<string, TValue>.this[string key]
        {
            get
            {
                return _innerDic[key];
            }
            set
            {
                _innerDic[key] = value;
            }
        }

        void ICollection<KeyValuePair<string, TValue>>.Add(KeyValuePair<string, TValue> item)
        {
            _innerDic.Add(item);
        }

        void ICollection<KeyValuePair<string, TValue>>.Clear()
        {
            _innerDic.Clear();
        }

        bool ICollection<KeyValuePair<string, TValue>>.Contains(KeyValuePair<string, TValue> item)
        {
            return _innerDic.Contains(item);
        }

        void ICollection<KeyValuePair<string, TValue>>.CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
            _innerDic.CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<string, TValue>>.Count
        {
            get { return _innerDic.Count; }
        }

        bool ICollection<KeyValuePair<string, TValue>>.IsReadOnly
        {
            get { return _innerDic.IsReadOnly; }
        }

        bool ICollection<KeyValuePair<string, TValue>>.Remove(KeyValuePair<string, TValue> item)
        {
            return _innerDic.Remove(item);
        }

        IEnumerator<KeyValuePair<string, TValue>> IEnumerable<KeyValuePair<string, TValue>>.GetEnumerator()
        {
            return _innerDic.GetEnumerator();
        }
    }

}

