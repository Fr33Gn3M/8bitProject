namespace TH.ServerFramework.WebEndpoint
{
    using ServerFramework.ServiceCatalog;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text.RegularExpressions;

    [ServiceContract]
    public interface IServiceRepoService
    {

        [OperationContract]
        void CreateService(Guid token, ServiceConfigurationBase configuration);
        [OperationContract]
        string[] FindService(Guid token, string serviceNamePattern, RegexOptions regexOption, IDictionary<string, string> properties);
        [OperationContract]
        ServiceConfigurationBase GetServiceConfiguration(Guid token, string serviceName);
        [OperationContract]
        void RemoveService(Guid token, string serviceName);
        [OperationContract]
        void UpdateServiceConfiguration(Guid token, string serviceName, ServiceConfigurationBase configuration);
    }
}

