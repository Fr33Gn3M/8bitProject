using TH.ServerFramework.WebEndpoint.RESTServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.WebEndpoint.Activation
{
    public class ServiceProfileRepo : IServiceProfileRepo
    {
        private static Dictionary<string, IServiceProfile> dicServiceProfiles = new Dictionary<string, IServiceProfile>();
        public void AddServiceProfile(string serviceName, IServiceProfile ServiceProfiles)
        {
            ServiceProfiles.SetServiceName(serviceName);
            if (dicServiceProfiles.ContainsKey(serviceName))
                return;
            dicServiceProfiles.Add(serviceName, ServiceProfiles);
        }

        public IServiceProfile GetServiceProfile(string serviceName)
        {
            if (dicServiceProfiles.ContainsKey(serviceName))
              return  dicServiceProfiles[serviceName];
            return null;
        }
    }
}
