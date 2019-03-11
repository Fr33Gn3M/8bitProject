namespace TH.ServerFramework.ServiceCatalog
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public interface IServiceConfigSource
    {
        string[] FindItem(string serviceNamePattern, RegexOptions regexOption, IDictionary<string, string> properties);
        ServiceConfigurationBase GetItem(string serviceName);
        bool HasItem(string serviceName);
        ServiceConfigurationBase InsertItem(ServiceConfigurationBase config);
        bool RemoveItem(string serviceName);
        void UpdateItem(string serviceName, ServiceConfigurationBase config);
    }
}

