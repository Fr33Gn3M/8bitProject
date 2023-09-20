using FC.Database.FilterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.BaseHelper
{
    public interface IDataHelper
    {
        void Reload();

        Dictionary<string, object> Get(string resource, int id);

        List<Dictionary<string, object>> Query(string resource, PageQueryFilter filter);
    }
}
