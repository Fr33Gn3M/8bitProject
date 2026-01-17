using FC.Database.DataHelper;
using FC.Database.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.Factory
{
    public class DataHelperFactory
    {
        public static IDataHelper Init(DatabaseType sqlType, string connectionString)
        {
            IDataHelper classHelper;
            switch (sqlType)
            {
                case DatabaseType.SqlServer:
                    {
                        classHelper = new SqlServerDataHelper(connectionString);
                        break;
                    }
                default:
                    {
                        classHelper = new SqlServerDataHelper(connectionString);
                        break;
                    }
            }
            return classHelper;
        }
    }
}
