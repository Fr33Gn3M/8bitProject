using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    public interface ITaskTransaction
    {
        void OnRollback();
    }
}
