using System;
using System.Collections.Generic;
using System.Web;
using LX.Commons.Common;
using NLog;

namespace LX.Commons.Logs
{
    public class LogHelper
    {
        //文本日志
        public static Dictionary<string, Logger> loggers = new Dictionary<string, Logger>();



        static LogHelper()
        {
            InitLogger();
        }

        public static Logger Current
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Items != null)
                {
                    if (!HttpContext.Current.Items.Contains("key")) return Logger.Default;
                    var key = HttpContext.Current.Items["key"].ToString();
                    if (loggers.ContainsKey(key))
                    {
                        var logger = loggers[key];
                        return logger;
                    }
                }
                return Logger.Default;
            }
        }


        private static void InitLogger()
        {
            var keys = ScopeManager.ConfigManager.GetMainProjectSetting("logger");
            if (keys != null)
            {
                var keyArr = keys.Split('|');
                foreach (var item in keyArr)
                {
                    var logDicArr = item.Split(':');
                    var logKey = logDicArr[0];

                    loggers[logKey] = new Logger(logKey);
                }
            }
        }

        public static void CollectLogInfo(string key, string msg, bool isError)
        {
            try
            {
                if (HttpContext.Current.Items.Contains(key))
                {
                    var infoList = HttpContext.Current.Items[key] as List<string>;
                    infoList.Add(msg);
                    HttpContext.Current.Items[key] = infoList;

                    if (!HttpContext.Current.Items.Contains("IsError"))
                        HttpContext.Current.Items.Add("IsError", false);
                    if ((bool)HttpContext.Current.Items["IsError"] == false && isError == true)
                    {
                        HttpContext.Current.Items["IsError"] = isError;
                    }
                }
            }
            catch (Exception ex)
            {

                LogHelper.Current.Info("CollectLogInfo ex:" + ex.Message);
            }

        }

        /// <summary>
        /// 创建请求时，设置每个请求的RequestId
        /// </summary>
        public static void CreateRequestId()
        {
            ScopeContext.PushProperty("RequestId", Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 请求结束后，清空ScopeContext
        /// </summary>
        public static void ClearScopeContext()
        {
            ScopeContext.Clear();
        }
    }
}