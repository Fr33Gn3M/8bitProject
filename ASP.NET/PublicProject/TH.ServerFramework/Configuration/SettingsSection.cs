namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;
    using System.Runtime.CompilerServices;

    public class SettingsSection : ConfigurationSection
    {
        private const string ApplicationServicePropName = "ApplicationServices";
        private const string MaxAllowedGetUrlLengthPropName = "maxAllowedGetUrlLength";
        private const string RIACrossDomainServicePropName = "RIACrossDomainService";
        public const string SectionName = "ServerFramework";

        public static SettingsSection GetSection()
        {
            return ConfigurationManager.GetSection(SectionName) as SettingsSection;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty(ApplicationServicePropName)]
        public ApplicationServicesElem ApplicationServices
        {
            get
            {
                return (ApplicationServicesElem)this[ApplicationServicePropName];
            }
            set
            {
                this[ApplicationServicePropName] = value;
            }
        }

        [ConfigurationProperty(MaxAllowedGetUrlLengthPropName)]
        public int MaxAllowedGetUrlLength
        {
            get
            {
                return (int)this[MaxAllowedGetUrlLengthPropName];
            }
            set
            {
                this[MaxAllowedGetUrlLengthPropName] = value;
            }
        }

        [ConfigurationProperty(RIACrossDomainServicePropName, IsRequired = false)]
        public RIACrossDomainServiceElem RIACrossDomainService
        {
            get
            {
                return (RIACrossDomainServiceElem)this[RIACrossDomainServicePropName];
            }
            set
            {
                this[RIACrossDomainServicePropName] = value;
            }
        }
    }
}

