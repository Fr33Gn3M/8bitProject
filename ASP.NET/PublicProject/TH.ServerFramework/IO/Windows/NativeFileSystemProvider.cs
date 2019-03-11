namespace TH.ServerFramework.IO.Windows
{
    using TH.ServerFramework.IO;
    using System;
    using System.IO;

    internal class NativeFileSystemProvider : IFileSystemProvider
    {
        private readonly ArgumentsBuilder _argumentsDescription;
        private readonly IPathHandler _pathHandler = new NativePathHandler();
        private bool disposedValue;

        public NativeFileSystemProvider(ArgumentsBuilder argumentsDescription)
        {
            this._argumentsDescription = argumentsDescription;
        }

        public ITemporaryDirectory CreateTemporaryDirectory()
        {
            return new NativeTemporaryDirectory();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue && disposing)
            {
            }
            this.disposedValue = true;
        }

        public IDirectory GetDirectory(string directoryPath)
        {
            string fullPath = null;
            if (string.IsNullOrEmpty(directoryPath))
            {
                fullPath = this._argumentsDescription.RootDirectory;
            }
            else
            {
                directoryPath = directoryPath.Trim(new char[] { '\\' });
                fullPath = this._pathHandler.Combine(new string[] { this._argumentsDescription.RootDirectory, directoryPath });
            }
            DirectoryInfo di = new DirectoryInfo(fullPath);
            DirectoryInfo root = new DirectoryInfo(this._argumentsDescription.RootDirectory);
            return new NativeDirectory(di, root);
        }

        public ArgumentsBuilder Arugments
        {
            get
            {
                return this._argumentsDescription;
            }
        }

        public IPathHandler PathHandler
        {
            get
            {
                return this._pathHandler;
            }
        }

       
    }
}

