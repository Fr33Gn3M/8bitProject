namespace TH.ServerFramework.Extensions
{
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Collections.Generic;

    public interface IExtension
    {
        void OnLoad(IDictionary<string, string> arguments, IServiceLocator serviceLocator);
        void OnUnload();
    }
}

