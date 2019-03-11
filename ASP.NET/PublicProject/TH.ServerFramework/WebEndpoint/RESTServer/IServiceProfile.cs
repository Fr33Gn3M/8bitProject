namespace TH.ServerFramework.WebEndpoint.RESTServer
{
    using TH.ServerFramework.WebEndpoint.RESTServer.WebHandler;
    using System;
    using System.IO;
    using System.ServiceModel.Web;

    public interface IServiceProfile
    {
        IWebHandler CreateWebHandler(WebOperationContext context, Stream postData);
        void RegisterWebHandler(Type type,params object[] objs);
        string ServiceName { get;  }
        void SetServiceName(string serviceName);
    }
}

