namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(KeyValueConfigurationElement))]
    public class WritableKeyValueConfigurationCollection : KeyValueConfigurationCollection
    {
        public override bool IsReadOnly()
        {
            return false;
        }
    }
}

