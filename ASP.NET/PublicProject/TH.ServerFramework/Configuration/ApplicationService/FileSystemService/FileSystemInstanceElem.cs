namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class FileSystemInstanceElem : ConfigurationElement
    {
        private const string InstanceNamePropName = "insanceName";
        private const string ProviderArgumentsPropName = "ProviderArguments";
        private const string ProviderNamePropName = "providerName";

        [ConfigurationProperty(InstanceNamePropName, IsKey = true)]
        public string InstanceName
        {
            get
            {
                return (string)this[InstanceNamePropName];
            }
            set
            {
                this[InstanceNamePropName] = value;
            }
        }

        [ConfigurationProperty(ProviderArgumentsPropName, IsRequired = false)]
        public WritableKeyValueConfigurationCollection ProviderArguments
        {
            get
            {
                return (WritableKeyValueConfigurationCollection)this[ProviderArgumentsPropName];
            }
            set
            {
                this[ProviderArgumentsPropName] = value;
            }
        }

        [ConfigurationProperty(ProviderNamePropName)]
        public string ProviderName
        {
            get
            {
                return (string)this[ProviderNamePropName];
            }
            set
            {
                this[ProviderNamePropName] = value;
            }
        }
    }
}

