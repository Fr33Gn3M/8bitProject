namespace TH.ServerFramework.IO
{
    using System;
    using System.Collections.Generic;

    public interface IFileSystemProviderRepo
    {
        IFileSystemProvider GetProviderInstance(string name);
        string[] GetProviderInstanceNames(string providerName);
        string[] GetProviderNames();
        void RegisterProviderFactory(IFileSystemProviderFactory factory);
        void RegisterProviderInstance(string name, string providerName, IDictionary<string, string> args);
        void UnregisterProviderInstance(string name);
        string GetDefaultInstanceName();
        void SetDefaultInstanceName(string name);
    }
}

