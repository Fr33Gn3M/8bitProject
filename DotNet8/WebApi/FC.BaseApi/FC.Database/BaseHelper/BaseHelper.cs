using System.Data.Common;
using System.Data;
using FC.Database.EnumModels;

namespace FC.Database.BaseHelper
{
    internal static class BaseHelper
    {
        internal static DbConnection GetDbConnection(DatabaseType dbType, string connectionString)
        {
            DbConnection? Conn;
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(EnumUtil.DbTypeName(dbType));
            Conn = dbProviderFactory.CreateConnection();
            //得到连接字符串
            if(Conn == null)
            {
                throw new Exception("数据库连接失败");
            }
            return Conn;
        }
    }
}
