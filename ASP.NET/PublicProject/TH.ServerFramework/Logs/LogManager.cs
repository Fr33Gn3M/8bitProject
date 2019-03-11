using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Logs
{
   public class LogManager
    {
       public static LogManagerRepo Logger = new LogManagerRepo();
       public const string SystemLog = "systemLog";
       public const string SystemErrorLog = "systemErrorLog";
       public const string DbErrorLog = "DbErrorLog";
       public const string DbLog = "DbLog";
       public const string TestLog = "TestLog";
    }
}
