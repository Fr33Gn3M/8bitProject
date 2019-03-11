namespace TH.ServerFramework.ServiceCatalog
{
    using System;
    using System.Runtime.InteropServices;

    public interface IService
    {
        TCommand CreateCommand<TCommand>() where TCommand: ICommand;
        ServiceConfigurationBase GetConfiguration(string[] properties);
        bool IsExists();

        string ServiceName { get; }
    }
}

