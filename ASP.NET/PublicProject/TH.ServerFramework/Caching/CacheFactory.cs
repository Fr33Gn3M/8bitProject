// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
using TH.ServerFramework.Caching;
// End of VB project level imports


namespace TH.ServerFramework
{
	namespace Caching
	{
		internal class CacheFactory : ICacheFactory
		{
			
			private readonly IDictionary<string, ICacheFactory> _cacheProviders;
			private readonly System.Collections.Generic.IDictionary<string, string> _namedCaches;
			
			public CacheFactory()
			{
				_cacheProviders = new Dictionary<string, ICacheFactory>();
				_namedCaches = new Dictionary<string, string>();
			}
			
			public void RegisterCacheProvider(ICacheFactory cacheFactory)
			{
				var providerType = cacheFactory.GetType();
				_cacheProviders.Add(providerType.FullName, cacheFactory);
			}
			
			public void CreateCache(CacheConfiguration configuration)
			{
				var cacheProvider = _cacheProviders[configuration.CacheProviderName];
				cacheProvider.CreateCache(configuration);
                _namedCaches.Add(configuration.CacheName, configuration.CacheProviderName);
			}
			
			public ICache GetCache(string cacheName)
			{
				if (!_namedCaches.ContainsKey(cacheName))
				{
					return null;
				}
				var cacheProviderName = _namedCaches[cacheName];
				var cacheProvider = _cacheProviders[cacheProviderName];
				var cache = cacheProvider.GetCache(cacheName);
				return cache;
			}
			
			public bool HasCache(string cacheName)
			{
				if (!_namedCaches.ContainsKey(cacheName))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			
			public string[] GetCacheNames()
			{
				return _namedCaches.Keys.ToArray();
			}
		}
	}
	
}
