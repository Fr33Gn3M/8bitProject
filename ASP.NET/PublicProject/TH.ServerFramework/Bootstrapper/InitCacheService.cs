// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Bootstrap.Extensions.StartupTasks;
using Microsoft.Practices.ServiceLocation;
using TH.ServerFramework.Utility;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.Caching;


namespace TH.ServerFramework
{
	namespace Bootstrapper
	{
		internal class InitCacheService : IStartupTask
		{
			
			public void Reset()
			{
				
			}
			
			public void Run()
			{
				var cacheServiceElem = SettingsSection.GetSection().ApplicationServices.CacheService;
				if ((cacheServiceElem.Providers == null) || (cacheServiceElem.NamedCaches == null))
				{
					return ;
				}
				var cacheFactory = ServiceLocator.Current.GetInstance<ICacheFactory>();
                var providerTypes = cacheServiceElem.Providers.GetAllClassTypes().ToDictionary(p => p.FullName);
				foreach (var providerType in providerTypes.Values)
				{
                    ICacheFactory cacheProvider = Activator.CreateInstance(providerType) as ICacheFactory;
					cacheFactory.RegisterCacheProvider(cacheProvider);
				}
				foreach (NamedCacheElem namedCache in cacheServiceElem.NamedCaches)
				{
					if (!providerTypes.ContainsKey(namedCache.ProviderName))
					{
						throw (new ArgumentException("providerName"));
					}
					var cacheConfig = new CacheConfiguration();
					cacheConfig.CacheName = namedCache.CacheName;
					cacheConfig.CacheProviderName = namedCache.ProviderName;
					cacheConfig.DefaultCacheItemTtl = namedCache.DefaultCacheItemTtl;
					cacheConfig.Properties = namedCache.CacheProperties.ToDictionary();
					cacheFactory.CreateCache(cacheConfig);
				}
			}
		}
	}
}
