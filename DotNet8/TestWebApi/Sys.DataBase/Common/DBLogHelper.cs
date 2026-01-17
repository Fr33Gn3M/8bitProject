using System;
using LX.Commons.Logs;

namespace Sys.DataBase.Common
{
    internal class DBLogHelper
    {
        #region 数据库日志记录方法（封装nlog）

        /// <summary>
        /// 封装的数据执行sql 信息
        /// 1、记录日志
        /// </summary>
        public static void InfoLog(string msg)
        {
            string logMsg = "数据库语句执行 信息：" + msg;
            Logger.DBLogger.Info(logMsg);
        }

        /// <summary>
        /// 封装的数据执行sql 异常处理方法(抛出异常)
        /// 1、记录异常日志
        /// 2、抛出异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="msg">错误信息</param>
        public static void ErrorLog(Exception ex, string msg)
        {
            string logMsg = "数据库语句执行 错误：" + msg;
            Logger.DBLogger.Error(ex, logMsg);
            throw new Exception(logMsg);
        }

        /// <summary>
        /// 封装的数据执行sql 不规范处理方法(不抛出异常，仅记录警告日志)
        /// 1、记录异常日志
        /// </summary>
        /// <param name="msg">警告信息</param>
        public static void WarnLog(string msg)
        {
            string logMsg = "数据库语句执行 警告：" + msg;
            Logger.DBLogger.Warn(logMsg);
        }

        #endregion
    }
}
