using FC.Database.FilterModels;
using FC.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.DataHelper
{
    public interface IDataHelper
    {
        Dictionary<string, object> Get(string resource, int id);

        PageQueryResult<T> Query<T>(string resource, PageQueryFilter filter);

        PageQueryDicResult Query(string resource, PageQueryFilter filter);
    }
}
