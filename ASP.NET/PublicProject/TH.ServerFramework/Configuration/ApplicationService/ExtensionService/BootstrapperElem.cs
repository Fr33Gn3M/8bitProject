namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class BootstrapperElem : ConfigurationElement
    {
        private const string ShutDownTasksPropName = "ShutDownTasks";
        private const string StartupTasksPropName = "StartupTasks";

        [ConfigurationProperty(ShutDownTasksPropName, IsRequired = false)]
        public ClassTypeCollElem ShutDownTasks
        {
            get
            {
                return (ClassTypeCollElem)this[ShutDownTasksPropName];
            }
            set
            {
                this[ShutDownTasksPropName] = value;
            }
        }

        [ConfigurationProperty(StartupTasksPropName, IsRequired = true)]
        public ClassTypeCollElem StartupTasks
        {
            get
            {
                return (ClassTypeCollElem)this[StartupTasksPropName];
            }
            set
            {
                this[StartupTasksPropName] = value;
            }
        }
    }
}

