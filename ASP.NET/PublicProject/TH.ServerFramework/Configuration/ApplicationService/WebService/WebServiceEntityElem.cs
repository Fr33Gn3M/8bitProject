namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class WebServiceEntityElem : ConfigurationElement
    {
        private const string BaseUrlPropName = "baseUrl";
        private const string ServiceNamePropName = "serviceName";

        [ConfigurationProperty(BaseUrlPropName)]
        public string BaseUrl
        {
            get
            {
                return (string)this[BaseUrlPropName];
            }
            set
            {
                this[BaseUrlPropName] = value;
            }
        }

        [ConfigurationProperty(ServiceNamePropName, IsKey = true)]
        public string ServiceName
        {
            get
            {
                return (string)this[ServiceNamePropName];
            }
            set
            {
                this[ServiceNamePropName] = value;
            }
        }
    }
}

