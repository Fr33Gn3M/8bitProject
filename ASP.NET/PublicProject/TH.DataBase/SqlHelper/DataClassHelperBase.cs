using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TH.DataBase
{
    public class DataClassHelperBase : IDataClassHelper
    {
        public DataClassHelperBase(string connstr, string modelNameSpace, SqlPrividerType prividerType)
        {
            m_CurrPrividerType = prividerType;
            m_CurrConnectionString = connstr;
            m_ModelNameSpace = modelNameSpace;
            GetSqlTableInfo();
        }

        public DataClassHelperBase(string connstr, string modelNameSpace, SqlPrividerType prividerType, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
        {
            m_CurrPrividerType = prividerType;
            m_CurrConnectionString = connstr;
            m_DataBaseKyFieldTableDic = dataBaseKyFieldTableDic;
            m_TableToTableNameDic = tableToTableNameDic;
            m_ModelNameSpace = modelNameSpace;
        }

        #region 虚拟方法
        /// <summary>
        /// 得到SQL语句，order by的语句
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual string GetOrderByString(QueryPageFilter filters)
        {
            string orderBy = string.Empty;
            if (filters != null && filters.OrderFieldNames != null && filters.OrderFieldNames.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < filters.OrderFieldNames.Length; i++)
                {
                    builder.Append(filters.OrderFieldNames[i]);
                    if (i != filters.OrderFieldNames.Length - 1)
                        builder.Append(",");
                }
                if (filters.OrderByType == SQLOrderBy.Desc)
                    orderBy = " order by " + builder.ToString() + " desc ";
                else
                    orderBy = " order by " + builder.ToString() + " asc ";
            }
            return orderBy;
        }

        /// <summary>
        /// 得到SQL语句，group by的语句
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual string GetGroupByFields(QueryPageFilter filters)
        {
            string groupBy = string.Empty;
            if (filters != null && filters.GroupByFieldNames != null && filters.GroupByFieldNames.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < filters.GroupByFieldNames.Length; i++)
                {
                    builder.Append(filters.GroupByFieldNames[i]);
                    if (i != filters.GroupByFieldNames.Length - 1)
                        builder.Append(",");
                }
                groupBy = builder.ToString();
            }
            return groupBy;
        }

        internal virtual void ExecuteDelSqlList(string tableName, object[] ids)
        {
            string tableName1 = tableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(tableName))
                tableName1 = TableToTableNameDic[tableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！"); ;
            StringBuilder builder = new StringBuilder();
            foreach (var item in ids)
            {
                var sqlInfo = new SqlTableInfo();
                sqlInfo.TableName = tableName1;
                var keyFieldName = DataBaseKyFieldTableDic[tableName1];
                sqlInfo.KeyFieldName = keyFieldName;
                sqlInfo.Fields = new Dictionary<string, object>();
                sqlInfo.Fields.Add(keyFieldName, item);
                var sql = GetSqlFromDelTableInfo(sqlInfo);
                builder.AppendLine(sql);
            }
            if (builder.Length > 0)
                ExecuteSqlList(builder);
        }
        /// <summary>
        /// 查询SQL语句
        /// </summary>
        /// <param name="sqlList"></param>
        internal virtual void ExecuteSqlList(StringBuilder sqlList)
        {
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(sqlList.ToString(), conn);
            conn.Close();
        }

        /// <summary>
        /// 得能删除语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal virtual string GetSqlFromDelTableInfo(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            foreach (var item in sql.Fields)
            {
                bool IsPkField = false;
                if (!string.IsNullOrEmpty(sql.KeyFieldName))
                    IsPkField = item.Key == sql.KeyFieldName;

                if (IsPkField)
                    fieldList.PrimaryField = new SqlField(item.Key, item.Value);
                fieldList.AddNonPrimaryField(item.Key, item.Value);
                continue;
            }
            return fieldList.GetDeleteString();
        }
        /// <summary>
        /// 查询反回列表
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual DataTable GetQueryTableResult(QueryPageFilter filters)
        {
            string whereStr = GetWhereString(filters);
            string orderBy = GetOrderByString(filters);
            var fields = GetQueryFields(filters);
            string strSql = string.Empty;
            if (filters.IsPage == true)
            {
                var index = filters.PageSize + (filters.PageIndex - 1);
                strSql = "select top " + filters.PageSize + fields + "  from " + filters.TableName +
                   " where Id Not In ( select top " + index + " Id from " + filters.TableName + " where " + whereStr + orderBy + ")";
            }
            else
            {
                strSql = "select " + fields + " from " + filters.TableName + " where " + whereStr;
            }
            strSql = strSql + orderBy;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
            conn.Close();
            return table;
        }

        internal virtual string GetQueryFields(QueryPageFilter filters)
        {
            if (filters.ReturnFieldNames == null || filters.ReturnFieldNames.Length == 0)
                return " * ";
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < filters.ReturnFieldNames.Length; i++)
            {
                if (i != 0)
                    str.Append(",");
                str.Append(filters.ReturnFieldNames[i]);
            }
            return str.ToString();
        }


        /// <summary>
        /// 查询反回结果
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual FilterQueryResult GetQueryResultFromDB(QueryPageFilter filters)
        {
            FilterQueryResult result = new FilterQueryResult();
            string whereStr = GetWhereString(filters);
            string orderBy = GetOrderByString(filters);
            string groupByFields = GetGroupByFields(filters);
            var fields = GetQueryFields(filters);
            string sql = "select count(*) from " + filters.TableName + " where " + whereStr;
            string strSql = string.Empty;
            if (filters.IsPage == true)
            {
                var index = filters.PageSize * (filters.PageIndex - 1);
                if (index < 0)
                    index = 0;

                strSql = "select top " + filters.PageSize + fields + " from " + filters.TableName +
                   " where Id Not In ( select top " + index + " Id from " + filters.TableName + " where " + whereStr + orderBy + ") and  ( " + whereStr + " ) " + orderBy;
                if (CurrPrividerType == SqlPrividerType.Sqlite)
                {
                    strSql = "select " + fields + " from " + filters.TableName +
                           " where Id Not In ( select Id from " + filters.TableName + " where " + whereStr + orderBy + " limit 0," + index + " ) and  ( " + whereStr + " ) " + orderBy + " limit 0," + filters.PageSize;
                }
            }
            else
            {
                var groupBy = string.Empty;
                if (!string.IsNullOrEmpty(groupByFields))
                {
                    fields = groupByFields;
                    groupBy = "group by " + groupByFields;
                }

                strSql = "select  " + fields + " from " + filters.TableName + " where " + whereStr + groupBy + orderBy;
            }
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var tableCount = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
            int count = int.Parse(tableCount.Rows[0][0].ToString());
            if (filters.IsReturnCount == false)
            {
                var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
                result.Result = table;
            }
            result.TotalCount = count;
            conn.Close();
            return result;
        }

        /// <summary>
        ///  得到where语句
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual string GetWhereString(QueryPageFilter filters)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            if (filters.Filters != null && filters.Filters.Length > 0)
            {
                foreach (var item in filters.Filters)
                {
                    if (index != 0)
                        builder.Append(" " + filters.FilterAndOrType.ToString() + " ");
                    string str = string.Empty;
                    if (item is QueryFilter)
                        str = GetSqlSign(item as QueryFilter);
                    if (item is SpatialQueryFilter)
                        str = GetSqlSign(item as SpatialQueryFilter);
                    if (item is AndOrQueryFilter)
                        str = GetSqlSign(item as AndOrQueryFilter);
                    builder.Append(str);
                    index++;
                }
                //foreach (var item in filters.Filters)
                //{
                //    builder.Append(GetSqlSign(item));
                //    index++;
                //    if (index != filters.Filters.Length)
                //        builder.Append(" " + filters.FilterAndOrType.ToString() + " ");
                //}
            }
            //if (filters.SpatialFilters != null&& filters.SpatialFilters.Length > 0)
            //{
            //    if (index > 0)
            //    {
            //        index = 0;
            //        builder.Append(" " + filters.FilterAndOrType.ToString() + " ");
            //    }
            //    foreach (var item in filters.SpatialFilters)
            //    {
            //        builder.Append(GetSqlSign(item));
            //        index++;
            //        if (index != filters.SpatialFilters.Length)
            //            builder.Append(" " + filters.FilterAndOrType.ToString() + " ");
            //    }
            //}
            if (index == 0)
                return "1=1";
            return builder.ToString();
        }

        internal virtual string GetSqlSign(AndOrQueryFilter filter)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            builder.Append(" ( ");
            foreach (var item in filter.Filters)
            {
                if (index != 0)
                    builder.Append(" " + filter.FilterAndOrType.ToString() + " ");
                string str = string.Empty;
                if (item is QueryFilter)
                    str = GetSqlSign(item as QueryFilter);
                if (item is SpatialQueryFilter)
                    str = GetSqlSign(item as SpatialQueryFilter);
                if (item is AndOrQueryFilter)
                    str = GetSqlSign(item as AndOrQueryFilter);
                builder.Append(str);
                index++;
            }
            builder.Append(" ) ");
            return builder.ToString();
        }

        internal virtual string GetSqlSign(SpatialQueryFilter filter)
        {
            var str = string.Format("{0}.MakeValid().{1}(geometry::STGeomFromText('{2}',{3}))={4}", filter.FieldName, filter.Sign, filter.Geometry.WKT, filter.Geometry.SRID, filter.IsTrue == true ? 1 : 0);
            return str;
        }
        /// <summary>
        /// 得到值
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        internal virtual string GetFieldValue(object fieldValue)
        {
            if (QueryPageFilter.IsSqlFilter(fieldValue) == true)
                throw new Exception("数据中含SQL注入，请误使用！");
            if (fieldValue is string)
            {
                string str = fieldValue.ToString();
                if (str.IndexOf("'") > -1)
                {
                    str = str.Replace("'", "''");
                }
                return string.Format("'{0}'", str);
            }
            if (fieldValue is DBNull || fieldValue is DateTime || fieldValue is Guid)
            {
                return string.Format("'{0}'", fieldValue.ToString());
            }
            if (fieldValue is Boolean)
            {
                bool t = (Boolean)fieldValue;
                return (t ? 1.ToString() : 0.ToString());
            }
            else
            {
                if (fieldValue == null)
                    return null;
                return fieldValue.ToString();
            }
        }
        /// <summary>
        /// 得到SQL里有条件值
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal virtual string GetSqlSign(QueryFilter filter)
        {
            var fieldValue = GetFieldValue(filter.Value);
            string str = string.Empty;
            switch (filter.Sign)
            {
                case SQLSign.IsNuLL:
                    str = string.Format(" {0}  {1} ", filter.FieldName, " is null ");
                    break;
                case SQLSign.IsNotNuLL:
                    str = string.Format(" {0}  {1} ", filter.FieldName, " is not null ");
                    break;
                case SQLSign.Equal:
                    str = string.Format(" {0} = {1} ", filter.FieldName, fieldValue);
                    break;
                case SQLSign.LessEqualThan:
                    str = string.Format(" {0} <= {1} ", filter.FieldName, fieldValue);
                    break;
                case SQLSign.LessThan:
                    str = string.Format(" {0} < {1} ", filter.FieldName, fieldValue);
                    break;
                case SQLSign.Like:
                    str = filter.FieldName + " like '%" + filter.Value + "%' ";
                    break;
                case SQLSign.MoreEqualThan:
                    str = string.Format(" {0} >= {1} ", filter.FieldName, fieldValue);
                    break;
                case SQLSign.MoreThan:
                    str = string.Format(" {0} > {1} ", filter.FieldName, fieldValue);
                    break;
                case SQLSign.NoEqual:
                    str = string.Format(" {0} <> {1} ", filter.FieldName, fieldValue);
                    break;
                case SQLSign.NotIn://需不需要引号自己添加
                    {
                        var arr = filter.Value.ToString().Split(';');
                        string sql = string.Empty;
                        int index = 0;

                        foreach (var item in arr)
                        {
                            // sql += ("'" + item + "'");
                            sql += (item);
                            index++;
                            if (index != arr.Length)
                                sql += ",";
                        }

                        str = filter.FieldName + " Not In( " + sql + ") ";
                        break;
                    }
                case SQLSign.In:
                    {
                        var arr = filter.Value.ToString().Split(';');
                        string sql = string.Empty;
                        int index = 0;
                        foreach (var item in arr)
                        {
                            // sql += ("'" + item + "'");
                            sql += (item);
                            index++;
                            if (index != arr.Length)
                                sql += ",";
                        }
                        str = filter.FieldName + " In( " + sql + ") ";
                        break;
                    }
                default:
                    break;
            }
            return str;
        }
        /// <summary>
        /// 传入模型更新数据库
        /// </summary>
        /// <param name="sqlList"></param>
        /// <param name="isconvertPart"></param>
        internal virtual void ExecuteSqlFromModelList(object[] sqlList, bool isnotconvertPart = false)
        {
            var sqlTableInfoList = ModelTypeConvert<object>.GetSqlList(sqlList, DataBaseKyFieldTableDic, isnotconvertPart);
            StringBuilder builder = new StringBuilder();
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo(item);
                builder.AppendLine(sql);
            }
            if (builder.Length > 0)
                ExecuteSqlList(builder);
        }

        internal virtual SqlTableInfo GetSqlTableInfo(DataRow model)
        {
            string tableName = null;
            var dic = ConvertDict(model, ref tableName);
            var sql = new SqlTableInfo();
            sql.TableName = tableName;
            if (DataBaseKyFieldTableDic.ContainsKey(tableName))
                sql.KeyFieldName = DataBaseKyFieldTableDic[tableName];
            sql.Fields = dic;
            return sql;
        }

        Dictionary<string, object> ConvertDict(DataRow dr, ref string tableName)
        {
            DataTable dt = dr.Table;
            tableName = dt.TableName;
            var dic = new Dictionary<string, object>();
            foreach (DataColumn item in dt.Columns)
            {
                var tempName = item.ColumnName;
                if (dr[tempName] != null && dr[tempName] != DBNull.Value && dr[tempName].ToString().Length != 0)
                    dic.Add(tempName, dr[tempName]);
            }
            return dic;
        }

        /// <summary>
        /// 得到查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal virtual string GetSqlFromTableInfo(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            fieldList.PrividerType = CurrPrividerType;
            foreach (var item in sql.Fields)
            {
                bool IsPkField = false;
                if (!string.IsNullOrEmpty(sql.KeyFieldName))
                    IsPkField = item.Key.ToLower() == sql.KeyFieldName.ToLower();
                if (IsPkField)
                    fieldList.PrimaryField = new SqlField(item.Key, item.Value);
                if (CurrPrividerType == SqlPrividerType.Sqlite)
                {
                    if (item.Value is DateTime)
                    {
                        string str = ((DateTime)item.Value).ToString("s");
                        fieldList.AddNonPrimaryField(item.Key, str);
                    }
                    else
                    {
                        fieldList.AddNonPrimaryField(item.Key, item.Value);
                    }
                }
                else
                {
                    if (!DicTableToTableNameFields[sql.TableName].ContainsKey(item.Key))
                        continue;
                    var typeName = DicTableToTableNameFields[sql.TableName][item.Key];
                    if (typeName == "datetime" && item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        var date = DateTime.Parse(item.Value.ToString());
                        fieldList.AddNonPrimaryField(item.Key, date);
                    }
                    else
                        fieldList.AddNonPrimaryField(item.Key, item.Value);
                }
                continue;
            }
            return SqlFieldList.GetSqlString(fieldList);
        }

        public List<string> GetDelSqlList(string tableName, object[] ids)
        {
            string tableName1 = tableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(tableName))
                tableName1 = TableToTableNameDic[tableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！"); ;
            var list = new List<string>();
            foreach (var item in ids)
            {
                var sqlInfo = new SqlTableInfo();
                sqlInfo.TableName = tableName1;
                var keyFieldName = DataBaseKyFieldTableDic[tableName1];
                sqlInfo.KeyFieldName = keyFieldName;
                sqlInfo.Fields = new Dictionary<string, object>();
                sqlInfo.Fields.Add(keyFieldName, item);
                var sql = GetSqlFromDelTableInfo(sqlInfo);
                list.Add(sql + ";");
            }
            return list;
        }


        internal virtual void GetSqlTableInfo()
        {

        }
        #endregion

        internal string m_CurrConnectionString;
        public string CurrConnectionString
        {
            get { return m_CurrConnectionString; }
        }

        internal Dictionary<string, List<Column>> m_TableToTableNameFields;
        public Dictionary<string, List<Column>> TableToTableNameFields
        {
            get { return m_TableToTableNameFields; }
        }

        internal SqlPrividerType m_CurrPrividerType = SqlPrividerType.SqlClient;
        public SqlPrividerType CurrPrividerType
        {
            get { return m_CurrPrividerType; }
        }

        internal Dictionary<string, string> m_DataBaseKyFieldTableDic;
        public Dictionary<string, string> DataBaseKyFieldTableDic
        {
            get { return m_DataBaseKyFieldTableDic; }
        }

        internal Dictionary<string, string> m_TableToTableNameDic;
        public Dictionary<string, string> TableToTableNameDic
        {
            get { return m_TableToTableNameDic; }
        }

        internal Dictionary<string, Dictionary<string, string>> m_DicTableToTableNameFields;
        public Dictionary<string, Dictionary<string, string>> DicTableToTableNameFields
        {
            get { return m_DicTableToTableNameFields; }
        }

        public QueryFilterResult GetQueryResult(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            var queryResult = new QueryFilterResult();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    var list = ModelTypeConvert<object>.ConvertToModelFromType(ModelNameSpace, tableName1, result.Result, filter.IsConvertPart);
                    queryResult.Result = list.ToArray();
                }
            }
            return queryResult;
        }

        public void UpdateObjects(object[] sqlList, bool isconvertPart = false)
        {
            if (sqlList == null)
                throw new Exception("不能传空数据！");
            //var count = sqlList.Length;
            //var pageCount = 100;
            //var pageIndex = count % pageCount > 0 ? count / pageCount + 1 : count / pageCount;
            //for (int i = 0; i < pageIndex; i++)
            //{
            //    var currCount = i * pageCount;
            //    var arr = sqlList.Skip(currCount).Take(pageCount).ToArray();
            //    ExecuteSqlFromModelList(arr, isconvertPart);
            //}
            ExecuteSqlFromModelList(sqlList, isconvertPart);
        }

        public virtual void ExecuteSqlList(List<string> sqlList)
        {
            var sqls = sqlList.Where(m => string.IsNullOrEmpty(m) == false).ToList();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            DbTransaction trans = conn.BeginTransaction();
            System.Data.IDbCommand cmd = conn.CreateCommand();
            { // <-------------------
                try
                {
                    foreach (var item in sqls)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit(); // <-------------------
                    trans.Dispose();
                    cmd.Dispose();
                }
                catch
                {
                    trans.Rollback(); // <-------------------
                    trans.Dispose();
                    cmd.Dispose();
                    throw; // <-------------------
                }
            }
            conn.Close();
        }

        public void ExecuteSqlListSQL(List<string> sqlList)
        {
            var sqls = sqlList.Where(m => string.IsNullOrEmpty(m) == false).ToList();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            System.Data.IDbCommand cmd = conn.CreateCommand();
            { // <-------------------
                try
                {
                    foreach (var item in sqls)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    cmd.Dispose();
                }
                catch
                {
                    cmd.Dispose();
                    throw; // <-------------------
                }
            }
            conn.Close();
        }

        public void DeleteObjects<T>(object[] Ids)
        {
            var type = typeof(T);
            var tableName = type.Name;
            GetDelSqlList(tableName, Ids);
        }

        public void DeleteObjects(QueryPageFilter filters)
        {
            string tableName1 = filters.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filters.TableName))
                tableName1 = TableToTableNameDic[filters.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filters.TableName = tableName1;
            string whereStr = GetWhereString(filters);
            string strSql = string.Empty;
            strSql = "delete from " + filters.TableName + " where " + whereStr;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(strSql, conn);
            conn.Close();
        }

        public DataTable ExecuteSqlToDataTable(string sqlList)
        {
            DataTable table = null;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            try
            {
                table = DBClassHelper.ExecuteQueryToDataTable(sqlList.ToString(), conn);
            }
            catch
            {
                throw; // <-------------------
            }
            conn.Close();
            return table;
        }
        public Dictionary<string,object>[] ExecuteSqlToDic(string sqlList)
        {
            DataTable table = null;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            try
            {
                table = DBClassHelper.ExecuteQueryToDataTable(sqlList.ToString(), conn);
                if (table != null)
                {
                    var dic = new Dictionary<string, object>[table.Rows.Count];
                    int index = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        var dd = ConvertDataRow(row);
                        dic[index] = dd;
                        index++;
                    }
                    conn.Close();
                    return dic;
                }
            }
            catch
            {
                throw; // <-------------------
            }
            conn.Close();
            return null;
        }

        public List<string> GetSqlListFromDataRow(DataRow[] rows)
        {
            var sqlTableInfoList = new List<SqlTableInfo>();
            foreach (var item in rows)
            {
                var sqlTable = GetSqlTableInfo(item);
                sqlTableInfoList.Add(sqlTable);
            }
            var list = new List<string>();
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo(item);
                list.Add(sql);
            }
            return list;
        }

        public List<string> GetSqlListFromDataTable(DataTable dt)
        {
            var sqlTableInfoList = new List<SqlTableInfo>();
            foreach (DataRow item in dt.Rows)
            {
                var sqlTable = GetSqlTableInfo(item);
                sqlTableInfoList.Add(sqlTable);
            }
            var list = new List<string>();
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo(item);
                list.Add(sql);
            }
            return list;
        }


        internal string m_ModelNameSpace;
        public string ModelNameSpace
        {
            get { return m_ModelNameSpace; }
        }


        public void DeleteObjects(string tableName, object[] Ids)
        {
            ExecuteDelSqlList(tableName, Ids);
        }

        public Type GetArrayType(string tableName)
        {
            string tableName1 = tableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(tableName))
                tableName1 = TableToTableNameDic[tableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            var typename = ModelNameSpace + "." + tableName1 + "[]," + ModelNameSpace;
            var type = Type.GetType(typename);
            return type;
        }



        //List<string> GetTableNames();
        //List<Column> GetColumnsFromTable(string tableName);

        internal virtual List<string> GetTableNamesFromService()
        {
            return null;
        }
        internal virtual List<Column> GetColumnsFromTableName(string tableName)
        {
            return null;
        }

        public List<string> GetTableNames()
        {
            return GetTableNamesFromService();
        }

        public List<Column> GetColumnsFromTable(string tableName)
        {
            return GetColumnsFromTableName(tableName);
        }

        public List<string> GetSqlListFromModels(object[] sqlList, bool isconvertPart = false)
        {
            var sqlTableInfoList = ModelTypeConvert<object>.GetSqlList(sqlList, DataBaseKyFieldTableDic, isconvertPart);
            var list = new List<string>();
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo(item);
                list.Add(sql + ";");
            }
            return list;
        }

        public QueryFilterResult GetQueryResult(SqlModel filter, params object[] objs)
        {
            if (filter == null)
                throw new Exception("违法输入方式！请联系开发人员！");
            var queryResult = new QueryFilterResult();
            string str = string.Format(filter.Value, objs);
            var table = ExecuteSqlToDataTable(str);
            if (table != null)
            {
                queryResult.TotalCount = table.Rows.Count;
                var list = ModelTypeConvert<object>.ConvertToModelFromType(filter.NameSpaceName, filter.ModelName, table, false);
                queryResult.Result = list.ToArray();
            }
            return queryResult;
        }

        public QueryFilterResult[] GetQueryResults(QueryPageFilter[] filters)
        {
            var list = new List<QueryFilterResult>();
            if (filters == null || filters.Length == 0)
                return null;
            foreach (var item in filters)
            {
                var result = GetQueryResult(item);
                list.Add(result);
            }
            return list.ToArray();
        }

        private QueryFilterResultDic GetQueryFieldResult(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            var queryResult = new QueryFilterResultDic();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    var list = new List<Dictionary<string, object>>();
                    foreach (DataRow item in result.Result.Rows)
                    {
                        var dic = ConvertDataRow(item, true);
                        list.Add(dic);
                    }
                    queryResult.Result = list.ToArray();
                }
            }
            return queryResult;
        }


        public static Dictionary<string, object> ConvertDataRow(DataRow row, bool isConvert = true)
        {
            var dic = new Dictionary<string, object>();
            foreach (DataColumn item in row.Table.Columns)
            {
                if (item.DataType == typeof(SqlGeometry))
                {
                    var value = row[item.ColumnName];
                    var geom = value as SqlGeometry;
                    var thGeom = new THGeometry() { WKT = geom.ToString() };
                    dic.Add(item.ColumnName, thGeom);
                }
                else
                {
                    if (isConvert == true && item.DataType == typeof(string))
                        dic.Add(item.ColumnName, row[item.ColumnName] == DBNull.Value ? "" : row[item.ColumnName].ToString());
                    else
                        dic.Add(item.ColumnName, row[item.ColumnName] == DBNull.Value ? null : row[item.ColumnName]);
                }
            }
            return dic;
        }

        public QueryFilterResultDic[] GetQueryFieldResults(QueryPageFilter[] filters)
        {
            var list = new List<QueryFilterResultDic>();
            if (filters == null || filters.Length == 0)
                return null;
            foreach (var item in filters)
            {
                var result = GetQueryFieldResult(item);
                list.Add(result);
            }
            return list.ToArray();
        }

        //public static Dictionary<string, object> ConvertDataRow(DataRow row)
        //{
        //    var dic = new Dictionary<string, object>();
        //    foreach (DataColumn item in row.Table.Columns)
        //        dic.Add(item.ColumnName, row[item.ColumnName]);
        //    return dic;
        //}

        public Dictionary<string, object>[] GetQueryStatResult(SqlModel filter, params object[] objs)
        {
            if (filter == null)
                throw new Exception("违法输入方式！请联系开发人员！");
            var queryResult = new QueryFilterResult();
            string str = string.Format(filter.Value, objs);
            var table = ExecuteSqlToDataTable(str);
            if (table != null)
            {
                var dic = new Dictionary<string, object>[table.Rows.Count];
                int index = 0;
                foreach (DataRow row in table.Rows)
                {
                    var dd = ConvertDataRow(row);
                    dic[index] = dd;
                    index++;
                }
                return dic;
            }
            return null;
        }


        public void UpdateDelObjects(QueryPageFilter filter, string fieldName)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;

            string whereStr = GetWhereString(filter);
            string strSql = string.Empty;

            strSql = "update " + filter.TableName + " set " + fieldName + " = 1 " + " where " + whereStr;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(strSql, conn);
            conn.Close();
        }

        public Dictionary<string, object>[] GetQueryStatResultN(SqlModel filter, params object[] objs)
        {
            if (filter == null)
                throw new Exception("违法输入方式！请联系开发人员！");
            var queryResult = new QueryFilterResult();
            string str = string.Format(filter.Value, objs);
            var table = ExecuteSqlToDataTable(str);
            if (table != null)
            {
                var dic = new Dictionary<string, object>[table.Rows.Count];
                int index = 0;
                foreach (DataRow row in table.Rows)
                {
                    var dd = ConvertDataRow(row, false);
                    dic[index] = dd;
                    index++;
                }
                return dic;
            }
            return null;
        }


        public Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            var queryResult = new QueryFilterResult();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    var dic = new Dictionary<string, object>[result.Result.Rows.Count];
                    int index = 0;
                    foreach (DataRow row in result.Result.Rows)
                    {
                        var dd = ConvertDataRow(row, true);
                        dic[index] = dd;
                        index++;
                    }
                    return dic;
                }
            }
            return null;
        }


        public void UpdateObjects(string tableName, Dictionary<string, object>[] dicList, bool isconvertPart = false)
        {
            var sqlTableInfoList = GetSqlList(tableName, dicList, DataBaseKyFieldTableDic, isconvertPart);
            StringBuilder builder = new StringBuilder();
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo(item);
                builder.AppendLine(sql);
            }
            if (builder.Length > 0)
                ExecuteSqlList(builder);
        }

        //public static SqlTableInfo[] GetSqlList(string tableName, Dictionary<string, object>[] models, Dictionary<string, string> DataBaseKyFieldTableDic, bool isnotconvertPart = false)
        //{
        //    var list = new List<SqlTableInfo>();
        //    foreach (var item in models)
        //    {
        //        var sql = new SqlTableInfo();
        //        sql.TableName = tableName;
        //        //sql.BaseType = model.GetType().UnderlyingSystemType;
        //        if (DataBaseKyFieldTableDic.ContainsKey(tableName))
        //            sql.KeyFieldName = DataBaseKyFieldTableDic[tableName];
        //        sql.Fields = item;
        //        list.Add(sql);
        //    }
        //    return list.ToArray();
        //}

        public SqlTableInfo[] GetSqlList(string tableName, Dictionary<string, object>[] models, Dictionary<string, string> DataBaseKyFieldTableDic, bool ispart = false)
        {
            var list = new List<SqlTableInfo>();
            foreach (var item in models)
            {
                var sql = new SqlTableInfo();
                sql.TableName = tableName;
                if (DataBaseKyFieldTableDic.ContainsKey(tableName))
                    sql.KeyFieldName = DataBaseKyFieldTableDic[tableName];
                var diclist = item.Where(m => DicTableToTableNameFields[tableName].ContainsKey(m.Key)).ToDictionary(m => m.Key, n => n.Value);
                if (ispart == false)
                {
                    //var noExists = DicTableToTableNameFields[tableName].Where(m => diclist.ContainsKey(m.Key) == false).Select(m => m.Key).ToArray();
                    //foreach (var field in noExists)
                    //    diclist.Add(field, null);
                }
                sql.Fields = diclist;
                list.Add(sql);
            }
            return list.ToArray();
        }

        public Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter, ref int count)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            var queryResult = new QueryFilterResult();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            count = result.TotalCount;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    var dic = new Dictionary<string, object>[result.Result.Rows.Count];
                    int index = 0;
                    foreach (DataRow row in result.Result.Rows)
                    {
                        var dd = ConvertDataRow(row, false);
                        dic[index] = dd;
                        index++;
                    }
                    return dic;
                }
            }
            return null;
        }

        public void UpdateObjects(Dictionary<string, object> objs, QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            string whereStr = GetWhereString(filter);
            StringBuilder fields = new StringBuilder();
            foreach (var item in objs)
            {
                SqlField field;
                var typeName = DicTableToTableNameFields[filter.TableName][item.Key];
                if (typeName == "datetime" && item.Value != null)
                {
                    var date = DateTime.Parse(item.Value.ToString());
                    field = new SqlField(item.Key, date);
                }
                else
                    field = new SqlField(item.Key, item.Value);
                if (fields.Length > 0)
                    fields.Append(",");
                fields.Append(field.GetKeyEqualsValueString());
            }

            var updateSql = string.Format("update {0} set {1} where {2} ", tableName1, fields.ToString(), whereStr);
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(updateSql, conn);
            conn.Close();
        }


        public QueryFilterResultDic GetQueryResultDic(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            var queryResult = new QueryFilterResultDic();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    var dic = new Dictionary<string, object>[result.Result.Rows.Count];
                    int index = 0;
                    foreach (DataRow row in result.Result.Rows)
                    {
                        var dd = ConvertDataRow(row, false);
                        dic[index] = dd;
                        index++;
                    }
                    queryResult.Result = dic;
                }
            }
            return queryResult;
        }
        public void UpdateSystemTable(string TableName, List<Column> colomns)
        {
            var builder = new StringBuilder();
            var descBuilder = new StringBuilder();
            if (TableToTableNameDic.ContainsKey(TableName))
            {
                foreach (var item in colomns)
                {
                    string len = "";
                    if (item.Precision == 0) len = "";
                    else if (item.Precision == -1) len = "(MAX)";
                    else len = string.Format("({0})", item.Precision);

                    if (item.TypeName == "varchar" && string.IsNullOrEmpty(len)) len = "(50)"; 

                    string isNullStr = item.IsNull ? "null" : "not null";
                    string sql = string.Format(
                        @" if exists(select * from syscolumns where id=object_id('{0}') and name='{1}') 
                                    alter table {0} alter column  {1}  {2}{3} {4};  else   alter table {0}  add   {1}  {2}{3} {4} ", TableName, item.ColumnName, item.TypeName, len, isNullStr);
                    var str = string.Format("EXECUTE sp_updateextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{0}', N'column', N'{2}'", TableName, item.Label, item.ColumnName);
                    builder.AppendLine(sql);
                    descBuilder.AppendLine(str);
                }
            }
            else
            {
                var keyCol = colomns.Where(m => m.IsPrimaryKey == true).FirstOrDefault();
                if (keyCol == null)
                    throw new Exception("缺少主键！");
                builder.AppendLine(string.Format("CREATE TABLE {0} (", TableName));
                foreach (var item in colomns)
                {
                    string len = "";
                    if (item.Precision == 0) len = "";
                    else if (item.Precision == -1) len = "(MAX)";
                    else len = string.Format("({0})", item.Precision);

                    if (item.TypeName == "varchar" && string.IsNullOrEmpty(len)) len = "(50)"; 

                    builder.AppendLine(string.Format("{0}     {1}{2}  {3} null,", item.ColumnName, item.TypeName, len, (item.IsNull ? "" : " not ")));
                    var str = string.Format("EXECUTE sp_addextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{0}', N'column', N'{2}'", TableName, item.Label, item.ColumnName);
                    descBuilder.AppendLine(str);
                }
                builder.AppendLine(string.Format(" constraint PK_{0} primary key clustered ({1})); ", TableName, keyCol.ColumnName));
            }
            // builder.AppendLine(descBuilder.ToString());
            var strSql = builder.ToString();
            if (!string.IsNullOrEmpty(strSql))
            {
                var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
                int count = DBClassHelper.Execute(strSql, conn);
                conn.Close();
            }
        }

        public void BatchInsert(string TableName, Dictionary<string, object>[] dicList)
        {
            DataTable dt = GetTableSchema(dicList);
            var keyArr = dicList[0].Keys.ToArray();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            if (conn is SqlConnection)
            {
                var sqlconn = conn as SqlConnection;
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
                bulkCopy.DestinationTableName = TableName;
                bulkCopy.BatchSize = dicList.Length;
                foreach(var obj in dicList)
                {
                    DataRow dr = dt.NewRow();
                    foreach (var item in keyArr)
                    {
                        dr[item] = obj[item];
                    }
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(dt);
                }
            }
            conn.Close();
            //using (SqlConnection conn = new SqlConnection(StrConnMsg))
            //{
                
            //    bulkCopy.DestinationTableName = "Product";
            //    bulkCopy.BatchSize = dt.Rows.Count;
            //    conn.Open();
            //    sw.Start();

            //    for (int i = 0; i < totalRow; i++)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr[0] = Guid.NewGuid();
            //        dr[1] = string.Format("商品", i);
            //        dr[2] = (decimal)i;
            //        dt.Rows.Add(dr);
            //    }
            //    if (dt != null && dt.Rows.Count != 0)
            //    {
            //        bulkCopy.WriteToServer(dt);
            //    }
            //    Console.WriteLine(string.Format("插入{0}条记录共花费{1}毫秒，{2}分钟", totalRow, sw.ElapsedMilliseconds, GetMinute(sw.ElapsedMilliseconds)));
            //}
        }

        /// <summary>
        /// 查询SQL语句
        /// </summary>
        /// <param name="sqlList"></param>
        internal virtual DataTable GetTableSchema(Dictionary<string, object>[] dicList)
        {
            DataTable dt = new DataTable();
            var keyArr = dicList[0].Keys.ToArray();
            IList<DataColumn> dcList = new List<DataColumn>();
            foreach (var item in keyArr)
            {
                dcList.Add(new DataColumn(item, typeof(object)));
            }
            dt.Columns.AddRange(dcList.ToArray());
            return dt;
        }

        public Dictionary<string, object> GetQueryDic(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetOneQueryResultFromDB(filter);
            var queryResult = new QueryFilterResult();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null&&result.Result.Rows.Count>0)
                {
                    var dic = new Dictionary<string, object>();
                    if (result.Result.Rows.Count > 0)
                    {
                        DataRow row = result.Result.Rows[0];
                        var dd = ConvertDataRow(row, true);
                        dic = dd;
                    }
                    return dic;
                }
            }
            return null;
        }

        /// <summary>
        /// 查询返回唯一值
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual FilterQueryResult GetOneQueryResultFromDB(QueryPageFilter filters)
        {
            FilterQueryResult result = new FilterQueryResult();
            string whereStr = GetWhereString(filters);
            string orderBy = GetOrderByString(filters);
            var fields = GetQueryFields(filters);
            string strSql = string.Empty;
            strSql = "select top 1  " + fields + " from " + filters.TableName + " where " + whereStr + orderBy;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            if (filters.IsReturnCount == false)
            {
                var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
                result.Result = table;
            }
            result.TotalCount = 1;
            conn.Close();
            return result;
        }

        public T[] GetObjResult<T>(QueryPageFilter filter) where T : class, new()
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            IList<T> list = new List<T>();
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    list = ModelTypeConvert<T>.ConvertToModelFromType(ModelNameSpace, tableName1, result.Result, filter.IsConvertPart);
                }
            }
            return list.ToArray();
        }
    }
}
