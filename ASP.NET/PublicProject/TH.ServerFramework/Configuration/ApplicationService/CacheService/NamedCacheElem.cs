namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class NamedCacheElem : ConfigurationElement
    {
        private const string CacheNamePropName = "cacheName";
        private const string CachePropertiesPropName = "CacheProperties";
        private const string DefaultCacheItemTtlPropName = "defaultCacheItemTtl";
        private const string ProviderNamePropName = "providerName";

        [ConfigurationProperty(CacheNamePropName, IsKey = true)]
        public string CacheName
        {
            get
            {
                return (string)this[CacheNamePropName];
            }
            set
            {
                this[CacheNamePropName] = value;
            }
        }

        [ConfigurationProperty(CachePropertiesPropName, IsRequired = false)]
        public WritableKeyValueConfigurationCollection CacheProperties
        {
            get
            {
                return (WritableKeyValueConfigurationCollection)this[CachePropertiesPropName];
            }
            set
            {
                this[CachePropertiesPropName] = value;
            }
        }

        [ConfigurationProperty(DefaultCacheItemTtlPropName)]
        public TimeSpan DefaultCacheItemTtl
        {
            get
            {
                return (TimeSpan)this[DefaultCacheItemTtlPropName];
            }
            set
            {
                this[DefaultCacheItemTtlPropName] = value;
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

