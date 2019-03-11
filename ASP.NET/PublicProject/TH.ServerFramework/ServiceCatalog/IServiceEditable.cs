namespace TH.ServerFramework.ServiceCatalog
{
    using System;

    public interface IServiceEditable
    {
        ServiceConfigurationBase Create(ServiceConfigurationBase config);
        void Remove();
        void Update(ServiceConfigurationBase config);
    }
}

