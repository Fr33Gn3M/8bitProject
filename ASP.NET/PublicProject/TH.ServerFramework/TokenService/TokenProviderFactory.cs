// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.ServiceModel.Web;
using System.ServiceModel;
using Microsoft.Practices.ServiceLocation;
using TH.ServerFramework.Caching;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.TokenService;


namespace TH.ServerFramework
{
	namespace TokenService
	{
		internal class TokenProviderFactory : ITokenProviderFactory
		{
			
			private ICache GetCache(string cacheName)
			{
				var cacheFactory = ServiceLocator.Current.GetInstance<ICacheFactory>();
				var cache = cacheFactory.GetCache(cacheName);
				return cache;
			}
			
			private TimeSpan GetTokenTtl()
			{
				return SettingsSection.GetSection().ApplicationServices.TokenService.AdminTokenTtl;
			}
			
			public ITokenProvider CreateDefaultProvider()
			{
				if (OperationContext.Current != null)
				{
					return CreateProvider(OperationContext.Current);
				}
				else if (WebOperationContext.Current != null)
				{
					return CreateProvider(WebOperationContext.Current);
				}
				else
				{
					return null;
				}
			}
			
			public ITokenProvider CreateProvider(OperationContext oc)
			{
				var cacheName = SettingsSection.GetSection().ApplicationServices.TokenService.TokenCacheName;
				var cache = GetCache(cacheName);
				var tokenTtl = GetTokenTtl();
				var provider = new OperationContextTokenProvider(oc, cache, tokenTtl);
				return provider;
			}
			
			public ITokenProvider CreateProvider(WebOperationContext woc)
			{
				throw (new NotImplementedException());
			}
		}
	}
}
