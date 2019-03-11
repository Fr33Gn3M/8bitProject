using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching.InMemoryCacheProvider
{
    public class InMeoryCacheFactory : ICacheFactory
    {
        private readonly IDictionary<string, MyInMemoryCache> _caches;
        public InMeoryCacheFactory()
        {
            _caches = new Dictionary<string, MyInMemoryCache>();
        }

        public void CreateCache(CacheConfiguration configuration)
        {
            dynamic cache = new MyInMemoryCache(configuration);
            _caches.Add(configuration.CacheName, cache);
        }

        public ICache GetCache(string cacheName)
        {
            if (_caches.ContainsKey(cacheName))
            {
                return _caches[cacheName] as ICache;
            }
            return null;
        }

        public bool HasCache(string cacheName)
        {
            return _caches.ContainsKey(cacheName);
        }

        public string[] GetCacheNames()
        {
            return _caches.Keys.ToArray();
        }

        public void RegisterCacheProvider(ICacheFactory cacheFactory)
        {
            cacheFactory.RegisterCacheProvider(this);
        }
    }

}
