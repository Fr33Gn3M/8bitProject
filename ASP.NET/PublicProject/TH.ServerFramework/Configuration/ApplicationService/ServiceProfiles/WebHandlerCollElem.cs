namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    [ConfigurationCollection(typeof(WebHandlerElem))]
    public class WebHandlerCollElem : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new WebHandlerElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            WebHandlerElem elem = (WebHandlerElem)element;
            return elem.WebHandlerType;
        }

    }
}

