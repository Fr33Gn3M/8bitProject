namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class FileSystemServiceElem : ConfigurationElement
    {
        private const string InstancesPropName = "Instances";
        private const string ProvidersPropName = "Providers";
        private const string TaskPoolPropName = "taskPool";

        [ConfigurationProperty(InstancesPropName, IsRequired = true)]
        public FileSystemInstanceCollElem Instances
        {
            get
            {
                return (FileSystemInstanceCollElem)this[InstancesPropName];
            }
            set
            {
                this[InstancesPropName] = value;
            }
        }

        [ConfigurationProperty(ProvidersPropName, IsRequired = true)]
        public ClassTypeCollElem Providers
        {
            get
            {
                return (ClassTypeCollElem)this[ProvidersPropName];
            }
            set
            {
                this[ProvidersPropName] = value;
            }
        }

        [ConfigurationProperty(TaskPoolPropName)]
        public string TaskPool
        {
            get
            {
                return (string)this[TaskPoolPropName];
            }
            set
            {
                this[TaskPoolPropName] = value;
            }
        }
    }
}

