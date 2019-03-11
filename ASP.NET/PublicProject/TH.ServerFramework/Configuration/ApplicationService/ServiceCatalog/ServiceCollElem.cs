namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ServiceElem))]
    public class ServiceCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceElem elem = (ServiceElem) element;
            return elem.ClassTypeName;
        }

        public ServiceElem GetItem(Type type)
        {
            int count = this.Count - 1;
            for (int i = 0; i <= count; i++)
            {
                ServiceElem elem = (ServiceElem) this.BaseGet(i);
                if (elem.ClassType.Equals(type))
                {
                    return elem;
                }
            }
            return null;
        }
    }
}

