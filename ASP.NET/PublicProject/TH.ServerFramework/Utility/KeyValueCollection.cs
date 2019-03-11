using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Utility
{
  public  class KeyValueCollection:IDictionary<string,string>
    {
      private Dictionary<string,string> _keyValue;
      public KeyValueCollection()
      {
          _keyValue =new Dictionary<string, string>();
      }
      public void Add(string key, string value)
      {
          _keyValue.Add(key, value);
      }

      public bool ContainsKey(string key)
      {
        return  _keyValue.ContainsKey(key);
      }

      public ICollection<string> Keys
      {
          get { return _keyValue.Keys; }
      }

      public bool Remove(string key)
      {
          return _keyValue.Remove(key);
      }

      public bool TryGetValue(string key, out string value)
      {
          return _keyValue.TryGetValue(key,out value);
      }

      public ICollection<string> Values
      {
          get { return _keyValue.Values; }
      }

      public string this[string key]
      {
          get
          {
              if (_keyValue.ContainsKey(key))
                  return _keyValue[key];
              return null;
          }
          set
          {
              if (_keyValue.ContainsKey(key))
                  _keyValue[key] = value;
              else
                  _keyValue.Add(key, value);
          }
      }

      public void Add(KeyValuePair<string, string> item)
      {
          _keyValue.Add(item.Key, item.Value);
      }

      public void Clear()
      {
          _keyValue.Clear();
      }

      public bool Contains(KeyValuePair<string, string> item)
      {
          return _keyValue.Contains(item);
      }

      public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
      {
          //_keyValue.CopyTo(array, arrayIndex);
      }

      public int Count
      {
          get {return _keyValue.Count; }
      }

      public bool IsReadOnly
      {
          get { return false; }
      }

      public bool Remove(KeyValuePair<string, string> item)
      {
         return _keyValue.Remove(item.Key);
      }

      public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
      {
          return _keyValue.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
          return _keyValue.GetEnumerator();
      }
    }
}
