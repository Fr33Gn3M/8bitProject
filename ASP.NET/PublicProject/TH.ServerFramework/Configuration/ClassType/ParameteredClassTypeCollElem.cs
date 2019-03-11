namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ParameteredClassTypeElem))]
    public class ParameteredClassTypeCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ParameteredClassTypeElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ParameteredClassTypeElem elem = (ParameteredClassTypeElem) element;
            return elem.ClassTypeName;
        }
    }
}

