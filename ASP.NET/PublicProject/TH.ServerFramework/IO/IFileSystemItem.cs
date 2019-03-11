namespace TH.ServerFramework.IO
{
    using System;
    using System.Collections;

    public interface IFileSystemItem
    {
        void CopyTo(IFileSystemItem item);
        void Delete();
        void MoveTo(IFileSystemItem item);
        void Rename(string name);

        DateTime CreatedDateUtc { get; }

        bool Exists { get; }

        string FullName { get; }

        string FullPath { get; }

        DateTime LastModifiedTimeUtc { get; }

        string Name { get; }

        IDictionary Parent { get; }
    }
}

