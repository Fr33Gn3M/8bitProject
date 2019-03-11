namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class TaskPoolElem : TaskPoolOptionsElem
    {
        private const string NamePropName = "name";

        [ConfigurationProperty(NamePropName, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this[NamePropName];
            }
            set
            {
                this[NamePropName] = value;
            }
        }
    }
}

