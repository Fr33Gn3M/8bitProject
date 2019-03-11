namespace TH.ServerFramework.WebEndpoint.RESTServer
{
    using System;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface IJSRestService
    {
        [WebInvoke(Method = "*", UriTemplate = "*")]
        Stream Handle(Stream postStream);
    }
}

