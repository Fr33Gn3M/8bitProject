namespace TH.ServerFramework.Endpoint
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable, DataContract]
    public class EndpointConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private bool _Enabled;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _EndpointName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private IDictionary<string, string> _Properties;

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
        public string EndpointName
        {
            [DebuggerNonUserCode]
            get
            {
                return this._EndpointName;
            }
            [DebuggerNonUserCode]
            set
            {
                this._EndpointName = value;
            }
        }

        [DataMember]
        public IDictionary<string, string> Properties
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Properties;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Properties = value;
            }
        }
    }
}

