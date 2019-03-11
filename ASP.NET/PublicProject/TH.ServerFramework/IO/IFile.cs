namespace TH.ServerFramework.IO
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public interface IFile : IFileSystemItem<IFile>
    {
        void Create(long size);
        Stream Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare, long bufferSize = 0x100000L);

        string Extension { get; }

        string NameWithoutExtension { get; }

        long Size { get; }

        string FullName { get;  }

        DateTime UpdateDate { get; }
    }
}

