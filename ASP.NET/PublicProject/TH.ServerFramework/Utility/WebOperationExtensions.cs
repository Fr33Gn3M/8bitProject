namespace TH.ServerFramework.Utility
{
    using Microsoft.VisualBasic;
    using RestSharp.Contrib;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.ServiceModel.Web;
    using System.Text;
    using System.Web;

    public static class WebOperationExtensions
    {
        public static string GetRootRequestUrl(this WebOperationContext source)
        {
            var iq = source.IncomingRequest;
            var url = iq.UriTemplateMatch.BaseUri.AbsoluteUri;
            return url;
        }

        public static string GetRelativeRequestUrlPath(this WebOperationContext source)
        {
            var iq = source.IncomingRequest;
            var result = iq.UriTemplateMatch.RelativePathSegments.ToArray().ToString("", "", "", "/");
            return result;
        }

        public static string GetWildcardPath(this WebOperationContext source)
        {
            var iq = source.IncomingRequest;
            var result = iq.UriTemplateMatch.WildcardPathSegments.ToArray().ToString("", "", "", "/");
            return result;
        }

        public static NameValueCollection GetQueryParamters(this WebOperationContext woc, Stream postData)
        {
            var ic = woc.IncomingRequest;
            var queryParams = ic.UriTemplateMatch.QueryParameters;
            if (ic.Method == WebRequestMethods.Http.Post)
            {
                var queryString = Encoding.UTF8.GetString(postData.ToBytes());
                var queryParams2 = HttpUtility.ParseQueryString(queryString);
                foreach (string key in queryParams2.Keys)
                {
                    var value = queryParams2[key];
                    value = Uri.UnescapeDataString(value);
                    queryParams.Set(key, value);
                }
            }
            return queryParams;
        }

        public static NameValueCollection GetQueryParamters2(this WebOperationContext woc, Stream postData)
        {
            var ic = woc.IncomingRequest;
            var queryParams = ic.UriTemplateMatch.QueryParameters;
            if (ic.Method == WebRequestMethods.Http.Post)
            {
                var queryString = Encoding.UTF8.GetString(postData.ToBytes());
                var queryParams2 = HttpUtility.ParseQueryString(queryString);
                foreach (string key in queryParams2.Keys)
                {
                    var value = queryParams2[key];
                    //value = Uri.UnescapeDataString(value);
                    queryParams.Set(key, value);
                }
            }
            return queryParams;
        }
    }

}

