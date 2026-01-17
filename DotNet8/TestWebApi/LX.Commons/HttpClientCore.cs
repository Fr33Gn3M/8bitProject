using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using LX.Commons.Logs;

namespace LX.Commons
{
    public class HttpClientCore
    {
        public static HttpWebRequest GetHttpWebRequest(HttpRequstSetting reqSetting)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            HttpWebRequest request = null;

            if (reqSetting.Method.ToUpper() == "GET" && reqSetting.PostBody != null && reqSetting.PostBody.Count > 0)
            {
                var paramString = string.Join("&", reqSetting.PostBody.Select(pattern => pattern.Key + "=" + pattern.Value));
                reqSetting.Url = reqSetting.Url + "?" + paramString;
            }

            if (reqSetting.IsSpecialEncode)
            {
                reqSetting.Url = reqSetting.Url.Replace("[", "%5B").Replace("]", "%5D");
                Logger.Default.Info("[]转义http请求：" + reqSetting.Url);
            }
            request = WebRequest.Create(reqSetting.Url) as HttpWebRequest;
            //如果是发送HTTPS请求 
            if (reqSetting.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request.ProtocolVersion = HttpVersion.Version11;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            }
            request.Timeout = reqSetting.Timeout;
            request.Method = reqSetting.Method;
            request.Accept = reqSetting.Accept;
            request.ContentType = reqSetting.ContentType;

            if (reqSetting.Header != null && reqSetting.Header.Count > 0)
            {
                foreach (var item in reqSetting.Header)
                {
                    //SetHeaderValue(request.Headers, item.Key, item.Value.ToString());
                    request.Headers.Add(item.Key, item.Value.ToString());
                }
            }


            if (reqSetting.Method.ToUpper() == "POST" && !reqSetting.ContentType.Contains("application/json") && !(reqSetting.PostBody == null || reqSetting.PostBody.Count == 0))
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
            if (reqSetting.Method.ToUpper() == "POST" && reqSetting.ContentType.Contains("application/json") && !(reqSetting.PostBody == null || reqSetting.PostBody.Count == 0))
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(reqSetting.PostBody);

                    streamWriter.Write(json);
                }
            }


            return request;
        }

        public static string GetHttpWebResult(HttpRequstSetting reqSetting)
        {
            var request = GetHttpWebRequest(reqSetting);
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            string strResult = string.Empty;
            try
            {
                var getresult = request.GetResponse() as HttpWebResponse;
                Stream streamReceive = getresult.GetResponseStream();
                var streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                LX.Commons.Logs.LogHelper.Current.Info("推送失败--" + ex.Message);
                throw ex;
            }
            return strResult;
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        public static long DownLoadFile(string url, string fileName)
        {
            long value = 0;
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = null;
                request = WebRequest.Create(url) as HttpWebRequest;
                response = request.GetResponse();
                stream = response.GetResponseStream();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    SaveBinaryFile(response, fileName);
                    value = response.ContentLength;
                }
            }
            catch (Exception)
            {
                value = 0;
            }
            return value;
        }

        private static bool SaveBinaryFile(WebResponse response, string fileName)
        {
            bool value = true;
            byte[] buffer = new byte[1024];
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);

                }
                Stream outStream = System.IO.File.Create(fileName);
                Stream inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                    {
                        outStream.Write(buffer, 0, l);
                    }
                }
                while (l > 0);
                outStream.Close();
                inStream.Close();
            }
            catch (Exception)
            {
                value = false;
            }
            return value;
        }
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
        public string Url { get; set; }

        public string Method { get; set; } = "get";

        public string ContentType { get; set; } = "application/json";

        public bool IsHttpsService { get; set; } = false;

        public string Accept { get; set; } = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";

        public string Encoding { get; set; } = "UTF-8";

        public IDictionary<string, object> PostBody { get; set; }

        public bool UseDefaultCredentials { get; set; } = false;

        public IDictionary<string, object> Header { get; set; }

        /// <summary>
        /// 有些特殊的post请求，有url参数，而url参数里如果有类似"[]"这样的字符会报400，这时候需要转义成%5B和%5D
        /// </summary>
        public bool IsSpecialEncode { get; set; } = false;

        //请求超时时间，默认60秒
        public int Timeout { get; set; } = 60000;

    }
}