using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.ServiceCatalog
{
    public enum ServiceStatus
    {
        None,
        Starting,
        Started,
        Pausing,
        Paused,
        Stoping,
        Stoped
    }
}
