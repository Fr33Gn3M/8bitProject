namespace TH.ServerFramework.WebEndpoint.RESTServer
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.VisualBasic;
    using TH.ServerFramework.WebEndpoint.RESTServer.WebHandler;
    using System;
    using System.IO;
    using System.ServiceModel.Web;
    using System.Diagnostics;
    using System.ServiceModel;

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public sealed class GenericRESTService : IGenericRESTService
    {
        public Stream Handle(string serviceName, Stream postStream)
        {
            var errorInfo = RegisterHelper.CheckIsRgistration();
            if (!string.IsNullOrEmpty(errorInfo))
            {
                throw new ArgumentException(errorInfo);
            }
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods:POST,GET");
            IServiceProfile serviceProfile = ServiceLocator.Current.GetInstance<IServiceProfileRepo>().GetServiceProfile(serviceName);
            if (serviceProfile == null)
            {
                string str = serviceName.ToLower();
                serviceProfile = ServiceLocator.Current.GetInstance<IServiceProfileRepo>().GetServiceProfile(str);
                if (serviceProfile == null)
                {
                    WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
                    return null;
                }
            }
            IWebHandler serviceHandler = serviceProfile.CreateWebHandler(WebOperationContext.Current, postStream);
            if (serviceHandler == null)
            {
                WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
                return null;
            }
            Stopwatch s1 = new Stopwatch();
            s1.Start();
            var ll = serviceHandler.Handle(WebOperationContext.Current, postStream);
            if (WebOperationContext.Current.OutgoingResponse.ContentType == "text/json")
                if (WebOperationContext.Current.OutgoingResponse.ContentType == "text/json")
                {
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Encoding", "gzip");
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/json;charset=UTF-8";
                }
            if (WebOperationContext.Current.OutgoingResponse.ContentType == "text/plain")
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Encoding", "gzip");
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain;charset=UTF-8";
            }
            s1.Stop();//80毫秒
            return ll;
        }


        public Stream Handle2(Stream postStream)
        {
           // {"currentVersion":10,"soapUrl":"http://WIN-S7VETONERDB/ArcGIS/services","secureSoapUrl":"https://WIN-S7VETONERDB:443/ArcGIS/services","authInfo":{"isTokenBasedSecurity":false}}
            return null;
        }
    }
}

