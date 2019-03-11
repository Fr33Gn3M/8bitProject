namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class ServiceElem : ClassTypeExElem
    {
        private const string CommandsPropName = "Commands";
        private const string ServiceSourcePropName = "serviceSource";
        private const string TriggerPropName = "trigger";

        [ConfigurationProperty(CommandsPropName, IsRequired = false)]
        public ServiceCommandCollElem Commands
        {
            get
            {
                return (ServiceCommandCollElem)this[CommandsPropName];
            }
            set
            {
                this[CommandsPropName] = value;
            }
        }

        [ConfigurationProperty(ServiceSourcePropName)]
        public string ServiceSource
        {
            get
            {
                return (string)this[ServiceSourcePropName];
            }
            set
            {
                this[ServiceSourcePropName] = value;
            }
        }

        [ConfigurationProperty(TriggerPropName)]
        public string Trigger
        {
            get
            {
                return (string)this[TriggerPropName];
            }
            set
            {
                this[TriggerPropName] = value;
            }
        }
    }
}

