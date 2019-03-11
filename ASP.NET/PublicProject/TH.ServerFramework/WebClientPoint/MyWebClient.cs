namespace TH.ServerFramework.WebClientPoint
{
    using RestSharp.Contrib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class MyWebClient
    {
        public HttpWebResponse GetResponse(WebRequestContext webReqCtx)
        {
            HttpWebRequest httpReq = (HttpWebRequest) WebRequest.Create(webReqCtx.BuiltUrl);
            httpReq.Method = webReqCtx.Method;
            foreach (KeyValuePair<string, string> header in webReqCtx.Headers)
            {
                httpReq.Headers.Set(header.Key, header.Value);
            }
            if ((webReqCtx.Method == "POST") && webReqCtx.Params.Any<KeyValuePair<string, string>>())
            {
                byte[] bytes = this.UploadValuesInternal(httpReq, webReqCtx.Params);
                using (Stream reqStream = httpReq.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                }
            }
            return (HttpWebResponse) httpReq.GetResponse();
        }

        private byte[] UploadValuesInternal(WebRequest httpReq, IDictionary<string, string> data)
        {
            httpReq.ContentType = "application/x-www-form-urlencoded";
            string str2 = string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (string str3 in data.Keys)
            {
                builder.Append(str2);
                builder.Append(HttpUtility.UrlEncode(str3));
                builder.Append("=");
                builder.Append(HttpUtility.UrlEncode(data[str3]));
                str2 = "&";
            }
            return Encoding.ASCII.GetBytes(builder.ToString());
        }
    }
}

