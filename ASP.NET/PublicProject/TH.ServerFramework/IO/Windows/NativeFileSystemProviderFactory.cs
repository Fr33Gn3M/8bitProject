namespace TH.ServerFramework.IO.Windows
{
    using TH.ServerFramework.IO;
    using TH.ServerFramework.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class NativeFileSystemProviderFactory : IFileSystemProviderFactory
    {
        private readonly IDictionary<string, ArgumentsBuilder> _instances = new Dictionary<string, ArgumentsBuilder>();

        public IFileSystemProvider CreateInstance(string name, IDictionary<string, string> arguments)
        {
            ArgumentsBuilder argsBuilder = ArgumentsBuilder.Parse(arguments);
            if (string.IsNullOrEmpty(argsBuilder.RootDirectory))
            {//todo
              argsBuilder.RootDirectory = IOExtensions.GetCurrentApplicationFolderPath();
            }
            if (!Directory.Exists(argsBuilder.RootDirectory))
            {
                Directory.CreateDirectory(argsBuilder.RootDirectory);
            }
            this._instances.Add(name, argsBuilder);
            return this.GetInstance(name);
        }

        public IFileSystemProvider GetInstance(string name)
        {
            if (!this._instances.ContainsKey(name))
            {
                return null;
            }
            return new NativeFileSystemProvider(this._instances[name]);
        }

        public void RemoveInstance(string name)
        {
            if (this._instances.ContainsKey(name))
            {
                this._instances.Remove(name);
            }
        }
    }
}

