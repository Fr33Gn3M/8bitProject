using FC.Database.FilterModels;
using FC.Database.Models;

namespace FC.BaseService.Interface
{
    public interface IDataService
    { 

        Dictionary<string, object> Get(string resource, int id);

        PageQueryResult<T> Query<T>(string resource, PageQueryFilter filter);

    }
}
