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
    public sealed class BusinessRestService : IBusinessRestService
    {
        public Stream Handle(Stream postStream)
        {
          //var   errorInfo = RegisterHelper.CheckIsRgistration();
          //if (!string.IsNullOrEmpty(errorInfo))
          //{
          //    throw new ArgumentException(errorInfo);
          //}
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods:POST,GET");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Encoding", "gzip");
            var match = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
          if (match.RelativePathSegments.Count < 2)
          {
              WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
              return null;
          }
          string serviceProfileName = match.RelativePathSegments[0];
          string businessInstanceName = match.RelativePathSegments[1];

          IServiceProfile serviceProfile = ServiceLocator.Current.GetInstance<IServiceProfileRepo>().GetServiceProfile(serviceProfileName);
            if (serviceProfile== null)
            {
                string str = serviceProfileName.ToLower();
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
          
            var ll= serviceHandler.Handle(WebOperationContext.Current, postStream);
            if (WebOperationContext.Current.OutgoingResponse.ContentType == "text/json")
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/json;charset=UTF-8";
            if (WebOperationContext.Current.OutgoingResponse.ContentType == "text/plain")
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain;charset=UTF-8";
            return ll;
        }

    }
}

