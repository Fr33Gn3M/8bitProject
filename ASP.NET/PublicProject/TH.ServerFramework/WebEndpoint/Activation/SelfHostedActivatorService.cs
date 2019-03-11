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
		public class SelfHostedActivatorService : ISelfHostedActivatorService
		{
			
			private readonly IDictionary<Type, ServiceHostBase> _serviceHosts;
			
			public SelfHostedActivatorService()
			{
				_serviceHosts = new Dictionary<Type, ServiceHostBase>();
			}
			
			public void Close(Type serviceType)
			{
				if (_serviceHosts.ContainsKey(serviceType))
				{
					var svcHost = _serviceHosts[serviceType];
					svcHost.Close();
					if (ServiceHostClosedEvent != null)
						ServiceHostClosedEvent(this, EventArgs.Empty);
				}
			}
			
			public void Open(Func<ServiceHostBase> factory)
			{
                var serviceHost = factory.Invoke();
                _serviceHosts.Add(serviceHost.Description.ServiceType, serviceHost);
                serviceHost.Open();
                if (ServiceHostOpenEvent != null)
                    ServiceHostOpenEvent(this, EventArgs.Empty);
			}
			
			public IDictionary<Type, Uri> GetServiceTypeUris()
			{
				var dic = new Dictionary<Type, Uri>();
				foreach (var svcHost in _serviceHosts.Values)
				{
					var serviceType = svcHost.Description.ServiceType;
					var serviceBaseUri = svcHost.BaseAddresses.First();
					dic.Add(serviceType, serviceBaseUri);
				}
				return dic;
			}
			
			private ServiceHostClosedEventHandler ServiceHostClosedEvent;
			
			public event ServiceHostClosedEventHandler ServiceHostClosed
			{
				add
				{
					ServiceHostClosedEvent = (ServiceHostClosedEventHandler) System.Delegate.Combine(ServiceHostClosedEvent, value);
				}
				remove
				{
					ServiceHostClosedEvent = (ServiceHostClosedEventHandler) System.Delegate.Remove(ServiceHostClosedEvent, value);
				}
			}
			
			
			private ServiceHostOpenEventHandler ServiceHostOpenEvent;
			
			public event ServiceHostOpenEventHandler ServiceHostOpen
			{
				add
				{
					ServiceHostOpenEvent = (ServiceHostOpenEventHandler) System.Delegate.Combine(ServiceHostOpenEvent, value);
				}
				remove
				{
					ServiceHostOpenEvent = (ServiceHostOpenEventHandler) System.Delegate.Remove(ServiceHostOpenEvent, value);
				}
			}


        
        }
	}
}
