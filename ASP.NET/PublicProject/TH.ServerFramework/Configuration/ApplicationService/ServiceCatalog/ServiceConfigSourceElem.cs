namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class ServiceConfigSourceElem : ConfigurationElement
    {
        private const string CreationPropertiesPropName = "CreateionProperties";
        private const string NamePropName = "name";
        private const string SourceTypePropName = "sourceType";

        [ConfigurationProperty(CreationPropertiesPropName)]
        public WritableKeyValueConfigurationCollection CreationProperties
        {
            get
            {
                return (WritableKeyValueConfigurationCollection)this[CreationPropertiesPropName];
            }
            set
            {
                this[CreationPropertiesPropName] = value;
            }
        }

        [ConfigurationProperty(NamePropName, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this[NamePropName];
            }
            set
            {
                this[NamePropName] = value;
            }
        }

        [ConfigurationProperty(SourceTypePropName)]
        public string SourceType
        {
            get
            {
                return (string)this[SourceTypePropName];
            }
            set
            {
                this[SourceTypePropName] = value;
            }
        }
    }
}

