namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class CacheServiceElem : ConfigurationElement
    {
        private const string NamedCachesPropName = "NamedCaches";
        private const string ProvidersPropName = "Providers";

        [ConfigurationProperty(NamedCachesPropName, IsRequired = false)]
        public NamedCacheCollElem NamedCaches
        {
            get
            {
                return (NamedCacheCollElem)this[NamedCachesPropName];
            }
            set
            {
                this[NamedCachesPropName] = value;
            }
        }

        [ConfigurationProperty(ProvidersPropName)]
        public ClassTypeCollElem Providers
        {
            get
            {
                return (ClassTypeCollElem)this[ProvidersPropName];
            }
            set
            {
                this[ProvidersPropName] = value;
            }
        }
    }
}

