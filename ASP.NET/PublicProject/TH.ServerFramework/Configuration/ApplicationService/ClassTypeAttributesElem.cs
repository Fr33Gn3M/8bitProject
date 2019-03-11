namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ClassTypeAttributesElem : ClassTypeExElem
    {
        private const string AttributesPropName = "Attributes";

        [ConfigurationProperty(AttributesPropName)]
        public WritableKeyValueConfigurationCollection Attributes
        {
            get
            {
                return (WritableKeyValueConfigurationCollection)this[AttributesPropName];
            }
            set
            {
                this[AttributesPropName] = value;
            }
        }
    }
}

