namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ServiceProfileElem : ConfigurationElement
    {
        private const string ServiceProfileNamePropName = "ServiceProfileName";
        [ConfigurationProperty(ServiceProfileNamePropName)]
        public string ServiceProfileName
        {
            get
            {
                return (string)this[ServiceProfileNamePropName];
            }
            set
            {
                this[ServiceProfileNamePropName] = value;
            }
        }

        private const string ServiceProfileTitlePropName = "ServiceProfileTitle";
        [ConfigurationProperty(ServiceProfileTitlePropName)]
        public string ServiceProfileTitle
        {
            get
            {
                return (string)this[ServiceProfileTitlePropName];
            }
            set
            {
                this[ServiceProfileTitlePropName] = value;
            }
        }

        private const string  ServiceProfileInstancePropName = "ServiceProfileInstance";
        [ConfigurationProperty(ServiceProfileInstancePropName)]
        public string  ServiceProfileInstance
        {
            get
            {
                return (string)this[ServiceProfileInstancePropName];
            }
            set
            {
                this[ServiceProfileInstancePropName] = value;
            }
        }

        private const string WebHandlersPropName = "WebHandlers";
        [ConfigurationProperty(WebHandlersPropName)]
        public WebHandlerCollElem WebHandlers
        {
            get
            {
                return (WebHandlerCollElem)this[WebHandlersPropName];
            }
            set
            {
                this[WebHandlersPropName] = value;
            }
        }

        public Type ClassType
        {
            get
            {
                return Type.GetType(this.ServiceProfileInstance);
            }
            set
            {
                this.ServiceProfileInstance = value.AssemblyQualifiedName;
            }
        }
    }
}

