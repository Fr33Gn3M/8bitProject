// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.IO;
using System.Reflection;
using System.Configuration;
using TH.ServerFramework.IO;
using TH.ServerFramework.Configuration;


namespace TH.ServerFramework
{
	namespace IO
	{
		internal class FileSystemProviderRepo : IFileSystemProviderRepo
		{
			
			private readonly System.Collections.Generic.IDictionary<string, IFileSystemProviderFactory> _providerFactories; //key=providerName
			private readonly IDictionary<string, string> _providerInstances; //key=instanceName,value=providerName
			private readonly string _defaultInstanceName;
			
			public FileSystemProviderRepo()
			{
				_providerFactories = new Dictionary<string, IFileSystemProviderFactory>();
				_providerInstances = new Dictionary<string, string>();
				_defaultInstanceName = SettingsSection.GetSection().ApplicationServices.FileSystemService.Instances.DefaultInstanceName;
			}
			
			public IFileSystemProvider GetProviderInstance(string name)
			{
				if (name == null)
				{
					name = _defaultInstanceName;
				}
				if (!_providerInstances.ContainsKey(name))
				{
					return null;
				}
				var providerName = _providerInstances[name];
				var factory = _providerFactories[providerName];
				var provider = factory.GetInstance(name);
				return provider;
			}
			
			public void RegisterProviderFactory(IFileSystemProviderFactory factory)
			{
				var providerName = factory.GetType().FullName;
				if (!_providerFactories.ContainsKey(providerName))
				{
					_providerFactories.Add(providerName, factory);
				}
			}
			
			public void RegisterProviderInstance(string name, string providerName, System.Collections.Generic.IDictionary<string, string> args)
			{
				if (!_providerFactories.ContainsKey(providerName))
				{
					throw (new NotSupportedException("providerName"));
				}
				var factory = _providerFactories[providerName];
				factory.CreateInstance(name, args);
				_providerInstances.Add(name, providerName);
				var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				SettingsSection section = (SettingsSection) (config.GetSection(SettingsSection.SectionName));
				var instanceElem = section.ApplicationServices.FileSystemService.Instances.GetInstance(name);
				section.ApplicationServices.FileSystemService.Instances.Remove(name);
				var elem = new FileSystemInstanceElem();
				elem.InstanceName = name;
				elem.ProviderName = providerName;
				if (args != null && args.Any())
				{
					elem.ProviderArguments = new WritableKeyValueConfigurationCollection();
					foreach (var argKv in args)
					{
						elem.ProviderArguments.Add((string) argKv.Key, (string) argKv.Value);
					}
				}
				section.ApplicationServices.FileSystemService.Instances.Add(elem);
				config.Save();
			}
			
			public string[] GetProviderNames()
			{
				return _providerFactories.Keys.ToArray();
			}
			
			public string[] GetProviderInstanceNames(string providerName)
			{
				var ls = new List<string>();
				foreach (var kv in _providerInstances)
				{
					if (kv.Value == providerName)
					{
						ls.Add(kv.Key);
					}
				}
				return ls.ToArray();
			}
			
			public void UnregisterProviderInstance(string name)
			{
				if (!_providerInstances.ContainsKey(name))
				{
					return ;
				}
				_providerInstances.Remove(name);
				var providerName = _providerInstances[name];
				var providerFactory = _providerFactories[providerName];
				providerFactory.RemoveInstance(name);
			}


            public string GetDefaultInstanceName()
            {
                return _defaultInstanceName;
            }

            public void SetDefaultInstanceName(string name)
            {
                if (!_providerInstances.ContainsKey(name))
                {
                    throw (new NotSupportedException("²»´æÔÚ"));
                }
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                SettingsSection section = (SettingsSection)(config.GetSection(SettingsSection.SectionName));
                var instanceElem = section.ApplicationServices.FileSystemService.Instances.DefaultInstanceName = name;
                config.Save();
            }
        }
	}
}
