using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace LX.Commons
{
    public class SysHttpHelper
    {
        private static readonly Encoding DEFAULTENCODE = Encoding.UTF8;

        #region http服务请求
        public static string PostForBody(string url, Dictionary<string, object> param = null)
        {
            var reqSetting = new HttpRequstSetting
            {
                Url = url,
                Method = "POST",
                ContentType = "application/json",
                PostBody = param
            };
            return HttpClientCore.GetHttpWebResult(reqSetting);
        }

        public static T PostForBody<T>(string url, Dictionary<string, object> param = null)
        {
            var res = PostForBody(url, param);
            return JsonConvert.DeserializeObject<T>(res);
        }

        public static string GetForBody(string url, Dictionary<string, object> param = null)
        {
            var reqSetting = new HttpRequstSetting
            {
                Url = url,
                Method = "GET",
                ContentType = "application/json",
                PostBody = param
            };
            return HttpClientCore.GetHttpWebResult(reqSetting);
        }

        public static T GetForBody<T>(string url, Dictionary<string, object> param = null)
        {
            var res = GetForBody(url, param);
            return JsonConvert.DeserializeObject<T>(res);
        }

        public static string PostForForm(string url, Dictionary<string, object> param = null, int timeout = 0)
        {
            var reqSetting = new HttpRequstSetting
            {
                Url = url,
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                PostBody = param
            };
            if (timeout > 0)
            {
                reqSetting.Timeout = timeout;
            }
            return HttpClientCore.GetHttpWebResult(reqSetting);

        }

        public static T PostForForm<T>(string url, Dictionary<string, object> param = null, int timeout = 0)
        {
            var res = PostForForm(url, param, timeout);
            return JsonConvert.DeserializeObject<T>(res);
        }

        public static string GetForForm(string url, Dictionary<string, object> param = null)
        {
            var reqSetting = new HttpRequstSetting
            {
                Url = url,
                Method = "GET",
                ContentType = "application/x-www-form-urlencoded",
                PostBody = param
            };
            return HttpClientCore.GetHttpWebResult(reqSetting);

        }

        public static T GetForForm<T>(string url, Dictionary<string, object> param = null)
        {
            var res = GetForForm(url, param);
            return JsonConvert.DeserializeObject<T>(res);
        }
        #endregion

        #region 文件上传
        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string file, NameValueCollection data)
        {
            return HttpUploadFile(url, file, data, DEFAULTENCODE);
        }

        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string file, NameValueCollection data, Encoding encoding)
        {
            return HttpUploadFile(url, new string[] { file }, data, encoding);
        }

        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string[] files, NameValueCollection data)
        {
            return HttpUploadFile(url, files, data, DEFAULTENCODE);
        }

        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string[] files, NameValueCollection data, Encoding encoding)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            //1.HttpWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetRequestStream())
            {
                //1.1 key/value
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                if (data != null)
                {
                    foreach (string key in data.Keys)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, data[key]);
                        byte[] formitembytes = encoding.GetBytes(formitem);
                        stream.Write(formitembytes, 0, formitembytes.Length);
                    }
                }

                //1.2 file
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    stream.Write(boundarybytes, 0, boundarybytes.Length);
                    string header = string.Format(headerTemplate, Path.GetFileName(files[i]), Path.GetFileName(files[i]));
                    byte[] headerbytes = encoding.GetBytes(header);
                    stream.Write(headerbytes, 0, headerbytes.Length);
                    using (FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                //1.3 form end
                stream.Write(endbytes, 0, endbytes.Length);
            }
            //2.WebResponse
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }
        #endregion
    }
}

