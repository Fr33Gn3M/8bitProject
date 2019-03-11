namespace TH.ServerFramework.ServiceCatalog
{
    using System;

    public interface IServiceTrigger
    {
        void OnAfterGet(ServiceConfigurationBase serviceConfig);
        void OnAfterInster(ServiceConfigurationBase config);
        void OnAfterRemove(ServiceConfigurationBase serviceConfig);
        void OnAfterUpdate(string serviceName, ServiceConfigurationBase config);
        void OnBeforeGet(string serviceName);
        void OnBeforeInsert(ServiceConfigurationBase config);
        void OnBeforeRemove(string serviceName);
        void OnBeforeUpdate(string serviceName, ServiceConfigurationBase config);
    }
}

