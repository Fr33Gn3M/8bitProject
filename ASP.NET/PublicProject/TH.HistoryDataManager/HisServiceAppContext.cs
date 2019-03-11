using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using TH.DataBase;
using System.Xml.Serialization;
using TH.DataModels;

namespace TH.HistoryDataManager
{
    public class HisServiceAppContext
    {
        public HisServiceAppContext()
        {
            var path = GetAssemblyPath(this.GetType());
            var strSourceConn = System.Configuration.ConfigurationManager.AppSettings["ConntionString"].ToString();
            var strConn = System.Configuration.ConfigurationManager.AppSettings["HisConntionString"].ToString();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, strSourceConn , GetNameSpaceName(this.GetType()));
            HisDataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, strConn,  GetNameSpaceName(this.GetType()));
             //new DataBaseClassHelper(DataBaseTableName.DataBaseKyFieldTableDic, DataBaseTableName.DataBaseNameSpaceTableDic, DataBaseTableName.TableToTableNameDic, strConn, prividerName);
            TokenCache = new MemoryCache("TokenCache");
        }

        public void Init()
        {
            var strSourceConn = System.Configuration.ConfigurationManager.AppSettings["ConntionString"].ToString();
            var strConn = System.Configuration.ConfigurationManager.AppSettings["HisConntionString"].ToString();
            DataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, strSourceConn, GetNameSpaceName(this.GetType()));
            HisDataBaseClassHelper = SqlHelperFactory.GetSqlDataClassHelper(SqlPrividerType.SqlClient, strConn,  GetNameSpaceName(this.GetType()));
        }

        public MemoryCache TokenCache;
        public IDataClassHelper DataBaseClassHelper;
        public IDataClassHelper HisDataBaseClassHelper;
        private static HisServiceAppContext instance;
        public static HisServiceAppContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new HisServiceAppContext();
                return instance;
            }
        }
        private IDictionary<string, string> m_correlationTableDic;
        public IDictionary<string, string> CorrelationTableDic
        {
            get
            {
                if (m_correlationTableDic == null)
                    InitCorrelationTableDic();
                return m_correlationTableDic;
            }
        }
        private IDictionary<string, Dictionary<string, string>> m_HistoryTableDic;
        public IDictionary<string, Dictionary<string, string>> HistoryTableDic
        {
            get
            {
                if (m_HistoryTableDic == null)
                    InitHistoryTableDic();
                return m_HistoryTableDic;
            }
        }
        private IDictionary<string, Dictionary<string,string>> m_ShowFieldDic;
        public IDictionary<string, Dictionary<string, string>> ShowFieldDic
        {
            get
            {
                if (m_ShowFieldDic == null)
                    InitTableFields();
                return m_ShowFieldDic;
            }
        }

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

        private void InitCorrelationTableDic()
        {
            var correlationDic = new Dictionary<string, string>();
            var queryFilter = QueryPageFilter.Create("SYS_LSBMDZB");
            var res = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(queryFilter);
            correlationDic = (from item in res select new {ybm=item["YBM"],lsbm=item["LSBM"]}).ToDictionary(k=>k.ybm.ToString(),k=>k.lsbm.ToString());
            m_correlationTableDic = correlationDic;
        }

        private void InitHistoryTableDic()
        {
            var historyDic = new Dictionary<string, Dictionary<string, string>>();
            var queryFilter = QueryPageFilter.Create("SYS_LSBMDZB");
            var res = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(queryFilter);
            if (res.Count() > 0)
            {
                foreach (var item in res)
                {
                    var zdic = new Dictionary<string, string>();
                    foreach (var zd in item)
                    {
                        if (zd.Value != null && !string.IsNullOrEmpty(zd.Value.ToString()))
                            zdic.Add(zd.Key, zd.Value.ToString());
                    }
                    historyDic.Add(item["LSBM"].ToString(), zdic);
                }
            }
            m_HistoryTableDic = historyDic;
        }

        private void InitTableFields()
        {
            var ReplaceDic = new Dictionary<string, Dictionary<string, string>>();
            var queryFilter3 = QueryPageFilter.Create(DataBaseTableName.VWZTYWYYZDBTableName).Equal(VWZTYWYYZDB.SFSCFieldName, false).Equal(VWZTYWYYZDB.SFJLLSFieldName, true);
            var sjzdbls = DataBaseClassHelper.GetQueryResultN(queryFilter3);
            if (sjzdbls.Length <= 0) return;
            foreach (var item in sjzdbls)
            {
                if (ReplaceDic.ContainsKey(item["SSBM"].ToString()))
                {
                    ReplaceDic[item["SSBM"].ToString()].Add(item["ZDMC"].ToString(), item["XSM"].ToString());
                }
                else
                {
                    ReplaceDic.Add(item["SSBM"].ToString(), new Dictionary<string, string>() { { item["ZDMC"].ToString(), item["XSM"].ToString() } });
                }
            }
            m_ShowFieldDic = ReplaceDic;
        }
    }
}
