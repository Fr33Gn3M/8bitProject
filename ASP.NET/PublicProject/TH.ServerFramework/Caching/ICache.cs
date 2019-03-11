using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching
{
    public interface ICache
    {
        string CacheName { get; }
        object Get(string cacheKey, Func<object> valueFactory);
        object Get(string cacheKey);
        void Put(string cacheKey, object cacheValue, TimeSpan ttl);
        void Remove(string cacheKey);
        long GetCacheCount();
        bool IsExists(string cacheKey);
        IEnumerable<string> GetAllCacheKeys();
        void Reduce(double percent);
        void Reset();
        object this[string cacheKey] { get; }
        object this[string cacheKey, Func<object> valueFactory] { get; }
    }

}
