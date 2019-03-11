namespace TH.ServerFramework.Endpoint
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable, DataContract]
    public class EndpointAddressDescription
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _Address;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _Name;

        [DataMember]
        public string Address
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Address;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Address = value;
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
    }
}

