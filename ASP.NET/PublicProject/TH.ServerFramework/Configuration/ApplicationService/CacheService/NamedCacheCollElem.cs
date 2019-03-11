namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(NamedCacheElem))]
    public class NamedCacheCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new NamedCacheElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            NamedCacheElem elem = (NamedCacheElem) element;
            return elem.CacheName;
        }
    }
}

