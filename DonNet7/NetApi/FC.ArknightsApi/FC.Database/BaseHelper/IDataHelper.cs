using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.BaseHelper
{
    public interface IDataHelper
    {
        Dictionary<string, object> Get(string resource, int id);
    }
}
