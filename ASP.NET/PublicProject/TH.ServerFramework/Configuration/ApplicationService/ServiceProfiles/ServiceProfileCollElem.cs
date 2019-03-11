namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    [ConfigurationCollection(typeof(ServiceProfileElem))]
    public class ServiceProfileCollElem : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceProfileElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceProfileElem elem = (ServiceProfileElem)element;
            return elem.ServiceProfileName;
        }

    }
}

