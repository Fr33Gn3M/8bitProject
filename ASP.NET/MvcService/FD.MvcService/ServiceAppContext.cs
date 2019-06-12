using FD.DataBase;
using FD.DataModels;
using System;
using System.IO;
using System.Reflection;

namespace FD.MvcService
{
    public class ServiceAppContext
    {
        public static ServiceAppContext Instance = new ServiceAppContext();
        public void Init()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["ConntionString"].ToString();
            DBBase dbbase = new DBBase();
            DataBaseHelper = DBHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, connStr, GetNameSpaceName(dbbase.GetType()));
        }
        public IDataBaseHelper DataBaseHelper;

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