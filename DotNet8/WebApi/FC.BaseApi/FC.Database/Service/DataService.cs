using FC.Database.DataHelper;
using FC.Database.FilterModels;
using FC.Database.Models;

namespace FC.Database.Service
{
    public class DataService : IDataService
    {
        private IDataHelper dataHelper;

        public DataService(IDataHelper dataHelper) { }

        public Dictionary<string, object> Get(string resource, int id)
        {
            return dataHelper.Get(resource, id);
        }

        public PageQueryDicResult Query(string resource, PageQueryFilter filter)
        {
            return dataHelper.Query(resource, filter);
        }

    }
}
