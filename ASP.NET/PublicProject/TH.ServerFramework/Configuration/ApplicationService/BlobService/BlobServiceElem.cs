namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class BlobServiceElem : ConfigurationElement
    {
        private const string DefaultSectionNamePropName = "defaultSectionName";
        private const string MaxAllowedBlobPropertyKeyLengthPropName = "maxAllowedBlobPropertyKeyLength";
        private const string MaxAllowedBlobPropertyValueLengthPropName = "maxAllowedBlobPropertyValueLength";
        private const string MaxAllowedBlobSizePropName = "maxAllowedBlobSize";

        [ConfigurationProperty(DefaultSectionNamePropName)]
        public string DefaultSectionName
        {
            get
            {
                return (string)this[DefaultSectionNamePropName];
            }
            set
            {
                this[DefaultSectionNamePropName] = value;
            }
        }

        [ConfigurationProperty(MaxAllowedBlobPropertyKeyLengthPropName)]
        public int MaxAllowedBlobPropertyKeyLength
        {
            get
            {
                return (int)this[MaxAllowedBlobPropertyKeyLengthPropName];
            }
            set
            {
                this[MaxAllowedBlobPropertyKeyLengthPropName] = value;
            }
        }

        [ConfigurationProperty(MaxAllowedBlobPropertyValueLengthPropName)]
        public int MaxAllowedBlobPropertyValueLength
        {
            get
            {
                return (int)this[MaxAllowedBlobPropertyValueLengthPropName];
            }
            set
            {
                this[MaxAllowedBlobPropertyValueLengthPropName] = value;
            }
        }

        [ConfigurationProperty(MaxAllowedBlobSizePropName)]
        public string MaxAllowedBlobSize
        {
            get
            {
                return (string)this[MaxAllowedBlobSizePropName];
            }
            set
            {
                this[MaxAllowedBlobSizePropName] = value;
            }
        }
    }
}

