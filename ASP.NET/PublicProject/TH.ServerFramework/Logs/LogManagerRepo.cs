using TH.ServerFramework.Logs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systemNLog = NLog;

namespace TH.ServerFramework.Logs
{
    //实现日志的记录功能
    public class LogManagerRepo : ILogManagerRepo
    {
        internal Logger CurrLogger;
        public LogManagerRepo()
        {
            CurrLogger = systemNLog.LogManager.GetLogger(LogManager.SystemLog);
        }

        public void Log(LogLevel level, string info)
        {
            CurrLogger = systemNLog.LogManager.GetLogger(LogManager.SystemLog);
            var loglevel = ChangedLogLevel(level);
            CurrLogger.Log(loglevel, info);
        }

        public void Log(LogLevel level, object info)
        {
            CurrLogger = systemNLog.LogManager.GetLogger(LogManager.SystemLog);
            var loglevel = ChangedLogLevel(level);
            CurrLogger.Log(loglevel, info);
        }

        public void Log<T>(LogLevel level, T info)
        {
            CurrLogger = systemNLog.LogManager.GetLogger(LogManager.SystemLog);
            var loglevel = ChangedLogLevel(level);
            CurrLogger.Log<T>(loglevel, info);
        }

        private static systemNLog.LogLevel ChangedLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return systemNLog.LogLevel.Debug;
                case LogLevel.Error:
                    return systemNLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return systemNLog.LogLevel.Fatal;
                case LogLevel.Info:
                    return systemNLog.LogLevel.Info;
                case LogLevel.Off:
                    return systemNLog.LogLevel.Off;
                case LogLevel.Trace:
                    return systemNLog.LogLevel.Trace;
                case LogLevel.Warn:
                    return systemNLog.LogLevel.Warn;
            }
            return systemNLog.LogLevel.Info;
        }

        public void Log(string logName, LogLevel level, string info)
        {
            CurrLogger = systemNLog.LogManager.GetLogger(logName);
            var loglevel = ChangedLogLevel(level);
            CurrLogger.Log(loglevel, info);
        }

        public void Log(string logName, LogLevel level, object info)
        {
            CurrLogger = systemNLog.LogManager.GetLogger(logName);
            var loglevel = ChangedLogLevel(level);
            CurrLogger.Log(loglevel, info);
        }

        public void Log<T>(string logName, LogLevel level, T info)
        {
            CurrLogger = systemNLog.LogManager.GetLogger(logName);
            var loglevel = ChangedLogLevel(level);
            CurrLogger.Log<T>(loglevel, info);
        }
    }
}
