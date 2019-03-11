namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ServiceInstanceProviderElem : ConfigurationElement
    {
        private const string ServiceInstanceTypePropName = "ServiceInstanceType";
        private const string ServiceTypePropName = "ServiceType";

        [ConfigurationProperty(ServiceInstanceTypePropName)]
        public ClassTypeExElem ServiceInstanceType
        {
            get
            {
                return (ClassTypeExElem)this[ServiceInstanceTypePropName];
            }
            set
            {
                this[ServiceInstanceTypePropName] = value;
            }
        }

        [ConfigurationProperty(ServiceTypePropName, IsKey = true)]
        public ClassTypeExElem ServiceType
        {
            get
            {
                return (ClassTypeExElem)this[ServiceTypePropName];
            }
            set
            {
                this[ServiceTypePropName] = value;
            }
        }
    }
}

