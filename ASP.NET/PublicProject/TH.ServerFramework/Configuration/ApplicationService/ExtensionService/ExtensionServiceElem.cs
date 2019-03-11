namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(ExtensionElem))]
    public class ExtensionServiceElem : ConfigurationElementCollection
    {
        private const string BootstrapperPropName = "Bootstrapper";
        private const string RepoProviderPropName = "RepoProvider";

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExtensionElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ExtensionElem elem = (ExtensionElem) element;
            return elem.ClassTypeName;
        }

        public ExtensionElem GetExtension(string name)
        {
            return (ExtensionElem) this.BaseGet(name);
        }

        [ConfigurationProperty(BootstrapperPropName, IsRequired = false)]
        public BootstrapperElem Bootstrapper
        {
            get
            {
                return (BootstrapperElem) this[BootstrapperPropName];
            }
            set
            {
                this[BootstrapperPropName] = value;
            }
        }

        [ConfigurationProperty(RepoProviderPropName)]
        public ClassTypeElem RepoProvider
        {
            get
            {
                return (ClassTypeElem)this[RepoProviderPropName];
            }
            set
            {
                this[RepoProviderPropName] = value;
            }
        }
    }
}

