using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using TH.DataBase;

namespace TH.HistoryDataManager
{
    public class ServiceAppContext
    {
        public ServiceAppContext()
        {
            var path = GetAssemblyPath(this.GetType());
            var strConn = System.Configuration.ConfigurationManager.AppSettings["HisConntionString"].ToString();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, strConn, GetNameSpaceName(this.GetType())); //new DataBaseClassHelper(DataBaseTableName.DataBaseKyFieldTableDic, DataBaseTableName.DataBaseNameSpaceTableDic, DataBaseTableName.TableToTableNameDic, strConn, prividerName);
            TokenCache = new MemoryCache("TokenCache");
        }

        public void Init()
        {
            var strConn = System.Configuration.ConfigurationManager.AppSettings["HisConntionString"].ToString();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, strConn, GetNameSpaceName(this.GetType()));
        }

        public MemoryCache TokenCache;
        public static ServiceAppContext Instance = new ServiceAppContext();
        public IDataClassHelper DataBaseClassHelper;
        public Dictionary<string, object> HistoryTableDic;
        public Dictionary<string, Dictionary<string, string>> ReplaceDic;

        public string GetUserName(Guid token)
        {
            if (TokenCache.Contains(token.ToString()))
                return TokenCache[token.ToString()].ToString();
            return null;
        }

        public static string GetAssemblyPath(Type type)
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(type).Location);
        }

        public static string GetNameSpaceName(Type type)
        {
            return type.Namespace;
        }

    }
}
