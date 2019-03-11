namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class StringCollElem : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new StringElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            StringElem elem = (StringElem) element;
            return elem.Value;
        }

        public string[] GetValues()
        {
            List<string> ls = new List<string>();
            int count = this.Count - 1;
            for (int i = 0; i <= count; i++)
            {
                StringElem elem = (StringElem) this.BaseGet(i);
                ls.Add(elem.Value);
            }
            return ls.ToArray();
        }
    }
}

