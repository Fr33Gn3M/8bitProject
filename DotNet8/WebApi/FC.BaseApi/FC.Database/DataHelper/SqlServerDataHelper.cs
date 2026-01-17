using FC.Database.BaseHelper;
using FC.Database.EnumModels;
using FC.Database.FilterModels;
using FC.Database.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace FC.Database.DataHelper
{
    internal class SqlServerDataHelper : IDataHelper
    {
        /// <summary>
        /// mysql数据库操作类
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SqlServerDataHelper(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            Connection = BaseHelper.BaseHelper.GetDbConnection(DatabaseType.SqlServer, connectionString);
        }

        #region 属性
        /// <summary>
        /// mysql数据库连接
        /// </summary>
        public DbConnection Connection { get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 模式（其实就是数据库名）
        /// </summary>
        public string Schema { get; private set; }

        /// <summary>
        /// 表名，表注释 list
        /// </summary>
        public IDictionary<string, string> TableInfos { get; private set; } = new SortedDictionary<string, string>();

        /// <summary>
        /// 表名，字段信息 List
        /// </summary>
        public IDictionary<string, List<DbFieldInfo>> FieldInfos { get; private set; } = new Dictionary<string, List<DbFieldInfo>>();
        #endregion

        #region 数据库静态资源配置加载

        #endregion

        #region 数据操作接口

        public Dictionary<string, object> Get(string resource, int id)
        {
            throw new NotImplementedException();
        }

        public PageQueryResult<T> Query<T>(string resource, PageQueryFilter filter)
        {
            throw new NotImplementedException();
        }

        public PageQueryDicResult Query(string resource, PageQueryFilter filter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}