namespace TH.ServerFramework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable, DataContract]
    public class ExtensionConfiguration
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Enabled;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _ExtensionName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private IDictionary<string, string> _LoadArguments;

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
        public string ExtensionName
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ExtensionName;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ExtensionName = value;
            }
        }

        [DataMember]
        public IDictionary<string, string> LoadArguments
        {
            [DebuggerNonUserCode]
            get
            {
                return this._LoadArguments;
            }
            [DebuggerNonUserCode]
            set
            {
                this._LoadArguments = value;
            }
        }
    }
}

