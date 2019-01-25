using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using DAL;

namespace Computing
{
    public class ServiceAppContext
    {
        private static ServiceAppContext instance;
        public static ServiceAppContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new ServiceAppContext();
                return instance;
            }
        }

        public ServiceAppContext()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["ConntionString"].ToString();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, connStr, "");
        }

        public void Init()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["ConntionString"].ToString();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, connStr, "");
        }
        public IDataClassHelper DataBaseClassHelper;

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