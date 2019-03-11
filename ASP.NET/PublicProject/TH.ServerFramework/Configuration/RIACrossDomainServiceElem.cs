namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class RIACrossDomainServiceElem : ConfigurationElement
    {
        private const string CrossDomainFilePropName = "crossDomainFile";
        private const string EnabledPropName = "enabled";

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty(CrossDomainFilePropName)]
        public string CrossDomainFile
        {
            get
            {
                return (string)this[CrossDomainFilePropName];
            }
            set
            {
                this[CrossDomainFilePropName] = value;
            }
        }

        [ConfigurationProperty(EnabledPropName)]
        public bool Enabled
        {
            get
            {
                return (bool)this[EnabledPropName];
            }
            set
            {
                this[EnabledPropName] = value;
            }
        }
    }
}

