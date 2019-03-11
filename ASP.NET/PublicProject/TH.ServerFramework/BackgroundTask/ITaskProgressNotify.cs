using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    public interface ITaskProgressNotify
    {
        bool HasPropgress();
        double GetTotalProgress();
        double GetCurrentProgress();
        string GetCurrentProgressText();
        bool IsComplated();
        Exception GetLastException();

    }
}
