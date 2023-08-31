using FC.Database.BaseHelper;
using FC.Database.Enum;
using FC.Database.Factory;

namespace FC.Database
{
    public class AppDb
    {

        public AppDb(string dbType, string connectionString)
        {
            SqlType sqlType = (SqlType)System.Enum.Parse(typeof(SqlType), dbType);
            dataHelper = DataHelperFactory.Init(sqlType, connectionString);
        }

        public IDataHelper dataHelper { get; set; }
    }
}
