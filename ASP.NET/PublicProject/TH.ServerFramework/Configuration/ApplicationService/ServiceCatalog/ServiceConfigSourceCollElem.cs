namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ServiceConfigSourceElem))]
    public class ServiceConfigSourceCollElem : ConfigurationElementCollection
    {
        private const string CacheNamePropName = "cacheName";

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceConfigSourceElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceConfigSourceElem elem = (ServiceConfigSourceElem) element;
            return elem.Name;
        }

        public ServiceConfigSourceElem GetItem(string name)
        {
            return (ServiceConfigSourceElem) this.BaseGet(name);
        }

        [ConfigurationProperty(CacheNamePropName)]
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
    }
}

