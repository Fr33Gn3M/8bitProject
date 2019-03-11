using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Caching;
using TH.Commons;

namespace TH.UserServiceManager
{
    /// <summary>
    /// 企业微信登录认证类
    /// </summary>
    public class WeiXinAuthHelper
    {

        public static string autoAuth(string suiteId, string suiteSecret, string redirectUrl, string suiteTicket)
        {
            string accessToken = getAccessToken(suiteId, suiteSecret, suiteTicket);
            string code = getCode(suiteId, redirectUrl);
            string userId = getOpenIdByCode(accessToken, code);
            return userId;
        }

        public static string getAccessToken(string suiteId, string suiteSecret, string suiteTicket)
        {
            object objCache = System.Web.HttpRuntime.Cache.Get(suiteId);
            if (objCache != null)        //判断缓存是否失效
            {
                //如果没有失效，则取出。
                return objCache.ToString();
            }
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/get_suite_token");
            var reqSetting = new HttpRequstSetting();
            reqSetting.Url = url;
            reqSetting.Method = "POST";
            reqSetting.IsHttpsService = true;
            reqSetting.PostBody.Add("suite_id", suiteId);
            reqSetting.PostBody.Add("suite_secret", suiteSecret);
            reqSetting.PostBody.Add("suite_ticket", suiteTicket);
            var request = HttpClientCore.getHttpWebRequest(reqSetting);

            try
            {
                var getresult = request.GetResponse() as HttpWebResponse;
                Stream streamReceive = getresult.GetResponseStream();
                var streamReader = new StreamReader(streamReceive, encoding);
                var strResult = streamReader.ReadToEnd();
                var field = JavaScriptSerializer.Json.Deserialize<JObject>(strResult);
                if (field["errcode"].ToString() == "0")
                {
                    var x = int.Parse(field["expires_in"].ToString());
                    System.Web.HttpRuntime.Cache.Add(suiteId, field["suite_access_token"].ToString(), null, DateTime.Now.AddSeconds(x), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    return field["suite_access_token"].ToString();
                }
            }
            catch (WebException ex)
            {

            }
            return null;
        }

        public static string getCode(string suiteId, string redirectUrl)
        {       
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            string redirectUri = HttpUtility.UrlEncode(redirectUrl, encoding);

            var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base#wechat_redirect", suiteId, redirectUri);

            var reqSetting = new HttpRequstSetting();
            reqSetting.Url = url;
            reqSetting.Method = "POST";
            reqSetting.IsHttpsService = true;
            var request = HttpClientCore.getHttpWebRequest(reqSetting);
            return null;
        }

        public static string getOpenIdByCode(string accessToken, string code)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");

            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/getuserinfo3rd?access_token={0}&code={1}", accessToken, code);
            var reqSetting = new HttpRequstSetting();
            reqSetting.Url = url;
            reqSetting.Method = "POST";
            reqSetting.IsHttpsService = true;
            var request = HttpClientCore.getHttpWebRequest(reqSetting);
            try
            {
                var getresult = request.GetResponse() as HttpWebResponse;
                Stream streamReceive = getresult.GetResponseStream();
                var streamReader = new StreamReader(streamReceive, encoding);
                var strResult = streamReader.ReadToEnd();
                var field = JavaScriptSerializer.Json.Deserialize<JObject>(strResult);
                if (field["errcode"].ToString() == "0")
                {
                    return field["OpenId"].ToString();
                }
            }
            catch (WebException ex)
            {

            }
            return null;
        }


    }
}
