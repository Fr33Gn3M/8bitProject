namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class WebHandlerElem : ConfigurationElement
    {
        private const string WebHandlerTypePropName = "WebHandlerType";
        [ConfigurationProperty(WebHandlerTypePropName, IsKey = true)]
        public string WebHandlerType
        {
            get
            {
                return (string)this[WebHandlerTypePropName];
            }
            set
            {
                this[WebHandlerTypePropName] = value;
            }
        }

        private const string urlPathPatternPropName = "UrlPathPattern";
        [ConfigurationProperty(urlPathPatternPropName)]
        public string UrlPathPattern
        {
            get
            {
                return (string)this[urlPathPatternPropName];
            }
            set
            {
                this[urlPathPatternPropName] = value;
            }
        }

        public Type ClassType
        {
            get
            {
                return Type.GetType(this.WebHandlerType);
            }
            set
            {
                this.WebHandlerType = value.AssemblyQualifiedName;
            }
        }
    }
}

