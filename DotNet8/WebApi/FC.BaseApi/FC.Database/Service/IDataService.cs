using FC.Database.FilterModels;
using FC.Database.Models;

namespace FC.Database.Service
{
    public interface IDataService
    {
        Dictionary<string, object> Get(string resource, int id);

        PageQueryDicResult Query(string resource, PageQueryFilter filter);

        
    }
}
