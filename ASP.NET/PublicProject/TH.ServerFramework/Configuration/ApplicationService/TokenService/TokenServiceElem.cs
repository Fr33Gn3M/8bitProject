// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Configuration;


namespace TH.ServerFramework
{

    public class TokenServiceElem : ConfigurationElement
    {
        private const string TokenCacheNamePropName = "tokenCacheName";
        [ConfigurationProperty(TokenCacheNamePropName)]
        public string TokenCacheName
        {
            get
            {
                return (string)(this[TokenCacheNamePropName]);
            }
            set
            {
                this[TokenCacheNamePropName] = value;
            }
        }

        private const string AdminTokenTtlPropName = "adminTokenTtl";
        [ConfigurationProperty(AdminTokenTtlPropName)]
        public TimeSpan AdminTokenTtl
        {
            get
            {
                return (TimeSpan)this[AdminTokenTtlPropName];
            }
            set
            {
                this[AdminTokenTtlPropName] = value;
            }
        }

        private const string SystemPasswordPropName = "systemPassword";
        [ConfigurationProperty(SystemPasswordPropName)]
        public string SystemPassword
        {
            get
            {
                return (string)(this[SystemPasswordPropName]);
            }
            set
            {
                this[SystemPasswordPropName] = value;
            }
        }

        private const string SystemUserNamePropName = "systemUserName";
        [ConfigurationProperty(SystemUserNamePropName)]
        public string SystemUserName
        {
            get
            {
                return (string)(this[SystemUserNamePropName]);
            }
            set
            {
                this[SystemUserNamePropName] = value;
            }
        }

        private const string DefaultPasswordPropName = "defaultPassword";
        [ConfigurationProperty(DefaultPasswordPropName)]
        public string DefaultPassword
        {
            get
            {
                return (string)(this[DefaultPasswordPropName]);
            }
            set
            {
                this[DefaultPasswordPropName] = value;
            }
        }

        private const string ServiceIpPropName = "ServiceIp";
        [ConfigurationProperty(ServiceIpPropName)]
        public string ServiceIp
        {
            get
            {
                return (string)(this[ServiceIpPropName]);
            }
            set
            {
                this[ServiceIpPropName] = value;
            }
        }

        private const string ServicePortPropName = "ServicePort";
        [ConfigurationProperty(ServicePortPropName)]
        public int ServicePort
        {
            get
            {
                return (int)(this[ServicePortPropName]);
            }
            set
            {
                this[ServicePortPropName] = value;
            }
        }

        private const string ClientPortPropName = "ClientPort";
        [ConfigurationProperty(ClientPortPropName)]
        public int ClientPort
        {
            get
            {
                return (int)(this[ClientPortPropName]);
            }
            set
            {
                this[ClientPortPropName] = value;
            }
        }

        private const string ClientServicePortPropName = "ClientServicePort";
        [ConfigurationProperty(ClientServicePortPropName)]
        public int ClientServicePort
        {
            get
            {
                return (int)(this[ClientServicePortPropName]);
            }
            set
            {
                this[ClientServicePortPropName] = value;
            }
        }



        private const string IsFileSystemServicePropName = "IsFileSystemService";
        [ConfigurationProperty(IsFileSystemServicePropName)]
        public bool IsFileSystemService
        {
            get
            {
                return (bool)(this[IsFileSystemServicePropName]);
            }
            set
            {
                this[IsFileSystemServicePropName] = value;
            }
        }

        private const string IsServiceVaildPropName = "IsServiceVaild";
        [ConfigurationProperty(IsServiceVaildPropName)]
        public bool IsServiceVaild
        {
            get
            {
                return (bool)(this[IsServiceVaildPropName]);
            }
            set
            {
                this[IsServiceVaildPropName] = value;
            }
        }

        private const string IsLogVaildPropName = "IsLog";
        [ConfigurationProperty(IsLogVaildPropName)]
        public bool IsLog
        {
            get
            {
                return (bool)(this[IsLogVaildPropName]);
            }
            set
            {
                this[IsLogVaildPropName] = value;
            }
        }

        private const string RegistrationPathPropName = "RegistrationPath";
        [ConfigurationProperty(RegistrationPathPropName)]
        public string RegistrationPath
        {
            get
            {
                return (string)this[RegistrationPathPropName];
            }
            set
            {
                this[RegistrationPathPropName] = value;
            }
        }

        private const string RegistrationSystemNamePropName = "RegistrationSystemName";
        [ConfigurationProperty(RegistrationSystemNamePropName)]
        public string RegistrationSystemName
        {
            get
            {
                return (string)this[RegistrationSystemNamePropName];
            }
            set
            {
                this[RegistrationSystemNamePropName] = value;
            }
        }

        private const string TokenServiceNamePropName = "TokenServiceName";
        [ConfigurationProperty(TokenServiceNamePropName)]
        public string TokenServiceName
        {
            get
            {
                return (string)(this[TokenServiceNamePropName]);
            }
            set
            {
                this[TokenServiceNamePropName] = value;
            }
        }

        // ServiceIp="192.168.0.100" ServicePort="8088" 
        public override bool IsReadOnly()
        {
            return false;
        }
    }
}