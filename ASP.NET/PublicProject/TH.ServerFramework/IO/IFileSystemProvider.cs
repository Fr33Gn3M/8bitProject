namespace TH.ServerFramework.IO
{
    using System;

    public interface IFileSystemProvider : IDisposable
    {
        ITemporaryDirectory CreateTemporaryDirectory();
        IDirectory GetDirectory(string directoryPath);

        IPathHandler PathHandler { get; }
    }
}

