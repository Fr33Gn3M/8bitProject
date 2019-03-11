using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cn.jpush.api;
using cn.jpush.api.common.resp;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TH.Commons;

namespace TH.JpushManager
{
    public class JpushApiHelper
    { 
        private static JpushApiHelper instance;
        public static JpushApiHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new JpushApiHelper();
                return instance;
            }
        }
        private JPushClient client;

        JpushApiHelper()
        {
            string app_key = System.Configuration.ConfigurationManager.AppSettings["jpush_app_key"];
            string master_secret = System.Configuration.ConfigurationManager.AppSettings["jpush_master_secret"];

            client = new JPushClient(app_key, master_secret);
        }
        
        public bool PushSMSByXXB(CusPushModel model)
        {
            try
            {
                PushPayload payload = new PushPayload();
                var dt = (PlatformType)Enum.Parse(typeof(PlatformType),model.PlatformType.ToLower().Replace("pc_",""));
                payload.platform = getPlatformByDeviceType(dt);
                payload.audience = Audience.s_registrationId(model.Audience);
                var notification = new Notification().setAlert(model.Alert);
                notification.AndroidNotification = new AndroidNotification().setTitle(model.Title);
                payload.notification = notification;
                payload.message = Message.content(model.MessContent);
                var result = client.SendPush(payload);
            }
            catch (APIRequestException e)
            {
                CommonHelper.WriteLog(model.Audience[0] + "推送错误，可能时当前用户未登录过。错误提示：" + e.Message);
            }
            catch (APIConnectionException e)
            {
                CommonHelper.WriteLog(e.Message);
            }
            return true;

        }

        public Platform getPlatformByDeviceType(PlatformType dt)
        {
            switch (dt)
            {
                case PlatformType.android: return Platform.android();
                case PlatformType.ios: return Platform.ios();
                case PlatformType.winphone: return Platform.winphone();
                case PlatformType.android_ios: return Platform.android_ios();
                case PlatformType.android_winphone: return Platform.winphone();
                case PlatformType.ios_winphone: return Platform.ios_winphone();
                case PlatformType.all: return Platform.all();
                default: return Platform.all();
            }
        }
    }
}