namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class WebServiceElem : ConfigurationElement
    {
        private const string WebServiceEntitiesPropName = "WebServiceEntities";

        [ConfigurationProperty(WebServiceEntitiesPropName)]
        public WebServiceEntityCollElem WebServiceEntities
        {
            get
            {
                return (WebServiceEntityCollElem)this[WebServiceEntitiesPropName];
            }
            set
            {
                this[WebServiceEntitiesPropName] = value;
            }
        }
    }
}

