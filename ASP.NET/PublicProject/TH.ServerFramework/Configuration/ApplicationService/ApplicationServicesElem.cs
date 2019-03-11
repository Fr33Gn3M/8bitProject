namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ApplicationServicesElem : ConfigurationElement
    {
        private const string BlobServicePropName = "BlobService";
        private const string CacheServicePropName = "CacheService";
        private const string ExtensionServicePropName = "ExtensionService";
        private const string FileSystemServicePropName = "FileSystemService";
        //private const string LoggingServicePropName = "LoggingService";
        //private const string ServiceBusPropName = "ServiceBus";
        private const string ServiceCatalogPropName = "ServiceCatalog";
        private const string ServiceLocatorProviderPropName = "ServiceLocatorProvider";
        private const string TaskPoolServicePropName = "TaskPoolService";
        private const string TokenServicePropName = "TokenService";
        private const string WebServicePropName = "WebService";

        [ConfigurationProperty(BlobServicePropName)]
        public BlobServiceElem BlobService
        {
            get
            {
                return (BlobServiceElem) this[BlobServicePropName];
            }
            set
            {
                this[BlobServicePropName] = value;
            }
        }

        [ConfigurationProperty(CacheServicePropName)]
        public CacheServiceElem CacheService
        {
            get
            {
                return (CacheServiceElem) this[CacheServicePropName];
            }
            set
            {
                this[CacheServicePropName] = value;
            }
        }

        [ConfigurationProperty(ExtensionServicePropName)]
        public ExtensionServiceElem ExtensionService
        {
            get
            {
                return (ExtensionServiceElem) this[ExtensionServicePropName];
            }
            set
            {
                this[ExtensionServicePropName] = value;
            }
        }

        [ConfigurationProperty(FileSystemServicePropName)]
        public FileSystemServiceElem FileSystemService
        {
            get
            {
                return (FileSystemServiceElem) this[FileSystemServicePropName];
            }
            set
            {
                this[FileSystemServicePropName] = value;
            }
        }

        //[ConfigurationProperty("LoggingService")]
        //public LoggingServiceElem LoggingService
        //{
        //    get
        //    {
        //        return (LoggingServiceElem) this["LoggingService"];
        //    }
        //    set
        //    {
        //        this["LoggingService"] = value;
        //    }
        //}

        //[ConfigurationProperty("ServiceBus")]
        //public ServiceBusElem ServiceBus
        //{
        //    get
        //    {
        //        return (ServiceBusElem) this["ServiceBus"];
        //    }
        //    set
        //    {
        //        this["ServiceBus"] = value;
        //    }
        //}

        [ConfigurationProperty(ServiceCatalogPropName)]
        public ServiceCatalogElem ServiceCatalog
        {
            get
            {
                return (ServiceCatalogElem)this[ServiceCatalogPropName];
            }
            set
            {
                this[ServiceCatalogPropName] = value;
            }
        }

        [ConfigurationProperty(ServiceLocatorProviderPropName)]
        public ClassTypeElem ServiceLocatorProvider
        {
            get
            {
                return (ClassTypeElem) this[ServiceLocatorProviderPropName];
            }
            set
            {
                this[ServiceLocatorProviderPropName] = value;
            }
        }

        [ConfigurationProperty(TaskPoolServicePropName)]
        public TaskServiceElem TaskPoolService
        {
            get
            {
                return (TaskServiceElem) this[TaskPoolServicePropName];
            }
            set
            {
                this[TaskPoolServicePropName] = value;
            }
        }

        [ConfigurationProperty("TokenService")]
        public TokenServiceElem TokenService
        {
            get
            {
                return (TokenServiceElem)this["TokenService"];
            }
            set
            {
                this["TokenService"] = value;
            }
        }

        [ConfigurationProperty(WebServicePropName)]
        public WebServiceElem WebService
        {
            get
            {
                return (WebServiceElem) this[WebServicePropName];
            }
            set
            {
                this[WebServicePropName] = value;
            }
        }

        private const string ServiceProfilesPropName = "ServiceProfiles";
        [ConfigurationProperty(ServiceProfilesPropName)]
        public ServiceProfileCollElem ServiceProfiles
        {
            get
            {
                return (ServiceProfileCollElem)this[ServiceProfilesPropName];
            }
            set
            {
                this[ServiceProfilesPropName] = value;
            }
        }
    }
}

