using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    public enum TaskStatus
    {
        InQueued,
        Running,
        Success,
        Rollbacking,
        Rollbacked,
        ErrorOccured
    }
}
