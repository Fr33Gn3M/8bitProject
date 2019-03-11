namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ServiceInstanceProviderElem))]
    public class ServiceInstanceProviderCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceInstanceProviderElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceInstanceProviderElem elem = (ServiceInstanceProviderElem) element;
            return elem.ServiceType.ClassType.FullName;
        }
    }
}

