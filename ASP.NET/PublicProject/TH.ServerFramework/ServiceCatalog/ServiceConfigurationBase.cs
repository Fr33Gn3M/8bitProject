namespace TH.ServerFramework.ServiceCatalog
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public abstract class ServiceConfigurationBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime _CreatedDate;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private DateTime _LastUpdatedDate;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _ServiceDescription;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _ServiceName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private IDictionary<string, string> _ServiceProperties;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _ServiceTitle;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] _ThumbImage;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private Guid _UniqueId;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _CopyrightText;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string[] _Tags;
        [DataMember]
        public  string[] Tags
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Tags;
            }
            set
            {
                this._Tags = value;
            }
        }
        protected ServiceConfigurationBase()
        {
        }

        [DataMember]
        public string CopyrightText
        {
            [DebuggerNonUserCode]
            get
            {
                return this._CopyrightText;
            }
            [DebuggerNonUserCode]
            set
            {
                this._CopyrightText = value;
            }
        }


        [DataMember]
        public DateTime CreatedDate
        {
            [DebuggerNonUserCode]
            get
            {
                return this._CreatedDate;
            }
            [DebuggerNonUserCode]
            set
            {
                this._CreatedDate = value;
            }
        }

        [DataMember]
        public DateTime LastUpdatedDate
        {
            [DebuggerNonUserCode]
            get
            {
                return this._LastUpdatedDate;
            }
            [DebuggerNonUserCode]
            set
            {
                this._LastUpdatedDate = value;
            }
        }

        [DataMember]
        public string ServiceDescription
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ServiceDescription;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ServiceDescription = value;
            }
        }

        [DataMember]
        public string ServiceName
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ServiceName;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ServiceName = value;
            }
        }

        [DataMember]
        public IDictionary<string, string> ServiceProperties
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ServiceProperties;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ServiceProperties = value;
            }
        }

        [DataMember]
        public string ServiceTitle
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ServiceTitle;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ServiceTitle = value;
            }
        }

        [DataMember]
        public byte[] ThumbImage
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ThumbImage;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ThumbImage = value;
            }
        }

        [DataMember]
        public Guid UniqueId
        {
            [DebuggerNonUserCode]
            get
            {
                return this._UniqueId;
            }
            [DebuggerNonUserCode]
            set
            {
                this._UniqueId = value;
            }
        }
    }
}

