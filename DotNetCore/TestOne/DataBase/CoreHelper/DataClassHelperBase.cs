using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataBase
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

        #region 变量
        internal string m_CurrConnectionString;
		public string CurrConnectionString => m_CurrConnectionString;

		internal SqlPrividerType m_CurrPrividerType = SqlPrividerType.SqlClient;
		public SqlPrividerType CurrPrividerType => m_CurrPrividerType;

		internal Dictionary<string, string> m_DataBaseKyFieldTableDic;
		public Dictionary<string, string> DataBaseKyFieldTableDic => m_DataBaseKyFieldTableDic;

		internal Dictionary<string, string> m_TableToTableNameDic;
		public Dictionary<string, string> TableToTableNameDic => m_TableToTableNameDic;

		internal string m_ModelNameSpace;
		public string ModelNameSpace => m_ModelNameSpace;

		internal Dictionary<string, List<Column>> m_TableToTableNameFields;
		public Dictionary<string, List<Column>> TableToTableNameFields => m_TableToTableNameFields;

		internal Dictionary<string, Dictionary<string, string>> m_DicTableToTableNameFields;
		public Dictionary<string, Dictionary<string, string>> DicTableToTableNameFields => m_DicTableToTableNameFields;

		internal Dictionary<string, string> m_TableTypeDic;
		public Dictionary<string, string> TableTypeDic => m_TableTypeDic;
		#endregion

		#region 虚拟方法
		internal virtual void GetSqlTableInfo()
        {

        }
        internal virtual List<string> GetTableNamesFromService()
        {
            return null;
        }
        internal virtual List<Column> GetColumnsFromTableName(string tableName, System.Data.Common.DbConnection conn = null)
        {
            return null;
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
                    if (item is AndOrQueryFilter)
                        str = GetSqlSign(item as AndOrQueryFilter);
                    builder.Append(str);
                    index++;
                }
            }
            if (index == 0)
                return "1=1";
            return builder.ToString();
        }
        /// <summary>
        /// 得到order by语句
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
                    if (filters.OrderByType == SQLOrderBy.Desc)
                        builder.Append(" desc ");
                    else
                        builder.Append(" asc ");
                    if (i != filters.OrderFieldNames.Length - 1)
                        builder.Append(",");
                }
                orderBy = " order by " + builder.ToString();
            }
            return orderBy;
        }
        /// <summary>
        /// 得到group by语句
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual string GetGroupByStrings(QueryPageFilter filters)
        {
            string fields = string.Empty;
            if (filters != null && filters.GroupByFieldNames != null && filters.GroupByFieldNames.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < filters.GroupByFieldNames.Length; i++)
                {
                    builder.Append(filters.GroupByFieldNames[i]);
                    if (i != filters.GroupByFieldNames.Length - 1)
                        builder.Append(",");
                }
                fields = builder.ToString();
            }
            var groupBy = string.Empty;
            if (!string.IsNullOrEmpty(fields))
                groupBy = "group by " + fields;
            return groupBy;
        }
        /// <summary>
        /// 得到查询字段
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        internal virtual string GetQueryFields(QueryPageFilter filters)
        {
            if (filters.ReturnFieldNames == null || filters.ReturnFieldNames.Length == 0)
                return GetFieldFromTable(filters.TableName);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < filters.ReturnFieldNames.Length; i++)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                else
                    builder.Append(filters.ReturnFieldNames[i]);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 得到条件语句
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
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
                if (item is AndOrQueryFilter)
                    str = GetSqlSign(item as AndOrQueryFilter);
                builder.Append(str);
                index++;
            }
            builder.Append(" ) ");
            return builder.ToString();
        }
        /// <summary>
        /// 得到SQL里的单个条件
        /// </summary>
        /// <param name="filter">过滤器</param>
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
        /// 从表DIC里获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        internal virtual string GetFieldFromTable(String tableName)
        {
            StringBuilder builder = new StringBuilder();
            if (m_TableToTableNameFields.ContainsKey(tableName))
            {
                var cols = m_TableToTableNameFields[tableName];
                foreach (var col in cols)
                {
                    if (builder.Length > 0)
                        builder.Append(",");
                    var colTypeName = col.TypeName.ToLower();
                    builder.Append(col.ColumnName);
                }
            }
            if (builder.Length > 0)
                return builder.ToString();
            return " * ";
        }
        internal virtual FilterQueryResult GetQueryResultFromDB(QueryPageFilter filters)
        {
            FilterQueryResult result = new FilterQueryResult();
            string whereStr = GetWhereString(filters);
            string orderBy = GetOrderByString(filters);
            string groupBy = GetGroupByStrings(filters);
            var fields = GetQueryFields(filters);
            string sql = "select count(*) from " + filters.TableName + " where " + whereStr;
            string strSql = string.Empty;

            var keyField = m_DataBaseKyFieldTableDic[filters.TableName];
            var allFieldsArr = m_DicTableToTableNameFields[filters.TableName];
            var queryFieldsArr = fields.Replace(" ","").Split(',').ToArray();
            if (string.IsNullOrEmpty(keyField) && fields.Replace(" ", "") == "*" && allFieldsArr.ContainsKey("ID"))
                keyField = "ID";
            if (string.IsNullOrEmpty(keyField) && fields.Replace(" ", "") == "*" && !allFieldsArr.ContainsKey("ID"))
                keyField = queryFieldsArr.First();
            if (string.IsNullOrEmpty(keyField) && fields.Replace(" ", "") != "*" && queryFieldsArr.Contains("ID"))
                keyField = "ID";
            if (string.IsNullOrEmpty(keyField) && fields.Replace(" ", "") != "*" && !queryFieldsArr.Contains("ID"))
                keyField = queryFieldsArr.First();

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = " order by " + keyField + " ASC ";
            }
            else
                orderBy = orderBy + "," + keyField + " ASC ";

            if (filters.IsPage == true)
            {
                var index = filters.PageSize * (filters.PageIndex - 1);
                if (index < 0)
                    index = 0;

                var pageSize = filters.PageSize;
                if (pageSize == 0)
                    pageSize = 1;

                strSql = string.Format("select {0} from {1} where {2} {3} offset {4} rows fetch next {5} rows only", fields, filters.TableName, whereStr, orderBy, index, pageSize);
                if (CurrPrividerType == SqlPrividerType.Sqlite)
                {
                    strSql = "select " + fields + " from " + filters.TableName +
                           " where " + keyField + " Not In ( select " + keyField + " from " + filters.TableName + " where " + whereStr + orderBy + " limit 0," + index + " ) and  ( " + whereStr + " ) " + orderBy + " limit 0," + filters.PageSize;
                }
            }
            else
            {
                strSql = "select  " + fields + " from " + filters.TableName + " where " + whereStr + groupBy + orderBy;
            }
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            
            if (filters.IsReturnCount == false)
            {
                var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
                result.Result = table;
                result.Result.TableName = filters.TableName;
                result.TotalCount = table.Rows.Count;
            }
            else
            {
                var tableCount = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
                int count = int.Parse(tableCount.Rows[0][0].ToString());
                result.TotalCount = count;
            }
            conn.Close();
            return result;
        }
        internal virtual string GetSqlFromTableInfo(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            fieldList.PrividerType = CurrPrividerType;
            foreach (var item in sql.Fields)
            {
                if (item.Key == "OBJECTID")
                    continue;// “OBJECTID”是自增树列，会出现 SQL 语句无法更新标识列 2019/07/29
                bool IsPkField = false;
                if (!string.IsNullOrEmpty(sql.KeyFieldName))
                    IsPkField = item.Key.ToLower() == sql.KeyFieldName.ToLower();
                if (IsPkField)
                    fieldList.PrimaryField = new SqlField(item.Key, item.Value, CurrPrividerType);
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
        internal virtual void ExecuteSqlList(StringBuilder sqlList)
        {
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(sqlList.ToString(), conn);
        }
        internal virtual SqlTableInfo[] GetSqlList(string tableName, Dictionary<string, object>[] models, Dictionary<string, string> DataBaseKyFieldTableDic, bool ispart = false)
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
        internal virtual void ExecuteDelSqlList(string tableName, object[] ids)
        {
            string tableName1 = tableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(tableName))
                tableName1 = TableToTableNameDic[tableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("缺少表或视图：" + tableName1 + "！请联系开发人员！"); ;
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
        internal virtual string GetSqlFromDelTableInfo(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            foreach (var item in sql.Fields)
            {
                bool IsPkField = false;
                if (!string.IsNullOrEmpty(sql.KeyFieldName))
                    IsPkField = item.Key == sql.KeyFieldName;

                if (IsPkField)
                    fieldList.PrimaryField = new SqlField(item.Key, item.Value, CurrPrividerType);
                fieldList.AddNonPrimaryField(item.Key, item.Value);
                continue;
            }
            return fieldList.GetDeleteString();
        }
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
        #endregion

        #region 外部方法

        public Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("缺少表或视图：" + tableName1 + "！请联系开发人员！");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    var list = ConvertDataTable(result.Result);
                    return list.ToArray();
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
        public void DeleteObjects(string tableName, string[] Ids)
        {
            ExecuteDelSqlList(tableName, Ids);
        }
        public void UpdateObjects(Dictionary<string, object> objs, QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("缺少表或视图：" + tableName1 + "！请联系开发人员！");
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
                    field = new SqlField(item.Key, date, CurrPrividerType);
                }
                else
                    field = new SqlField(item.Key, item.Value, CurrPrividerType);
                if (fields.Length > 0)
                    fields.Append(",");
                fields.Append(field.GetKeyEqualsValueString());
            }

            var updateSql = string.Format("update {0} set {1} where {2} ", tableName1, fields.ToString(), whereStr);
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(updateSql, conn);
            conn.Close();
        }
        public void DeleteObjects(QueryPageFilter filters)
        {
            string tableName1 = filters.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filters.TableName))
                tableName1 = TableToTableNameDic[filters.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("缺少表或视图：" + tableName1 + "！请联系开发人员！");
            filters.TableName = tableName1;
            string whereStr = GetWhereString(filters);
            string strSql = string.Empty;
            strSql = "delete from " + filters.TableName + " where " + whereStr;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(strSql, conn);
            conn.Close();
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
                foreach (var obj in dicList)
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
        }

        public List<Dictionary<string, object>> ConvertDataTable(DataTable table, bool isStringNullToEmpty = false)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn item in row.Table.Columns)
                {
                    if (isStringNullToEmpty == true && item.DataType == typeof(string))
                        dic.Add(item.ColumnName, row[item.ColumnName] == DBNull.Value ? "" : row[item.ColumnName].ToString());
                    else
                        dic.Add(item.ColumnName, row[item.ColumnName] == DBNull.Value ? null : row[item.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }
        #endregion

    }
}
