namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ParameteredClassTypeElem : ClassTypeElem
    {
        private const string ParametersPropName = "Parameters";

        [ConfigurationProperty(ParametersPropName)]
        public KeyValueConfigurationCollection Parameters
        {
            get
            {
                return (KeyValueConfigurationCollection)this[ParametersPropName];
            }
            set
            {
                this[ParametersPropName] = value;
            }
        }
    }
}

