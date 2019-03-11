using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Logs
{
    public interface ILogManagerRepo
    {
        void Log( LogLevel level, string info);
        void Log( LogLevel level, object info);
        void Log<T>( LogLevel level, T info);
        void Log(string logName,  LogLevel level, string info);
        void Log(string logName,  LogLevel level, object info);
        void Log<T>(string logName,  LogLevel level, T info);
    }
}
