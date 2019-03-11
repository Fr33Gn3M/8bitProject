using FD.DataBase;
using FD.DataModels;
using FD.HistoryDataManager.Impl;
using FD.HistoryDataManager.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace FD.ModelCreator.WebSerice
{
    public class ServiceAppContext
    {
        private ServiceAppContext()
        {
            Init();
        }
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
        private void Init()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["ConntionString"].ToString();
            DBBase dbbase = new DBBase();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, connStr, GetNameSpaceName(dbbase.GetType()));
            if(System.Configuration.ConfigurationManager.AppSettings["HisConntionString"]!=null)
                HistroyBaseManage = HistoryManage.Instant;
        }
        public void ReInit()
        {
            instance = null;
            if (instance == null)
                instance = new ServiceAppContext();
            else Init();
        }
        public IDataClassHelper DataBaseClassHelper;
        public IHistoryManage HistroyBaseManage = null;

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