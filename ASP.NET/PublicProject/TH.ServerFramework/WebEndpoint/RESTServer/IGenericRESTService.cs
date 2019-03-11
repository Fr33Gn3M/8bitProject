namespace TH.ServerFramework.WebEndpoint.RESTServer
{
    using System;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface IGenericRESTService
    {
        [WebInvoke(Method = "*", UriTemplate = "{serviceName}/*")]
        Stream Handle(string serviceName, Stream postStream);
        [WebInvoke(Method = "*", UriTemplate = "info")]
        Stream Handle2(Stream postStream);
    }
}

