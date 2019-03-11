using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching.MockCacheProvider
{
    public class MockCacheFactory : ICacheFactory
    {
        public void CreateCache(CacheConfiguration configuration)
        {
        }

        public ICache GetCache(string cacheName)
        {
            return new MockCache(cacheName);
        }

        public bool HasCache(string cacheName)
        {
            return true;
        }

        public string[] GetCacheNames()
		{
			return null;
		}

        public void RegisterCacheProvider(ICacheFactory cacheFactory)
        {
            cacheFactory.RegisterCacheProvider(this);
        }
    }

}
