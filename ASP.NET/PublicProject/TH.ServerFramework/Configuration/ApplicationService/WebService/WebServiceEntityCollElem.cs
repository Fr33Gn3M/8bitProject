namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    [ConfigurationCollection(typeof(WebServiceEntityElem))]
    public class WebServiceEntityCollElem : ConfigurationElementCollection
    {
        private const string BaseUrlPropName = "baseUrl";

        protected override ConfigurationElement CreateNewElement()
        {
            return new WebServiceEntityElem();
        }

        public IDictionary<Type, Uri> GetAllEntities()
        {
            Dictionary<Type, Uri> dic = new Dictionary<Type, Uri>();
            int list = this.Count - 1;
            for (int i = 0; i <= list; i++)
            {
                WebServiceEntityElem elem = (WebServiceEntityElem) this.BaseGet(i);
                Type serviceType = Type.GetType(elem.ServiceName);
                Uri url = this.GetUrl(elem);
                dic.Add(serviceType, url);
            }
            return dic;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            WebServiceEntityElem elem = (WebServiceEntityElem) element;
            return elem.ServiceName;
        }

        private Uri GetUrl(WebServiceEntityElem entity)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
            {
                return new Uri(entity.BaseUrl);
            }
            return new Uri(string.Format("{0}/{1}", this.BaseUrl.TrimEnd(new char[] { '/' }), entity.BaseUrl));
        }

        [ConfigurationProperty(BaseUrlPropName, DefaultValue = null)]
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
    }
}

