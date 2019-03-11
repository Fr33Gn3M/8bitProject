using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching.MockCacheProvider
{
    internal class MockCache : ICache
    {
        private readonly string _cacheName;
        public MockCache(string cacheName)
        {
            _cacheName = cacheName;
        }

        public string CacheName
        {
            get { return _cacheName; }
        }

        public object Get(string cacheKey)
        {
            return null;
        }

        public object Get(string cacheKey, Func<object> valueFactory)
        {
            dynamic value = valueFactory.Invoke();
            return value;
        }

        public IEnumerable<string> GetAllCacheKeys()
		{
			return null;
		}

        public object this[string cacheKey]
        {
            get { return null; }
        }

        public object this[string cacheKey, Func<object> valueFactory]
        {
            get
            {
                dynamic value = valueFactory.Invoke();
                return value;
            }
        }

        public bool IsExists(string cacheKey)
        {
            return false;
        }


        public void Put(string cacheKey, object cacheValue, TimeSpan ttl)
        {
        }


        public void Reduce(double percent)
        {
        }


        public void Remove(string cacheKey)
        {
        }


        public void Reset()
        {
        }

        public long GetCacheCount()
        {
            return 0L;
        }
    }

}
