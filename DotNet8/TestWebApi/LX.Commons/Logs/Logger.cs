using System;
using LX.Commons.Common;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace LX.Commons.Logs
{
    public class Logger
    {

        public NLog.Logger logger;

        private Logger(NLog.Logger logger)
        {
            this.logger = logger;
        }

        public Logger(string name) : this(NLog.LogManager.GetLogger(name))
        {

        }


        public static Logger Default { get; private set; }
        public static Logger DBLogger { get; private set; }

        static Logger()
        {
            Default = new Logger(NLog.LogManager.GetCurrentClassLogger());
            DBLogger = new Logger("DBLogger");
            //动态设置在web.config里不同日志的target及rule
            var config = LogManager.Configuration;
            //读取web.config 的logger日志配置，格式 logkey1:logkey1CN|logkey2:logkey2CN
            //logkeyCN作为文件夹的中文名展示，没配置的情况下默认使用logkey1作为文件名的一部分
            string logkeys = ScopeManager.ConfigManager.GetMainProjectSetting("logger");
            var logkeyArr = logkeys.Split('|');
            foreach (var logDic in logkeyArr)
            {
                var logDicArr = logDic.Split(':');
                var logKey = logDicArr[0];
                var logKeyCN = logDicArr.Length > 1 ? logDicArr[1] : logKey;

                //每种日志创建3个日志级别
                var infoFileTarget = new FileTarget
                {
                    Name = logKey + "_info_htm",
                    MaxArchiveFiles = 90,
                    ArchiveNumbering = ArchiveNumberingMode.Sequence,

                    ArchiveAboveSize = 5000000,
                    FileName = "../Logs/Info/Info_" + logKeyCN + "/${shortdate}/info.htm",
                    Header = "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" />",
                    Layout = "<p><span style=\"color:red; font - weight:bold; \">[${level}]</span><br />" +
                    " <span style=\"color:#666\">${longdate}</span><br />" +
                    " <span style=\"color:#666\">${scopeproperty:item=RequestId}</span><br />" +
                    "<span style=\"color:#0066FF; font-weight:bold;\">[${stacktrace}]</span><br />" +
                    "<span style=\"color:#0066FF;font-family:KaiTi_GB2312;font-size:18px;\">${message}${onexception:${exception:format=tostring}</span></p>"
                };

                config.AddTarget(infoFileTarget);
                var warnFileTarget = new FileTarget
                {
                    Name = logKey + "_warn_htm",
                    MaxArchiveFiles = 90,
                    ArchiveNumbering = ArchiveNumberingMode.Sequence,
                    ArchiveAboveSize = 5000000,
                    FileName = "../Logs/Warn/Warn_" + logKeyCN + "/${shortdate}/warn.htm",
                    Header = "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" />",
                    Layout = "<p><span style=\"color:red; font - weight:bold; \">[${level}]</span><br />" +
                    " <span style=\"color:#666\">${longdate}</span><br />" +
                    " <span style=\"color:#666\">${scopeproperty:item=RequestId}</span><br />" +
                    "<span style=\"color:#0066FF; font-weight:bold;\">[${stacktrace}]</span><br />" +
                    "<span style=\"color:#0066FF;font-family:KaiTi_GB2312;font-size:18px;\">${message}${onexception:${exception:format=tostring}</span></p>"
                };

                config.AddTarget(warnFileTarget);

                var errorFileTarget = new FileTarget
                {
                    Name = logKey + "_error_htm",
                    MaxArchiveFiles = 90,
                    ArchiveNumbering = ArchiveNumberingMode.Sequence,
                    ArchiveAboveSize = 5000000,
                    FileName = "../Logs/Error/Error_" + logKeyCN + "/${shortdate}/error.htm",
                    Header = "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" />",
                    Layout = "<p><span style=\"color:red; font - weight:bold; \">[${level}]</span><br />" +
                    " <span style=\"color:#666\">${longdate}</span><br />" +
                    " <span style=\"color:#666\">${scopeproperty:item=RequestId}</span><br />" +
                    "<span style=\"color:#0066FF; font-weight:bold;\">[${stacktrace}]</span><br />" +
                    "<span style=\"color:#0066FF;font-family:KaiTi_GB2312;font-size:18px;\">${message}${onexception:${exception:format=tostring}</span></p>"
                };

                config.AddTarget(errorFileTarget);

                //创建规则，每个日志级别一个规则，目标指向上面添加的目标
                var infoRule = new LoggingRule(logKey, LogLevel.Info, infoFileTarget);
                var warnRule = new LoggingRule(logKey, LogLevel.Warn, LogLevel.Warn, warnFileTarget);
                var errorRule = new LoggingRule(logKey, LogLevel.Error, LogLevel.Error, errorFileTarget);

                config.LoggingRules.Add(infoRule);
                config.LoggingRules.Add(warnRule);
                config.LoggingRules.Add(errorRule);
            }

            LogManager.Configuration = config;
        }

        public void Debug(string msg, params object[] args)
        {
            logger.Debug(msg, args);
        }

        public void Debug(Exception err, string msg)
        {
            logger.Debug(err, msg);
        }

        /// <summary>
        /// 示例用法 LogHelper.Current.Info("yes:{0},no:{1}",new object[]{ "1","2" });
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Info(string msg, params object[] args)
        {
            logger.Info(msg, args);
        }

        public void Info(Exception err, string msg)
        {
            logger.Info(err, msg);
        }

        public void Trace(string msg, params object[] args)
        {
            logger.Trace(msg, args);
        }

        public void Trace(Exception err, string msg)
        {
            logger.Trace(err, msg);
        }

        public void Error(string msg, params object[] args)
        {
            logger.Error(msg, args);
        }

        public void Error(Exception err, string msg)
        {
            logger.Error(err, msg);
        }

        public void Fatal(string msg, params object[] args)
        {
            logger.Fatal(msg, args);
        }

        public void Fatal(Exception err, string msg)
        {
            logger.Fatal(err, msg);
        }

        public void Warn(string msg, params object[] args)
        {
            logger.Warn(msg, args);
        }

        public void Warn(Exception err, string msg)
        {
            logger.Warn(err, msg);
        }

    }
}
