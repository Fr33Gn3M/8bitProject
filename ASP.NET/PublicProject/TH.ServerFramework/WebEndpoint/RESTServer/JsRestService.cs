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
    using System.Reflection;
    using TH.ServerFramework.Utility;

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public sealed class JSRestService : IJSRestService
    {
        public Stream Handle(Stream postStream)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods:POST,GET");
            var match = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
            var folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location), "Javascripts");
            var path = folderPath;
            foreach (var item in match.RelativePathSegments)
                path = Path.Combine(path, item);
            if (System.IO.File.Exists(path))
            {
                var file = new System.IO.FileInfo(path);
                        WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
                        switch (file.Extension.ToLower())
                        {
                            case ".js":
                                WebOperationContext.Current.OutgoingResponse.ContentType = "application/x-javascript";
                                break;
                            case ".css":
                                WebOperationContext.Current.OutgoingResponse.ContentType = "text/css";
                                break;
                            case ".png":
                            case ".jpg":
                            case ".gif":
                                {
                                    var str = file.Extension.Substring(1);
                                    WebOperationContext.Current.OutgoingResponse.ContentType = "image/" + str;
                                    var result1 = System.IO.File.ReadAllBytes(path);
                                    var ms = new MemoryStream(result1);
                                    return ms;
                                }
                        }
                var result = System.IO.File.ReadAllBytes(path);
                var stream = new MemoryStream(result);
                return stream;
            }
            else
                return null;
        }
    }
}

