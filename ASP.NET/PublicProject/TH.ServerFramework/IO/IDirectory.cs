namespace TH.ServerFramework.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public interface IDirectory : IFileSystemItem<IDirectory>
    {
        IEnumerable<IDirectory> GetDirectories();
        IEnumerable<IDirectory> GetDirectories(string filter, SearchOption scope);
        IDirectory GetDirectory(string directoryName);
        IFile GetFile(string fileName);
        IEnumerable<IFile> GetFiles();
        IEnumerable<IFile> GetFiles(string filter, SearchOption scope);
        string FullName { get; }

    }
}

