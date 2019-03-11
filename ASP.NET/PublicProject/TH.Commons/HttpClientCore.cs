using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using System.IO;

namespace TH.Commons
{
    public class HttpClientCore
    {
        public static HttpWebRequest getHttpWebRequest(HttpRequstSetting reqSetting)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            HttpWebRequest request = null;

            request = WebRequest.Create(reqSetting.Url) as HttpWebRequest;
            //如果是发送HTTPS请求 
            if (reqSetting.IsHttpsService)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request.ProtocolVersion = HttpVersion.Version11;
            }
            request.Method = reqSetting.Method;
            request.Accept = reqSetting.Accept;
            request.ContentType = reqSetting.ContentType;

            if (reqSetting.Method.ToUpper() == "POST" && !(reqSetting.PostBody == null || reqSetting.PostBody.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in reqSetting.PostBody.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, reqSetting.PostBody[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, reqSetting.PostBody[key]);
                    }
                    i++;
                }

                byte[] data = encoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request;
        }

        //public static HttpWebResponse getHttpWebResponse()
        //{

        //}

        /// <summary>
        /// https服务验证方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        /// <summary>
        /// C# 的UrlEncode方法重写，效仿Java，将编码部分大写，其余部分不变
        /// </summary>
        /// <param name="temp">需要编码的串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>编码后的串</returns>
        public static string UrlEncode(string temp, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                string t = temp[i].ToString();
                string k = HttpUtility.UrlEncode(t, encoding);
                if (t == k)
                {
                    stringBuilder.Append(t);
                }
                else
                {
                    stringBuilder.Append(k.ToUpper());
                }
            }
            return stringBuilder.ToString();
        }
    }

    public class HttpRequstSetting
    {
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private string method = "get";

        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        private string contentType = "application/json";

        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        private bool isHttpsService = false;

        public bool IsHttpsService
        {
            get { return isHttpsService; }
            set { isHttpsService = value; }
        }

        private string accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";

        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }

        private string encoding = "UTF-8";

        public string Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        private IDictionary<string, object> postBody;

        public IDictionary<string, object> PostBody
        {
            get { return postBody; }
            set { postBody = value; }
        }
    }
}