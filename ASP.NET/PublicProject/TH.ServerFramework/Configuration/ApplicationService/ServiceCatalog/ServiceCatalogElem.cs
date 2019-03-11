namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ServiceCatalogElem : ConfigurationElement
    {
        private const string PredefinedServiceSourcePropName = "PredefinedServiceSource";
        private const string ServiceConfigSourcesPropName = "ServiceConfigSources";
        private const string ServicesPropName = "Services";

        [ConfigurationProperty(PredefinedServiceSourcePropName, IsRequired = false)]
        public PredefinedServiceCollElem PredefinedServiceSource
        {
            get
            {
                return (PredefinedServiceCollElem)this[PredefinedServiceSourcePropName];
            }
            set
            {
                this[PredefinedServiceSourcePropName] = value;
            }
        }

        [ConfigurationProperty(ServiceConfigSourcesPropName)]
        public ServiceConfigSourceCollElem ServiceConfigSources
        {
            get
            {
                return (ServiceConfigSourceCollElem)this[ServiceConfigSourcesPropName];
            }
            set
            {
                this[ServiceConfigSourcesPropName] = value;
            }
        }

        [ConfigurationProperty(ServicesPropName)]
        public ServiceCollElem Services
        {
            get
            {
                return (ServiceCollElem)this[ServicesPropName];
            }
            set
            {
                this[ServicesPropName] = value;
            }
        }
    }
}

