namespace TH.ServerFramework.Extensions
{
    using System;

    public interface IExtensionRepository
    {
        ExtensionConfiguration[] GetExtensionConfigurations();
        string[] GetLoadedExtensionNames();
        void LoadAll();
        void SetExtensionConfiguration(ExtensionConfiguration config);
        void UnloadAll();
    }
}

