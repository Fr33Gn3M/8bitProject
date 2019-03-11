namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ServiceCommandElem))]
    public class ServiceCommandCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceCommandElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceCommandElem elem = (ServiceCommandElem) element;
            return elem.ClassTypeName;
        }

        public ServiceCommandElem GetItem(Type type)
        {
            int count = this.Count - 1;
            for (int i = 0; i <= count; i++)
            {
                ServiceCommandElem elem = (ServiceCommandElem) this.BaseGet(i);
                if (elem.ClassType.Equals(type))
                {
                    return elem;
                }
            }
            return null;
        }
    }
}

