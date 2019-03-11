using Bootstrap.Extensions.StartupTasks;
using TH.ServerFramework.WebEndpoint.RESTServer;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ServerFramework.Configuration;

namespace TH.ServerFramework.Bootstrapper
{
    /// <summary>
    /// arcgis rest 服务 适配器的注册
    /// </summary>
    public class RegisterServiceProfile : IStartupTask
    {
        public void Reset()
        {

        }

        public void Run()
        {
            var serviceProfile = ServiceLocator.Current.GetInstance<IServiceProfileRepo>();
            var section = SettingsSection.GetSection();
            if (section.ApplicationServices.ServiceProfiles != null)
            {
                foreach (ServiceProfileElem item in section.ApplicationServices.ServiceProfiles)
                {
                    var profile = Activator.CreateInstance(item.ClassType) as IServiceProfile;
                    serviceProfile.AddServiceProfile(item.ServiceProfileName, profile);
                    foreach (WebHandlerElem webHandle in item.WebHandlers)
                        profile.RegisterWebHandler(webHandle.ClassType, item.ServiceProfileName, webHandle.UrlPathPattern);
                }
            }
        }
    }
}
