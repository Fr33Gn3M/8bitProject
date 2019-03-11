namespace TH.ServerFramework.IO
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable, DataContract]
    public class FileItem : FileSystemItemBase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private long _Size;

        [DataMember]
        public long Size
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Size;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Size = value;
            }
        }
    }
}

