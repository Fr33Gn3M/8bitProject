namespace TH.ServerFramework.IO
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable, DataContract, KnownType(typeof(FolderItem)), KnownType(typeof(FileItem))]
    public abstract class FileSystemItemBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime _CreatedDate;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _FullPath;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime _ModifiedDate;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _Name;

        protected FileSystemItemBase()
        {
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
        public string FullPath
        {
            [DebuggerNonUserCode]
            get
            {
                return this._FullPath;
            }
            [DebuggerNonUserCode]
            set
            {
                this._FullPath = value;
            }
        }

        [DataMember]
        public DateTime ModifiedDate
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ModifiedDate;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ModifiedDate = value;
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

