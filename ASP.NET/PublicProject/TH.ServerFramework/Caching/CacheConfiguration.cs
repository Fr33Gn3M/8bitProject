using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching
{
    public class CacheConfiguration
    {
        public string CacheProviderName { get; set; }
        public string CacheName { get; set; }
        public TimeSpan DefaultCacheItemTtl { get; set; }
        public IDictionary<string, string> Properties { get; set; }
    }

}
