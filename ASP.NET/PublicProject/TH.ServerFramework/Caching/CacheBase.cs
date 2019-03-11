using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace TH.ServerFramework.Caching
{
    public abstract class CacheBase : ICache
    {

        private readonly string _cacheName;
        private readonly CacheConfiguration _cacheConfiguration;
       // private static ConcurrentDictionary<string, object> _cacheLockers = new ConcurrentDictionary<string, object>();

        private static ConcurrentDictionary<string, MemoryCache> _indexCaches = new ConcurrentDictionary<string, MemoryCache>();
        protected CacheBase(CacheConfiguration cacheConfiguration)
        {
            _cacheName = cacheConfiguration.CacheName;
            _cacheConfiguration = cacheConfiguration;
            //if (!_cacheLockers.ContainsKey(_cacheName))
            //{
             //   _cacheLockers.TryAdd(CacheName, new object());
          //  }
            if (!_indexCaches.ContainsKey(_cacheName))
            {
                var keyIndexCache = CreateKeyIndexCache(_cacheName);
                var indexCacheName = GetIndexCacheName(_cacheName);
                var indexCache = new MemoryCache(indexCacheName);
                _indexCaches.TryAdd(_cacheName, indexCache);
            }
        }

        private static string GetIndexCacheName(string cacheName)
        {
            cacheName = string.Format("_IndexCache#{0}", cacheName);
            return cacheName;
        }

        private static MemoryCache CreateKeyIndexCache(string cacheName)
        {
            var prefix = Guid.NewGuid().ToString().Replace("-", string.Empty);
            cacheName = string.Format("{0}_{1}", prefix, cacheName);
            var cache = new MemoryCache(cacheName);
            return cache;
        }

        private MemoryCache GetIndexCache()
        {
            dynamic cache = _indexCaches[_cacheName];
            return cache;
        }

        public string CacheName
        {
            get { return _cacheName; }
        }

        public abstract object Get(string cacheKey);

        public IEnumerable<string> GetAllCacheKeys()
        {
            var keys = new HashSet<string>();
            var indexCache = GetIndexCache();
            foreach (var item in indexCache)
            {
                keys.Add(item.Key);
            }
            return keys;
        }

        public object this[string cacheKey]
        {
            get { return Get(cacheKey); }
        }

        public bool IsExists(string cacheKey)
        {
            var indexCache = GetIndexCache();
            return indexCache.Contains(cacheKey);
        }

        protected abstract void InternalsPut(string cacheKey, object cacheValue, TimeSpan ttl);

        public void Put(string cacheKey, object cacheValue, TimeSpan ttl)
        {
            if ((cacheValue == null))
            {
                return;
            }
            if (ttl == TimeSpan.Zero)
            {
                ttl = _cacheConfiguration.DefaultCacheItemTtl;
            }
            InternalsPut(cacheKey, cacheValue, ttl);
            var indexCache = GetIndexCache();
            var indexItem = new CacheIndexItem
            {
                CreatedDate = System.DateTime.Now,
                LockerType = CacheLockerType.None
            };
            indexCache.Set(cacheKey, indexItem, new DateTimeOffset(System.DateTime.Now + ttl));
        }

        public void Reduce(double percent)
        {
            var indexCache = GetIndexCache();
            var removingCount = (int)Math.Floor(indexCache.GetCount() * percent);
            if (removingCount == 0)
            {
                return;
            }
            var remoingKeys = ((from e in indexCache select e.Key).Take(removingCount)).ToArray();
            foreach (var removingKey in remoingKeys)
            {
                Remove(removingKey);
            }
        }

        protected abstract void InternalsRemove(string cacheKey);

        public void Remove(string cacheKey)
        {
            var indexCache = GetIndexCache();
            indexCache.Remove(cacheKey);
            InternalsRemove(cacheKey);
        }

        public object Get(string cacheKey, Func<object> valueFactory)
        {
            if (IsExists(cacheKey))
            {
                return Get(cacheKey);
            }
            //var locker = _cacheLockers[_cacheName];
            //lock (locker)
            //{
                if (IsExists(cacheKey))
                {
                    return Get(cacheKey);
                }
                dynamic value = valueFactory.Invoke();
                Put(cacheKey, value,TimeSpan.Zero);
                return value;
           // }
        }

        public object this[string cacheKey, Func<object> valueFactory]
        {
            get { return Get(cacheKey, valueFactory); }
        }

        public void Reset()
        {
            Reduce(1f);
        }

        public long GetCacheCount()
        {
            var indexCache = GetIndexCache();
            return indexCache.GetCount();
        }
    }

}
