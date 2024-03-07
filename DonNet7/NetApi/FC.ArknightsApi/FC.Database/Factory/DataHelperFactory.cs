using FC.Database.DataHelper;
using FC.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.Factory
{
    public class DataHelperFactory
    {
        public static IDataHelper Init(SqlType sqlType, string connectionString)
        {
            IDataHelper classHelper = null;
            switch (sqlType)
            {
                case SqlType.MySql:
                    {
                        classHelper = new MySqlDataHelper(connectionString);
                        break;
                    }
            }
            return classHelper;
        }
    }
}
