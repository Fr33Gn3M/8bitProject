using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching
{
    public interface ICacheFactory
    {
        void RegisterCacheProvider(ICacheFactory cacheFactory);
        bool HasCache(string cacheName);
        void CreateCache(CacheConfiguration configuration);
        ICache GetCache(string cacheName);
        string[] GetCacheNames();
    }

}
