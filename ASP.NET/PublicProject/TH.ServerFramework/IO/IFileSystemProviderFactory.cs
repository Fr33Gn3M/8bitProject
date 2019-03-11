namespace TH.ServerFramework.IO
{
    using System;
    using System.Collections.Generic;

    public interface IFileSystemProviderFactory
    {
        IFileSystemProvider CreateInstance(string name, IDictionary<string, string> arguments);
        IFileSystemProvider GetInstance(string name);
        void RemoveInstance(string name);
    }
}

