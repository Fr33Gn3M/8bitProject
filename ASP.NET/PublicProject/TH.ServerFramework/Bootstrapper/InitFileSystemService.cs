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
using TH.ServerFramework.IO;
using TH.ServerFramework.Configuration;


namespace TH.ServerFramework
{
	namespace Bootstrapper
	{
		internal class InitFileSystemService : IStartupTask
		{
			
			public void Reset()
			{
				
			}
			
			public void Run()
			{
				var repo = ServiceLocator.Current.GetInstance<IFileSystemProviderRepo>();
				var fileSystemServiceElem = SettingsSection.GetSection().ApplicationServices.FileSystemService;
				if ((fileSystemServiceElem.Providers == null) || (fileSystemServiceElem.Instances == null))
				{
					return ;
				}
                var providerTypeDic = fileSystemServiceElem.Providers.GetAllClassTypes().ToDictionary(p => p.FullName);
				foreach (var providerType in providerTypeDic.Values.ToArray())
				{
                    IFileSystemProviderFactory provider = Activator.CreateInstance(providerType) as IFileSystemProviderFactory;
					if (provider == null)
					{
						throw (new ArgumentException());
					}
					repo.RegisterProviderFactory(provider);
				}
				foreach (FileSystemInstanceElem instanceElem in fileSystemServiceElem.Instances)
				{
					if (!providerTypeDic.ContainsKey(instanceElem.ProviderName))
					{
						throw (new ArgumentException("providerName"));
					}
					var instanceName = instanceElem.InstanceName;
					var providerName = instanceElem.ProviderName;
					IDictionary<string, string> providerArgs = null;
					if (instanceElem.ProviderArguments != null)
					{
						providerArgs = instanceElem.ProviderArguments.ToDictionary();
					}
					repo.RegisterProviderInstance(instanceName, providerName, providerArgs);
				}
			}
		}
	}
}
