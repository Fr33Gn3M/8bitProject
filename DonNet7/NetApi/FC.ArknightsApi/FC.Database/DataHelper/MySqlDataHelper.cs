using FC.Database.BaseHelper;
using FC.Database.Enum;
using FC.Database.FilterModel;
using FC.Database.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FC.Database.DataHelper
{
    internal class MySqlDataHelper : IDataHelper
    {
        /// <summary>
        /// mysql数据库操作类
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public MySqlDataHelper(string connectionString) 
        {
            ConnectionString = connectionString;
            Connection = new MySqlConnection(connectionString);
            LoadDBInfo();
        }

        #region 数据库静态资源配置加载
        /// <summary>
        /// 供外部接口访问，重新装载数据库静态资源配置
        /// </summary>
        public void Reload()
        {
            LoadDBInfo();
        }

        /// <summary>
        /// 装载数据库静态资源配置
        /// </summary>
        private void LoadDBInfo()
        {
            LoadSchema();
            LoadTableInfos();
            LoadFieldInfos();
        }

        /// <summary>
        /// 加载模式名（数据库名称）
        /// </summary>
        private void LoadSchema()
        {
            Schema = ConnectionString.Substring(ConnectionString.LastIndexOf("="));
        }

        /// <summary>
        /// 加载表信息
        /// </summary>
        private void LoadTableInfos()
        {
            Connection.Open();
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = $"select `table_name`,`table_comment` from information_schema.tables where `table_schema`=@schema";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@schema",
                DbType = DbType.String,
                Value = Schema,
            });
            var result = ReadAll(cmd.ExecuteReader());
            //包装成{"tableName","tableNameCN"}的结构
            var dic = new SortedDictionary<string, string>();
            foreach(var item in result)
            {
                dic.Add(item["table_name"].ToString(), item["table_comment"].ToString());
            }
            TableInfos = dic;
            Connection.Close();
        }

        /// <summary>
        /// 加载字段信息
        /// </summary>
        private void LoadFieldInfos()
        {
            string schema = ConnectionString.Substring(ConnectionString.LastIndexOf("="));
            Connection.Open();
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = $"select `table_name`, `column_name`, `is_nullable`, `data_type`, `character_maximum_length`, `column_key`, `column_comment` from information_schema.columns where `table_schema`=@schema";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@schema",
                DbType = DbType.String,
                Value = Schema,
            });
            var result = ReadAll(cmd.ExecuteReader());
            //包装
            var dic = new Dictionary<string, List<DbFieldInfo>>();
            foreach (var item in result)
            {
                var tableName = item["table_name"].ToString();

                if (!dic.ContainsKey(tableName))
                {
                    List<DbFieldInfo> list = new List<DbFieldInfo>();
                    dic.Add(tableName, list);
                }
                var dbFieldInfo = DbFieldDicToDbFiledInfo(item);
                dic[tableName].Add(dbFieldInfo);
            }
            FieldInfos = dic;
            Connection.Close();
        }

        /// <summary>
        /// 数据库查询出来的字段信息dic转成DbFieldInfo对象
        /// </summary>
        /// <param name="dic">数据库查询出来的字段信息dic</param>
        /// <returns>DbFieldInfo对象</returns>
        private DbFieldInfo DbFieldDicToDbFiledInfo(Dictionary<string, object> dic)
        {
            DbFieldInfo dbFieldInfo = new DbFieldInfo();
            dbFieldInfo.Name = dic["column_name"].ToString();
            dbFieldInfo.NameCN = dic["column_comment"] == null? null : dic["column_comment"].ToString();
            dbFieldInfo.DbType = dbFieldInfo.DataTypeTransform(dic["data_type"].ToString());
            dbFieldInfo.IsNullable = dic["is_nullable"].ToString() == "NO"?false:true;
            dbFieldInfo.IsPrimaryKey = dic["column_key"] == null?false: dic["column_key"].ToString() == "PRI" ? true : false;
            if (dic["column_key"] != null)
            {
                dbFieldInfo.MaxLength = Convert.ToInt32(dic["character_maximum_length"]);
            }
            return dbFieldInfo;
        }
        #endregion

        #region 接口方法
        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="resource">资源名称</param>
        /// <param name="id">id</param>
        /// <returns>结果字典</returns>
        public Dictionary<string, object> Get(string resource, int id)
        {
            Connection.Open();
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM `{resource}` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = ReadAll(cmd.ExecuteReader());
            Connection.Close();
            return result.Count > 0 ? result.First() : null;
        }

        /// <summary>
        /// 根据过滤器查询结果集 字典列表
        /// </summary>
        /// <param name="resource">资源名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns>结果集 字典列表</returns>
        public List<Dictionary<string, object>> Query(string resource, PageQueryFilter filter)
        {
            var list = new List<Dictionary<string, object>>();
            string where = GetWhereString(filter);
            List<MySqlParameter> sqlParameterList = GetWhereParameterList(filter);
            string order = GetOrderString(filter);
            string field = GetFieldString(filter);
            string page = GetPageString(filter);

            string sql = string.Format("select {0} from `{1}` {2} {3} {4}", field, resource, where, order, page);

            Connection.Open();

            using var cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            foreach (var item in sqlParameterList)
            {
                cmd.Parameters.Add(item);
            }
            var result = ReadAll(cmd.ExecuteReader());
            
            Connection.Close();
            return result;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 获取where语句
        /// </summary>
        /// <param name="filter">分页查询过滤器</param>
        /// <returns>where语句</returns>
        private string GetWhereString(PageQueryFilter filter)
        {
            //DONE 拼接where语句
            string where = string.Empty;
            if (filter == null || filter.Filter == null || filter.Filter.Filters.Length <= 0) return where;

            StringBuilder builder = new StringBuilder();
            var baseFilter = filter.Filter;
            foreach(var item in baseFilter.Filters)
            {
                string str = string.Empty;
                if (item is QueryFilter)
                {
                    str = GetWhereString(item as QueryFilter);
                }
                if(item is MiniQueryFilter)
                {
                    str = GetWhereString(item as MiniQueryFilter);
                }

                if (item != baseFilter.Filters.First() && !string.IsNullOrEmpty(str))
                {
                    builder.Append(" " + baseFilter.SqlLogicOperator.ToString() + " ");
                }

                builder.Append(str);
            }
            where = string.Format("where {0}", builder.ToString());
            return where;
        }

        /// <summary>
        /// 获取where语句
        /// </summary>
        /// <param name="filter">标准查询过滤器</param>
        /// <returns>where语句</returns>
        private string GetWhereString(QueryFilter filter)
        {
            string where = string.Empty;
            StringBuilder builder = new StringBuilder();
            if (filter.Filters.Length <= 0) return where;
            //嵌套的where语句要有小括号
            builder.Append('(');
            foreach (var item in filter.Filters)
            {
                if (item != filter.Filters.First())
                {
                    builder.Append(" " + filter.SqlLogicOperator.ToString() + " ");
                }

                string str = string.Empty;
                if (item is QueryFilter)
                {
                    str = GetWhereString(item as QueryFilter);
                }
                if (item is MiniQueryFilter)
                {
                    str = GetWhereString(item as MiniQueryFilter);
                }
                builder.Append(str);
            }
            builder.Append(')');
            where = builder.ToString();
            return where;
        }

        /// <summary>
        /// 获取where语句
        /// </summary>
        /// <param name="filter">最小查询过滤器</param>
        /// <returns>where语句</returns>
        private string GetWhereString(MiniQueryFilter filter)
        {
            //TODO 拼接最小化where语句
            string where = string.Empty;
            StringBuilder builder = new StringBuilder();
            where = builder.ToString();
            return where;
        }

        /// <summary>
        /// 获取where语句 参数映射 对象列表
        /// </summary>
        /// <param name="filter">分页查询过滤器</param>
        /// <returns>MySqlParameter列表</returns>
        private List<MySqlParameter> GetWhereParameterList(PageQueryFilter filter)
        {
            //TODO 构造where语句参数 映射对象列表
            return null;
        }

        private string GetOrderString(PageQueryFilter filter)
        {
            //DONE 拼接order语句
            string order = string.Empty;
            if (filter == null || filter.Orders.Length <= 0) return order;
            StringBuilder builder = new();
            foreach (var item in filter.Orders)
            {
                builder.Append(string.Format(" `{0}` {1}", item.Field, item.OrderType.ToString()));
                if (item != filter.Orders.Last())
                {
                    builder.Append(',');
                }
            }
            order = string.Format("order by{0}", builder.ToString());
            return order;
        }

        private string GetFieldString(PageQueryFilter filter)
        {
            //DONE 拼接查询字段
            string field = "*";
            if (filter == null || filter.Fields.Length <= 0) return field;
            StringBuilder builder = new();
            foreach (var item in filter.Fields)
            {
                builder.Append(string.Format(" `{0}`", item));
                if (item != filter.Fields.Last())
                {
                    builder.Append(',');
                }
            }
            return field;
        }

        /// <summary>
        /// 拼接分页语句
        /// </summary>
        /// <param name="filter">过滤器对象</param>
        /// <returns>分页语句</returns>
        private string GetPageString(PageQueryFilter filter)
        {
            //DONE 拼接分页语句
            string pageLimit = string.Empty;
            if (filter.Page.IsPage)
            {
                //mysql limit分页查询公式 limit (n-1)*m,m; 
                //n:求第几页的数据
                //m:每页显示m条数据
                pageLimit = string.Format("limit {0},{1}", (filter.Page.Index - 1) * filter.Page.Size, filter.Page.Size);
            }
            return pageLimit;
        }

        /// <summary>
        /// 数据库语句执行
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>执行结果 字典列表</returns>
        private List<Dictionary<string, object>> ReadAll(DbDataReader reader)
        {
            var list = new List<Dictionary<string, object>>();
            using (reader)
            {
                while (reader.Read())
                {
                    var dic = DataReaderToDic(reader);
                    list.Add(dic);
                }
            }
            return list;
        }

        /// <summary>
        /// DataReader转Dictionary字典
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Dictionary字典</returns>
        private Dictionary<string, object> DataReaderToDic(IDataReader dataReader)
        {
            var dic = new Dictionary<string, object>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                dic.Add(dataReader.GetName(i), dataReader.GetValue(i));
            }
            return dic;
        }
        #endregion

        #region 属性
        /// <summary>
        /// mysql数据库连接
        /// </summary>
        public MySqlConnection Connection { get; }

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

    }
}
