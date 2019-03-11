// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Configuration;
using Microsoft.Practices.ServiceLocation;
using Bootstrap.Extensions.StartupTasks;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.Extensions;
using TH.ServerFramework.Utility;


namespace TH.ServerFramework
{
    namespace Extensions
	{
		internal class ExtensionRepository : IExtensionRepository
		{
			
			private readonly System.Collections.Generic.IList<LoadedExtensionContext> _loadedExtensionContexts;
			private StartupTaskLoader _startupTaskLoader;
			
			public ExtensionRepository()
			{
				_loadedExtensionContexts = new List<LoadedExtensionContext>();
			}
			
			public ExtensionConfiguration[] GetExtensionConfigurations()
			{
				var section = SettingsSection.GetSection();
				var extensionServiceElem = section.ApplicationServices.ExtensionService;
				var ls = new List<ExtensionConfiguration>();
				foreach (ExtensionElem extensionCfgElem in extensionServiceElem)
				{
					var extensionCfg = new ExtensionConfiguration();
					extensionCfg.Enabled = extensionCfgElem.Enabled;
					extensionCfg.ExtensionName = extensionCfgElem.ClassType.FullName;
					if (extensionCfgElem.LoadArguments != null)
					{
						extensionCfg.LoadArguments = extensionCfgElem.LoadArguments.ToDictionary();
					}
					ls.Add(extensionCfg);
				}
				return ls.ToArray();
			}
			
			public string[] GetLoadedExtensionNames()
			{
				return _loadedExtensionContexts.Select(p => p.Name).ToArray();
			}
			
			public void LoadAll()
			{
				var appService = SettingsSection.GetSection().ApplicationServices;
				if (appService.ExtensionService == null)
				{
					return ;
				}
				var extensionServiceElem = appService.ExtensionService;
				_startupTaskLoader = CreateStartupTaskLoader(extensionServiceElem.Bootstrapper);
				_startupTaskLoader.LoadAll();
				foreach (ExtensionElem extensionCfgElem in extensionServiceElem)
				{
					LoadExtension(extensionCfgElem);
				}
			}
			
			private void LoadExtension(ExtensionElem extensionConfigElem)
			{
				if (!extensionConfigElem.Enabled)
				{
					return ;
				}
				RegisterServiceLocatorInstances(extensionConfigElem);
				var extensionLoader = CreateStartupTaskLoader(extensionConfigElem.Bootstrapper);
				extensionLoader.LoadAll();
				var extensionName = extensionConfigElem.ClassType.FullName;
				var locator = ServiceLocator.Current;
				IDictionary<string, string> loadArgs = null;
				if (extensionConfigElem.LoadArguments != null)
				{
					loadArgs = extensionConfigElem.LoadArguments.ToDictionary();
				}
                IExtension ext = Activator.CreateInstance(extensionConfigElem.ClassType) as IExtension;
				ext.OnLoad(loadArgs, locator);
				var ctx = new LoadedExtensionContext();
				ctx.Extension = ext;
				ctx.LoadArguments = loadArgs;
				ctx.Name = extensionName;
				ctx.ServiceLocator = locator;
				ctx.TaskLoader = extensionLoader;
				_loadedExtensionContexts.Add(ctx);
			}
			
			private void RegisterServiceLocatorInstances(ExtensionElem extensionElem)
			{
				if (extensionElem.ServiceInstanceProviders == null)
				{
					return ;
				}
				foreach (ServiceInstanceProviderElem elem in extensionElem.ServiceInstanceProviders)
				{
					var serviceType = elem.ServiceType.ClassType;
					var serviceInstanceType = elem.ServiceInstanceType.ClassType;
					var serviceInstance = Activator.CreateInstance(serviceInstanceType);
					MyServiceLocatorProvider.RegisterInstance(serviceType, serviceInstance);
				}
			}
			
			private StartupTaskLoader CreateStartupTaskLoader(BootstrapperElem bootstraperElem)
			{
				if (bootstraperElem == null)
				{
					return new StartupTaskLoader();
				}
				var startupTasks = CreateInstances<IStartupTask>(bootstraperElem.StartupTasks);
				var shutdownTasks = CreateInstances<IStartupTask>(bootstraperElem.ShutDownTasks);
				var loader = new StartupTaskLoader(startupTasks);
				loader.SetResetTasks(shutdownTasks);
				return loader;
			}
			
			private T[] CreateInstances<T>(ClassTypeCollElem classTypeCollElem)
			{
				var ls = new List<T>();
				if (classTypeCollElem == null)
				{
					return null;
				}
				var types = classTypeCollElem.GetAllClassTypes();
				foreach (var t in types)
				{
					T instance = (T)Activator.CreateInstance(t);
					if (instance == null)
					{
						continue;
					}
					ls.Add(instance);
				}
				return ls.ToArray();
			}
			
			public void SetExtensionConfiguration(ExtensionConfiguration config)
			{
				var section = SettingsSection.GetSection();
				var extensionElem = section.ApplicationServices.ExtensionService.GetExtension((string) config.ExtensionName);
				extensionElem.Enabled = System.Convert.ToBoolean(config.Enabled);
				if (config.LoadArguments == null)
				{
					extensionElem.LoadArguments = null;
				}
				else
				{
                    extensionElem.LoadArguments = config.LoadArguments.ToKeyValueConfigurationCollection() as WritableKeyValueConfigurationCollection;
				}
				var appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				appConfig.Save();
			}
			
			public void UnloadAll()
			{
				if (_startupTaskLoader == null)
				{
					return ;
				}
				var unloadOrder = _loadedExtensionContexts.Reverse().ToList();
				for (var i = 0; i <= unloadOrder.Count - 1; i++)
				{
					var extensionCtx = unloadOrder[i];
					extensionCtx.Extension.OnUnload();
					extensionCtx.TaskLoader.ResetAll();
					_loadedExtensionContexts.Remove(extensionCtx);
				}
				_startupTaskLoader.ResetAll();
			}
			
			private class LoadedExtensionContext
			{
				public string Name {get; set;}
				public IExtension Extension {get; set;}
				public StartupTaskLoader TaskLoader {get; set;}
				public IDictionary<string, string> LoadArguments {get; set;}
				public IServiceLocator ServiceLocator {get; set;}
			}
		}
	}
}
