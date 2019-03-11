namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections;
    using System.Configuration;

    [ConfigurationCollection(typeof(FileSystemInstanceElem))]
    public class FileSystemInstanceCollElem : ConfigurationElementCollection
    {
        private const string DefaultInstanceNamePropName = "defaultInstanceName";

        public void Add(FileSystemInstanceElem elem)
        {
            this.BaseAdd(elem);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FileSystemInstanceElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            FileSystemInstanceElem elem = (FileSystemInstanceElem) element;
            return elem.InstanceName;
        }

        public FileSystemInstanceElem GetInstance(string name)
        {
            IEnumerator list= null;
            try
            {
                list = this.GetEnumerator();
                while (list.MoveNext())
                {
                    FileSystemInstanceElem elem = (FileSystemInstanceElem) list.Current;
                    if (elem.InstanceName == name)
                    {
                        return elem;
                    }
                }
            }
            finally
            {
                if (list is IDisposable)
                {
                    (list as IDisposable).Dispose();
                }
            }
            return null;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public void Remove(string name)
        {
            this.BaseRemove(name);
        }

        [ConfigurationProperty(DefaultInstanceNamePropName, IsRequired = true)]
        public string DefaultInstanceName
        {
            get
            {
                return (string)this[DefaultInstanceNamePropName];
            }
            set
            {
                this[DefaultInstanceNamePropName] = value;
            }
        }
    }
}

