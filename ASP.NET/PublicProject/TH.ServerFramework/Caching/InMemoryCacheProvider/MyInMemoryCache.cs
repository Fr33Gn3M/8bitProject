using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace TH.ServerFramework.Caching.InMemoryCacheProvider
{
    internal class MyInMemoryCache : CacheBase
    {
        private readonly MemoryCache _cache;
        public MyInMemoryCache(CacheConfiguration cacheConfiguration)
            : base(cacheConfiguration)
        {
            var cacheName = GetCacheName(cacheConfiguration.CacheName);
            _cache = new MemoryCache(cacheName);
        }

        private string GetCacheName(string cacheName)
        {
            dynamic prefix = Guid.NewGuid().ToString().Replace("-", string.Empty);
            cacheName = string.Format("{0}_{1}", prefix, cacheName);
            return cacheName;
        }

        public override object Get(string cacheKey)
        {
            return _cache[cacheKey];
        }

        protected override void InternalsPut(string cacheKey, object cacheValue, TimeSpan ttl)
        {
            _cache.Set(cacheKey, cacheValue, new DateTimeOffset(System.DateTime.Now + ttl));
        }

        protected override void InternalsRemove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }

}
