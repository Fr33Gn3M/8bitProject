namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class StringElem : ConfigurationElement
    {
        private const string ValuePropName = "value";

        [ConfigurationProperty(ValuePropName, IsKey = true)]
        public string Value
        {
            get
            {
                return (string)this[ValuePropName];
            }
            set
            {
                this[ValuePropName] = value;
            }
        }
    }
}

