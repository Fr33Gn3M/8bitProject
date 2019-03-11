namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler
{
    using System;
    using System.IO;
    using System.ServiceModel.Web;

    public interface IWebHandler
    {
        bool CanHandle(WebOperationContext context, Stream postData);
        Stream Handle(WebOperationContext context, Stream postData);
        string ServiceName { get; }
        void SetMatchedUrlParamters(WebOperationContext context);
        void InitWebHandler(string serviceName, string urlPathPattern);
    }
}

