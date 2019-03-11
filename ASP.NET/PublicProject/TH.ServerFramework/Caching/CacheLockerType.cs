using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching
{
    [Flags()]
    internal enum CacheLockerType : int
    {
        None = 0,
        R = 2 ^ 0,
        W = 2 ^ 1,
        X = 2 ^ 2
    }
}

