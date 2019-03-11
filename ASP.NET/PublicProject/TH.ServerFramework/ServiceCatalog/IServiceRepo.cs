namespace TH.ServerFramework.ServiceCatalog
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public interface IServiceRepo
    {
        IService CreateService(ServiceConfigurationBase serviceConfig);
        string[] FindService(string serviceNamePattern, RegexOptions regexOption, IDictionary<string, string> properties);
        IService GetService(string serviceName);
        bool HasService(string serviceName);
        void RegisterServiceSource(string name,IServiceConfigSource source);
    }
}

