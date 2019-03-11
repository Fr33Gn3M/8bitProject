namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ServiceCommandElem : ClassTypeExElem
    {
        private const string InstanceClassPropName = "InstanceClass";

        [ConfigurationProperty(InstanceClassPropName)]
        public ClassTypeExElem InstanceClass
        {
            get
            {
                return (ClassTypeExElem)this[InstanceClassPropName];
            }
            set
            {
                this[InstanceClassPropName] = value;
            }
        }
    }
}

