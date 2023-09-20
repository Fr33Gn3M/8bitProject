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
            //数据库查询工厂类初始化，生产数据库查询帮助类
            DataHelper = DataHelperFactory.Init(sqlType, connectionString);
        }

        /// <summary>
        /// 数据库查询帮助类
        /// </summary>
        public IDataHelper DataHelper { get; set; }
    }
}
