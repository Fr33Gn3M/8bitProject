using FC.Database.DataHelper;
using FC.Database.Service;
using FC.Database.EnumModels;
using FC.Database.Factory;

namespace FC.Database.Dao
{
    internal class Db : IDb
    {
        private IDataHelper dataHelper;

        public IDataService dataService;

        public Db(DatabaseType databaseType, string connectionString) 
        {
            dataHelper = DataHelperFactory.Init(databaseType, connectionString);
            dataService = new DataService(dataHelper);
        }

        public IDataService GetDataService()
        {
            return dataService;
        }
    }
}
