using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
   public class TimedTaskStatus
    {
       public DateTime EventTime { get; set; }
       public TaskStatus Status { get; set; }
    }
}
