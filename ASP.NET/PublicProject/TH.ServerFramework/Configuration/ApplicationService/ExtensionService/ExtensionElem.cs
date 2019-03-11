namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class ExtensionElem : ClassTypeElem
    {
        private const string BootstrapperPropName = "Bootstrapper";
        private const string LoadArgumentsPropName = "LoadArguments";
        private const string ServiceInstanceProvidersPropName = "ServiceInstanceProviders";

        [ConfigurationProperty(BootstrapperPropName, IsRequired = false)]
        public BootstrapperElem Bootstrapper
        {
            get
            {
                return (BootstrapperElem)this[BootstrapperPropName];
            }
            set
            {
                this[BootstrapperPropName] = value;
            }
        }

        private const string EnabledPropName = "enabled";
        [ConfigurationProperty(EnabledPropName)]
        public bool Enabled
        {
            get
            {
                return (bool)this[EnabledPropName];
            }
            set
            {
                this[EnabledPropName] = value;
            }
        }

        [ConfigurationProperty(LoadArgumentsPropName)]
        public WritableKeyValueConfigurationCollection LoadArguments
        {
            get
            {
                return (WritableKeyValueConfigurationCollection)this[LoadArgumentsPropName];
            }
            set
            {
                this[LoadArgumentsPropName] = value;
            }
        }

        [ConfigurationProperty(ServiceInstanceProvidersPropName, IsRequired = false)]
        public ServiceInstanceProviderCollElem ServiceInstanceProviders
        {
            get
            {
                return (ServiceInstanceProviderCollElem)this[ServiceInstanceProvidersPropName];
            }
            set
            {
                this[ServiceInstanceProvidersPropName] = value;
            }
        }
    }
}

