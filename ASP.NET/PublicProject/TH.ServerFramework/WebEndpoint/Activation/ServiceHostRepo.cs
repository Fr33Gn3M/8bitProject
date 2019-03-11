// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.ServiceModel;
using System.ServiceModel.Web;
using TH.ServerFramework.WebEndpoint.Activation;


namespace TH.ServerFramework
{
    namespace WebEndpoint.Activation
	{
        public class ServiceHostRepo : IServiceHostRepo
		{
			
			private readonly System.Collections.Generic.IList<Func<IDictionary<Type, Uri>>> _serviceTypeUrlSources;
			
			public ServiceHostRepo()
			{
				_serviceTypeUrlSources = new List<Func<IDictionary<Type, Uri>>>();
			}
			
			public Uri GetBaseUrl(Type serviceType)
			{
				foreach (var source in _serviceTypeUrlSources)
				{
					var typeUrls = source.Invoke();
					if (typeUrls.ContainsKey(serviceType))
					{
						return typeUrls[serviceType];
					}
				}
				return null;
			}
			
			public Type[] GetServiceTypes()
			{
				var hs = new System.Collections.Generic.HashSet<Type>();
				foreach (var source in _serviceTypeUrlSources)
				{
					var typeUrls = source.Invoke();
					foreach (var serviceType in typeUrls.Keys)
					{
						hs.Add(serviceType);
					}
				}
				return hs.ToArray();
			}
			
			public void AddSource(Func<IDictionary<Type, Uri>> serviceTypeUrlSource)
			{
				_serviceTypeUrlSources.Add(serviceTypeUrlSource);
			}
		}
	}
}
