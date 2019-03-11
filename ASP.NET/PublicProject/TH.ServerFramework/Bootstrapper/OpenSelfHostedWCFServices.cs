// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Bootstrap.Extensions.StartupTasks;
using Microsoft.Practices.ServiceLocation;
using System.ServiceModel;
using System.ServiceModel.Web;
using TH.ServerFramework.WebEndpoint.Activation;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.WebEndpoint.RESTServer;


namespace TH.ServerFramework
{
	namespace Bootstrapper
	{
		internal class OpenSelfHostedWCFServices : IStartupTask
		{
			
            //private static readonly Type[] _serviceType = new Type[] { //GetType(ConfigurationService),
            //typeof(EndpointAdministratorService),
            //typeof(BlobStorageService),
            //typeof(ExtensionService),
            //typeof(FileSystemService),
            //typeof(ServiceRepoService),
            //typeof(TaskPoolService)};

            //private static readonly Type[] _serviceRestType = new Type[] { //GetType(ConfigurationService),
            //typeof(GenericRESTService),
            //typeof(WebFileSystemService),
            //typeof(RIACrossDomainService)};
			
			public void Reset()
			{
                var selfHostedService = ServiceLocator.Current.GetInstance<ISelfHostedActivatorService>();
                var section = SettingsSection.GetSection();
                foreach (PredefinedServiceElem item in section.ApplicationServices.ServiceCatalog.PredefinedServiceSource)
                {
                    if (item.Enabled == true)
                    {
                        if (item.WebServiceType.ToLower() == "wcfservice")
                            selfHostedService.Close(item.ServiceClassType);
                        if (item.WebServiceType.ToLower() == "restservice")
                            selfHostedService.Close(item.ServiceClassType);
                        if (item.WebServiceType.ToLower() == "webservice")
                            selfHostedService.Close(item.ServiceClassType);
                    }
                }
			}
			
			public void Run()
			{
                var selfHostedService = ServiceLocator.Current.GetInstance<ISelfHostedActivatorService>();
                var section = SettingsSection.GetSection();
                foreach (PredefinedServiceElem item in section.ApplicationServices.ServiceCatalog.PredefinedServiceSource)
                {
                    if (item.Enabled == true)
                    {
                        if (item.WebServiceType.ToLower() == "wcfservice")
                            selfHostedService.Open(item.ServiceClassType);
                        if (item.WebServiceType.ToLower() == "restservice")
                            selfHostedService.OpenAsREST(item.ServiceClassType);
                        if (item.WebServiceType.ToLower() == "webservice")
                            selfHostedService.OpenAsREST(item.ServiceClassType);
                    }
                }
			}
		}
	}
}
