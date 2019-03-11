namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections;
    using System.Configuration;

    [ConfigurationCollection(typeof(PredefinedServiceElem))]
    public class PredefinedServiceCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PredefinedServiceElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            PredefinedServiceElem elem = (PredefinedServiceElem) element;
            return elem.ServiceName;
        }

        public PredefinedServiceElem GetService(string serviceName)
        {
            IEnumerator list= null;
            try
            {
                list = this.GetEnumerator();
                while (list.MoveNext())
                {
                    PredefinedServiceElem elem = (PredefinedServiceElem) list.Current;
                    if (elem.ServiceName == serviceName)
                    {
                        return elem;
                    }
                }
            }
            finally
            {
                if (list is IDisposable)
                {
                    (list as IDisposable).Dispose();
                }
            }
            return null;
        }
    }
}

