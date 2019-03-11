// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Microsoft.Practices.ServiceLocation;


namespace TH.ServerFramework.Extensions
{
	public class MyServiceLocatorProvider : IServiceLocator
	{
		private static readonly IDictionary<Type, object> _registeredServiceInstances = new Dictionary<Type, object>();
		
		public static void RegisterInstance(Type serviceType, object serviceInstance)
		{
			if (_registeredServiceInstances.ContainsKey(serviceType))
			{
				_registeredServiceInstances[serviceType] = serviceInstance;
			}
			else
			{
				_registeredServiceInstances.Add(serviceType, serviceInstance);
			}
		}
		
		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			if (!_registeredServiceInstances.ContainsKey(serviceType))
			{
                return null;
			}
			else
			{
				var serviceInstance = _registeredServiceInstances[serviceType];
				return new[] {serviceInstance};
			}
		}
		
		public IEnumerable<TService> GetAllInstances<TService>()
		{
			var serviceType = typeof(TService);
            var allInstances = GetAllInstances(serviceType);
            foreach (var item in allInstances)
            {
                yield return (TService)item;
            }
		}
		
		public object GetInstance(Type serviceType)
		{
			if (_registeredServiceInstances.ContainsKey(serviceType))
			{
				return _registeredServiceInstances[serviceType];
			}
			else
			{
				return null;
			}
		}
		
		public object GetInstance(Type serviceType, string key)
		{
			throw (new NotSupportedException());
		}
		
		public TService GetInstance<TService>()
		{
			var serviceType = typeof(TService);
            return (TService)GetInstance(serviceType);
		}
		
		public TService GetInstance<TService>(string key)
		{
			throw (new NotSupportedException());
		}
		
		public object GetService(Type serviceType)
		{
			return GetInstance(serviceType);
		}
	}
}
