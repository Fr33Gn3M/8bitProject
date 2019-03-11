using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.Caching
{
    internal class CacheIndexItem
    {
        public System.DateTime CreatedDate { get; set; }
        public CacheLockerType LockerType { get; set; }
    }

}
