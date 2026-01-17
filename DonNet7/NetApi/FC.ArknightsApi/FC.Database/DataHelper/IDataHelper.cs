using FC.Database.FilterModel;
using FC.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.DataHelper
{
    public interface IDataHelper
    {
        void Reload();

        Dictionary<string, object> Get(string resource, int id);

        PageQueryResult Query(string resource, PageQueryFilter filter);
    }
}
