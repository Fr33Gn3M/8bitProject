namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class PredefinedServiceElem : ConfigurationElement
    {
        private const string CreatedDatePropName = "createdDate";
        private const string DescriptionPropName = "description";
        private const string ServiceNamePropName = "serviceName";
        private const string ServicePropertiesPropName = "ServiceProperties";
        private const string ThumbLocationPropName = "thumbLocation";
        private const string TitlePropName = "title";
        private const string UniqueIdPropName = "uniqueId";

        private const string ServiceTypeNamePropName = "serviceTypeName";
        private const string WebServiceTypeNamePropName = "webServiceTypeName";

        [ConfigurationProperty(WebServiceTypeNamePropName)]
        public string WebServiceType
        {
            get
            {
                return (string)this[WebServiceTypeNamePropName];
            }
            set
            {
                this[WebServiceTypeNamePropName] = value;
            }
        }

        [ConfigurationProperty(ServiceTypeNamePropName)]
        public string ServiceTypeName
        {
            get
            {
                return (string)this[ServiceTypeNamePropName];
            }
            set
            {
                this[ServiceTypeNamePropName] = value;
            }
        }

        protected Type GetClassType()
        {
            return Type.GetType(this.ServiceTypeName);
        }

        protected void SetClassType(Type t)
        {
            this.ServiceTypeName = t.AssemblyQualifiedName;
        }

        public Type  ServiceClassType
        {
            get
            {
                return this.GetClassType();
            }
            set
            {
                this.SetClassType(value);
            }
        }

        //private const string ServiceInstanceTypePropName = "servicInstanceType";
        //[ConfigurationProperty(ServiceInstanceTypePropName)]
        //public string ServiceInstanceType
        //{
        //    get
        //    {
        //        return (string)this[ServiceInstanceTypePropName];
        //    }
        //    set
        //    {
        //        this[ServiceInstanceTypePropName] = value;
        //    }
        //}

        private const string EnabledPropName = "enabled";
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

        [ConfigurationProperty(CreatedDatePropName)]
        public DateTime CreatedDate
        {
            get
            {
                return (DateTime)this[CreatedDatePropName];
            }
            set
            {
                this[CreatedDatePropName] = value;
            }
        }

        [ConfigurationProperty(DescriptionPropName)]
        public string Description
        {
            get
            {
                return (string)this[DescriptionPropName];
            }
            set
            {
                this[DescriptionPropName] = value;
            }
        }

        [ConfigurationProperty(ServiceNamePropName, IsKey = true)]
        public string ServiceName
        {
            get
            {
                return (string)this[ServiceNamePropName];
            }
            set
            {
                this[ServiceNamePropName] = value;
            }
        }

        [ConfigurationProperty(ServicePropertiesPropName)]
        public WritableKeyValueConfigurationCollection ServiceProperties
        {
            get
            {
                return (WritableKeyValueConfigurationCollection)this[ServicePropertiesPropName];
            }
            set
            {
                this[ServicePropertiesPropName] = value;
            }
        }

        [ConfigurationProperty(ThumbLocationPropName)]
        public string ThumbLocation
        {
            get
            {
                return (string)this[ThumbLocationPropName];
            }
            set
            {
                this[ThumbLocationPropName] = value;
            }
        }

        [ConfigurationProperty(TitlePropName)]
        public string Title
        {
            get
            {
                return (string)this[TitlePropName];
            }
            set
            {
                this[TitlePropName] = value;
            }
        }

        [ConfigurationProperty(UniqueIdPropName, IsRequired = true)]
        public string UniqueId
        {
            get
            {
                return (string)this[UniqueIdPropName];
            }
            set
            {
                this[UniqueIdPropName] = value;
            }
        }
    }
}

