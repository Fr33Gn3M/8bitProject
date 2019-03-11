namespace TH.ServerFramework.Bootstrapper
{
    using TH.ServerFramework.Configuration;
    using TH.ServerFramework.Extensions;
    using TH.ServerFramework.Logs;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ApplicationBuilder
    {
        private ApplicationBuilder()
        {
            throw new NotSupportedException();
        }

        public static void Build()
        {
            var serviceLocatorProviderType = SettingsSection.GetSection().ApplicationServices.ServiceLocatorProvider.ClassType;
            IServiceLocator serviceLocatorProvider = Activator.CreateInstance(serviceLocatorProviderType) as IServiceLocator;
            if ((serviceLocatorProvider == null))
            {
                throw new ArgumentException("Exception Occured");
            }
            ServiceLocator.SetLocatorProvider(() => serviceLocatorProvider);
            dynamic providerType = SettingsSection.GetSection().ApplicationServices.ExtensionService.RepoProvider.ClassType;
            IExtensionRepository extensionRepo = Activator.CreateInstance(providerType);
            extensionRepo.LoadAll();
            var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
            log.Log(LogLevel.Info, "系统初始化完成!");
        }

        public static void Reset()
        {
            dynamic extensionRepo = ServiceLocator.Current.GetInstance<IExtensionRepository>();
            extensionRepo.UnloadAll();
        }
    }

}

