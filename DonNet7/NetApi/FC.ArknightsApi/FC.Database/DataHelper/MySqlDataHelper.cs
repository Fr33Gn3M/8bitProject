using FC.Database.Enum;
using FC.Database.FilterModel;
using FC.Database.Model;
using MySqlConnector;
using System.Data;
using System.Text;
using FC.Database.BaseHelper;

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
            string sql = $"select `table_name`,`table_comment` from information_schema.tables where `table_schema`=@schema";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>
            {
                new MySqlParameter
                {
                    ParameterName = "@schema",
                    DbType = DbType.String,
                    Value = Schema,
                }
            };
            var result = DBHelper.Instance.Execute(Connection, sql, mySqlParameters);
            //包装成{"tableName","tableNameCN"}的结构
            var dic = new SortedDictionary<string, string>();
            foreach(var item in result)
            {
                dic.Add(item["table_name"].ToString(), item["table_comment"].ToString());
            }
            TableInfos = dic;
        }

        /// <summary>
        /// 加载字段信息
        /// </summary>
        private void LoadFieldInfos()
        {
            string sql = $"select `table_name`, `column_name`, `is_nullable`, `data_type`, `character_maximum_length`, `column_key`, `column_comment` from information_schema.columns where `table_schema`=@schema";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>
            {
                new MySqlParameter
                {
                    ParameterName = "@schema",
                    DbType = DbType.String,
                    Value = Schema,
                }
            };
            var result = DBHelper.Instance.Execute(Connection, sql, mySqlParameters);
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
            string sql = $"SELECT * FROM `{resource}` WHERE `id` = @id";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>
            {
                new MySqlParameter
                {
                    ParameterName = "@id",
                    DbType = DbType.Int32,
                    Value = id,
                }
            };
            var result = DBHelper.Instance.Execute(Connection,sql,mySqlParameters);
            return result.Count > 0 ? result.First() : null;
        }

        /// <summary>
        /// 根据过滤器查询结果集 字典列表
        /// </summary>
        /// <param name="resource">资源名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns>结果集 字典列表</returns>
        public PageQueryResult Query(string resource, PageQueryFilter filter)
        {
            string field = GetFieldString(filter);
            var whereTuple = GetWhereString(resource, filter);
            string order = GetOrderString(filter);
            string page = GetPageString(filter);
            //TODO 分页查询获取总条数的优化方法，但是后面的数量怎么获取需要等实际测试看看
            string sql = string.Format("select SQL_CALC_FOUND_ROWS {0} from `{1}` {2} {3} {4};" +
                "SELECT FOUND_ROWS() as total;", field, resource, whereTuple.Item1, order, page);
            var result = DBHelper.Instance.Execute(Connection, sql, whereTuple.Item2);

            //TODO 总条数
            int total = 0;
            Connection.Close();
            return new PageQueryResult(result, total);
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 获取where语句
        /// </summary>
        /// <param name="filter">分页查询过滤器</param>
        /// <returns>where语句</returns>
        private Tuple<string, List<MySqlParameter>> GetWhereString(string resource, PageQueryFilter filter)
        {
            //DONE 拼接where语句
            string where = string.Empty;
            List<MySqlParameter> list = new();
            if (filter == null || filter.Filter == null || filter.Filter.Filters.Length <= 0) return Tuple.Create(where, list);

            StringBuilder builder = new StringBuilder();
            var baseFilter = filter.Filter;
            foreach(var item in baseFilter.Filters)
            {
                string str = string.Empty;
                if (item is QueryFilter)
                {
                    var tuple = GetWhereString(resource, item as QueryFilter);
                    str = tuple.Item1;
                    list = list.Concat(tuple.Item2).ToList();
                }
                if(item is MiniQueryFilter)
                {
                    var tuple = GetWhereString(resource, item as MiniQueryFilter);
                    str = tuple.Item1;
                    list = list.Concat(tuple.Item2).ToList();
                }

                if (item != baseFilter.Filters.First() && !string.IsNullOrEmpty(str))
                {
                    builder.Append(" " + baseFilter.SqlLogicOperator.ToString() + " ");
                }

                builder.Append(str);
            }
            if (builder.Length > 0)
            {
                where = string.Format("where {0}", builder.ToString());
            }
            return Tuple.Create(where, list);
        }

        /// <summary>
        /// 获取where语句
        /// </summary>
        /// <param name="filter">标准查询过滤器</param>
        /// <returns>where语句</returns>
        private Tuple<string, List<MySqlParameter>> GetWhereString(string resource, QueryFilter filter)
        {
            string where = string.Empty;
            List<MySqlParameter> list = new();
            StringBuilder builder = new();
            if (filter.Filters.Length <= 0) return Tuple.Create(where, list);
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
                    var tuple = GetWhereString(resource, item as QueryFilter);
                    str = tuple.Item1;
                    list = list.Concat(tuple.Item2).ToList();
                }
                if (item is MiniQueryFilter)
                {
                    var tuple = GetWhereString(resource, item as MiniQueryFilter);
                    str = tuple.Item1;
                    list = list.Concat(tuple.Item2).ToList();
                }
                builder.Append(str);
            }
            builder.Append(')');
            where = builder.ToString();
            return Tuple.Create(where, list);
        }

        /// <summary>
        /// 获取where语句
        /// </summary>
        /// <param name="filter">最小查询过滤器</param>
        /// <returns>where语句</returns>
        private Tuple<string, List<MySqlParameter>> GetWhereString(string resource, MiniQueryFilter filter)
        {
            //TODO 拼接最小化where语句
            string where = string.Empty;
            List<MySqlParameter> list = new();
            //用于区分同名字段
            int sign = Environment.TickCount;
            var fieldCode = "@" + filter.FieldName + sign;
            bool isValue = false;
            object value = null;
            switch (filter.Sign)
            {
                case SqlOperator.IsNuLL:
                    where = string.Format("`{0}` is null", filter.FieldName);
                    break;
                case SqlOperator.IsNotNuLL:
                    where = string.Format("`{0}` is not null", filter.FieldName);
                    break;
                case SqlOperator.Equal:
                    where = string.Format("`{0}` = {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = filter.Value;
                    break;
                case SqlOperator.NoEqual:
                    where = string.Format("`{0}` <> {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = filter.Value;
                    break;
                case SqlOperator.Like:
                    where = string.Format("`{0}` like {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = "'%"+filter.Value.ToString()+ "%'";
                    break;
                case SqlOperator.LeftLike:
                    where = string.Format("`{0}` like {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = "'" + filter.Value.ToString() + "%'";
                    break;
                case SqlOperator.RightLike:
                    where = string.Format("`{0}` like {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = "'%" + filter.Value.ToString() + "'";
                    break;
                case SqlOperator.NotLike:
                    where = string.Format("`{0}` not like {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = "'%" + filter.Value.ToString() + "%'";
                    break;
                case SqlOperator.LessThan:
                    where = string.Format("`{0}` < {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = filter.Value;
                    break;
                case SqlOperator.LessEqualThan:
                    where = string.Format("`{0}` <= {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = filter.Value;
                    break;
                case SqlOperator.MoreThan:
                    where = string.Format("`{0}` > {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = filter.Value;
                    break;
                case SqlOperator.MoreEqualThan:
                    where = string.Format("`{0}` >= {1}", filter.FieldName, fieldCode);
                    isValue = true;
                    value = filter.Value;
                    break;
                case SqlOperator.In:
                    {
                        where = string.Format("`{0}` in ({1})", filter.FieldName, fieldCode);
                        isValue = true;
                        var arr = filter.Value.ToString().Split(',');
                        string inStr = string.Empty;
                        foreach (string item in arr)
                        {
                            inStr += "'" + item + "',";
                        }
                        value = inStr[..^1];
                        break;
                    }
                case SqlOperator.NotIn:
                    {
                        where = string.Format("`{0}` not in ({1})", filter.FieldName, fieldCode);
                        isValue = true;
                        var arr = filter.Value.ToString().Split(',');
                        string inStr = string.Empty;
                        foreach (string item in arr)
                        {
                            inStr += "'" + item + "',";
                        }
                        value = inStr[..^1];
                        break;
                    }
                default: break;
            }

            if (isValue)
            {
                var field = (from fieldInfo in FieldInfos[resource] where fieldInfo.Name.Equals(filter.FieldName) select fieldInfo).First();
                list.Add(new MySqlParameter
                {
                    ParameterName = fieldCode,
                    DbType = field.DbType,
                    Value = value
                });
            }

            return Tuple.Create(where, list);
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
