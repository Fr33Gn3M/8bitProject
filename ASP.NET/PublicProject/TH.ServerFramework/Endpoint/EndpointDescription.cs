namespace TH.ServerFramework.Endpoint
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable, DataContract]
    public class EndpointDescription
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _Description;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Enabled;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _Name;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _Title;

        [DataMember]
        public string Description
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Description;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Description = value;
            }
        }

        [DataMember]
        public bool Enabled
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Enabled;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Enabled = value;
            }
        }

        [DataMember]
        public string Name
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Name;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Name = value;
            }
        }

        [DataMember]
        public string Title
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Title;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Title = value;
            }
        }
    }
}

