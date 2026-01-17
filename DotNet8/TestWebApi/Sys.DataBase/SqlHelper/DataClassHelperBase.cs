using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using Sys.DataBase.Common;
using Sys.DataBase.Models;

namespace Sys.DataBase
{
    public class DataClassHelperBase : IDataClassHelper
    {
        public DataClassHelperBase(string connstr, string modelNameSpace, SqlPrividerType prividerType)
        {
            m_CurrPrividerType = prividerType;
            m_CurrConnectionString = connstr;
            m_ModelNameSpace = modelNameSpace;
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            GetSqlTableInfo();
        }

        public DataClassHelperBase(string connstr, string modelNameSpace, SqlPrividerType prividerType, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
        {
            m_CurrPrividerType = prividerType;
            m_CurrConnectionString = connstr;
            m_DataBaseKyFieldTableDic = dataBaseKyFieldTableDic;
            m_TableToTableNameDic = tableToTableNameDic;
            m_ModelNameSpace = modelNameSpace;
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
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
                    var fieldname = filters.OrderFieldNames[i];
                    if (QueryPageFilter.IsSqlFilter2(fieldname) == true)
                        throw new Exception("数据中含SQL注入，请误使用！");
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
        /// 得到SQL语句，group by的语句
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
                    var fieldname = filters.GroupByFieldNames[i];
                    if (QueryPageFilter.IsSqlFilter2(fieldname) == true)
                        throw new Exception("数据中含SQL注入，请误使用！");
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

        internal virtual void ExecuteDelSqlList(string tableName, object[] ids)
        {
            string tableName1 = tableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(tableName))
                tableName1 = TableToTableNameDic[tableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName + ")"); ;
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
                    fieldList.PrimaryField = new SqlField(item.Key, item.Value, CurrPrividerType);
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
            string groupBy = GetGroupByStrings(filters);
            string strSql = string.Empty;
            var keyField = m_DataBaseKyFieldTableDic[filters.TableName];
            if (string.IsNullOrEmpty(keyField))
                keyField = "ID";
            if (filters.IsPage == true)
            {
                var index = filters.PageSize + (filters.PageIndex - 1);
                strSql = "select top " + filters.PageSize + " " + fields + "  from " + filters.TableName +
                   "     where " + keyField + " Not In ( select top " + index + " " + keyField + " from " + filters.TableName + "     where " + whereStr + orderBy + ")";
            }
            else
            {
                strSql = "select " + fields + " from " + filters.TableName + "     where " + whereStr;
                strSql = strSql + groupBy + orderBy;
            }
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);

            int count = int.Parse(table.Rows[0][0].ToString());
            if (count > 1000)
            {
                //DBClassHelper.WriteLog("GetQueryTableResult - SQL 语句查询不规范：" + strSql + ",数据条数为：" + count);
                DBLogHelper.WarnLog($"SQL 语句查询不规范(数据行超过1000)：{strSql},数据条数为：{count}");
            }
            conn.Dispose();
            conn.Close();
            return table;
        }

        internal virtual string GetQueryFields(QueryPageFilter filters)
        {
            if (filters.ReturnFieldNames == null || filters.ReturnFieldNames.Length == 0)
                return GetFieldFromTable(filters.TableName);
            StringBuilder builder = new StringBuilder();
            var shapeFields = m_DicTableToShapeFields[filters.TableName];
            for (int i = 0; i < filters.ReturnFieldNames.Length; i++)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                if (shapeFields.ContainsKey(filters.ReturnFieldNames[i]))
                    builder.Append(filters.ReturnFieldNames[i] + ".STAsText() As " + filters.ReturnFieldNames[i]);
                else
                {
                    var fieldname = filters.ReturnFieldNames[i];
                    if (QueryPageFilter.IsSqlFilter2(fieldname) == true)
                        throw new Exception("数据中含SQL注入，请误使用！");
                    var arr = fieldname.Split('#');
                    if (fieldname.Contains("#") && arr.Length >= 3)
                    {
                        var type = (SqlFunctionType)int.Parse(arr[0]);
                        var oField = arr[1];
                        var nField = arr[2];

                        var fieldName = string.Format("{0} ({1}) as {2}", type.ToString(), oField, nField);
                        builder.Append(fieldName);
                    }
                    else
                        builder.Append(filters.ReturnFieldNames[i]);
                }
            }
            return builder.ToString();
        }


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
                    if (colTypeName == "geometry")
                        builder.Append(col.ColumnName + ".STAsText() As " + col.ColumnName);
                    else
                        builder.Append(col.ColumnName);
                }
            }
            if (builder.Length > 0)
                return builder.ToString();
            return " * ";
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
            string groupBy = GetGroupByStrings(filters);
            var fields = GetQueryFields(filters);
            string sql = "select count(*) from " + filters.TableName + "   where " + whereStr;
            string strSql = string.Empty;
            if (filters.IsPage == true)
            {
                var index = filters.PageSize * (filters.PageIndex - 1);
                if (index < 0)
                    index = 0;

                var keyField = m_DataBaseKyFieldTableDic[filters.TableName];
                strSql = "select top " + filters.PageSize + " " + fields + " from " + filters.TableName +
                   " where " + keyField + " Not In ( select top " + index + " " + keyField + " from " + filters.TableName + "    where " + whereStr + orderBy + ") and  ( " + whereStr + " ) " + orderBy;
                if (CurrPrividerType == SqlPrividerType.Sqlite)
                {
                    strSql = "select " + fields + " from " + filters.TableName +
                           " where " + keyField + " Not In ( select " + keyField + " from " + filters.TableName + "    where " + whereStr + orderBy + " limit 0," + index + " ) and  ( " + whereStr + " ) " + orderBy + " limit 0," + filters.PageSize;
                }
            }
            else
            {
                strSql = "select  " + fields + " from " + filters.TableName + "    where " + whereStr + groupBy + orderBy;
            }

            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
            result.Result = table;
            result.Result.TableName = filters.TableName;
            int count = table.Rows.Count;

            if (count > 1000)
            {
                //DBClassHelper.WriteLog("GetQueryResultFromDB - SQL 语句查询不规范：" + strSql + ",数据条数为：" + count);
                DBLogHelper.WarnLog($"SQL 语句查询不规范(数据行超过1000)：{strSql},数据条数为：{count}");
            }

            if (filters.IsReturnCount == true)
            {
                var tableCount = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
                count = int.Parse(tableCount.Rows[0][0].ToString());
            }


            result.TotalCount = count;
            conn.Dispose();
            conn.Close();
            return result;
        }

        //internal virtual string GetGroupByString(QueryPageFilter filters)
        //{

        //    var groupBy = string.Empty;
        //    if (!string.IsNullOrEmpty(groupByFields))
        //    {
        //        fields = groupByFields;
        //        groupBy = "group by " + groupByFields;
        //    }
        //}

        internal virtual int GetQueryResultCount(QueryPageFilter filters)
        {
            string whereStr = GetWhereString(filters);
            string sql = "select count(*) from " + filters.TableName + "    where " + whereStr;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var tableCount = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
            int count = int.Parse(tableCount.Rows[0][0].ToString());
            conn.Dispose();
            conn.Close();
            return count;
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
            var str = string.Format("{0}.MakeValid().{1}(geometry::STGeomFromText('{2}',{3}).MakeValid())={4}", filter.FieldName, filter.Sign, filter.Geometry.WKT, filter.Geometry.SRID, filter.IsTrue == true ? 1 : 0);
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
                case SQLSign.NotLike:
                    str = filter.FieldName + " not like '%" + filter.Value + "%' ";
                    break;
                case SQLSign.LeftLike:
                    str = filter.FieldName + " like '" + filter.Value + "%' ";
                    break;
                case SQLSign.RightLike:
                    str = filter.FieldName + " like '%" + filter.Value + "' ";
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

                        if (arr.Length > 1000)
                        {
                            //DBClassHelper.WriteLog("GetSqlSign1 - SQL 语句查询不规范：" + sql + ",数据条数为：" + arr.Length);
                            DBLogHelper.WarnLog($"SQL 语句查询不规范(数据行超过1000)：{sql},数据条数为：{arr.Length}");
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

                        if (arr.Length > 1000)
                        {
                            //DBClassHelper.WriteLog("GetSqlSign2 - SQL 语句查询不规范：" + sql + ",数据条数为：" + arr.Length+ ",FieldName:" + filter.FieldName);
                            DBLogHelper.WarnLog($"SQL 语句查询不规范(数据行超过1000)：{sql},数据条数为：{arr.Length},FieldName:{filter.FieldName}");
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
        internal virtual string GetSqlFromTableInfoOld(SqlTableInfo sql)
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
        /// <summary>
        /// 得到查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal virtual string GetSqlFromTableInfo(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            fieldList.PrividerType = CurrPrividerType;
            bool isGxsj = false;
            foreach (var item in sql.Fields)
            {
                if (item.Key == "OBJECTID")
                    continue;// “OBJECTID”是自增树列，会出现 SQL 语句无法更新标识列 2019/07/29
                bool IsPkField = false;
                if (item.Key.ToUpper() == "GXSJ")
                    isGxsj = true;//有没有修改时间
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
                        if (item.Key.ToUpper() == "GXSJ")
                            fieldList.AddNonPrimaryField(item.Key, DateTime.Now);
                        else
                            fieldList.AddNonPrimaryField(item.Key, date);
                    }
                    else
                        fieldList.AddNonPrimaryField(item.Key, item.Value);
                }
                continue;
            }
            if (DicTableToTableNameFields[sql.TableName].ContainsKey("GXSJ"))
            {
                if (isGxsj == false)
                    fieldList.AddNonPrimaryField("GXSJ", DateTime.Now);
            }
            return SqlFieldList.GetSqlString(fieldList);
        }
        /// <summary>
        /// 得到新增插入sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal virtual string GetSqlFromTableInfo2(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            fieldList.PrividerType = CurrPrividerType;
            bool isGxsj = false;
            foreach (var item in sql.Fields)
            {
                if (item.Key == "OBJECTID")
                    continue;// “OBJECTID”是自增树列，会出现 SQL 语句无法更新标识列 2019/07/29
                bool IsPkField = false;
                if (item.Key.ToUpper() == "GXSJ")
                    isGxsj = true;//有没有修改时间
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
                        if (item.Key.ToUpper() == "GXSJ")
                            fieldList.AddNonPrimaryField(item.Key, DateTime.Now);
                        else
                            fieldList.AddNonPrimaryField(item.Key, date);
                    }
                    else
                        fieldList.AddNonPrimaryField(item.Key, item.Value);
                }
                continue;
            }
            if (DicTableToTableNameFields[sql.TableName].ContainsKey("GXSJ"))
            {
                if (isGxsj == false)
                    fieldList.AddNonPrimaryField("GXSJ", DateTime.Now);
            }
            return SqlFieldList.GetAddSqlString(fieldList);
        }
        /// <summary>
        /// 得到更新sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal virtual string GetSqlFromTableInfo3(SqlTableInfo sql)
        {
            SqlFieldList fieldList = new SqlFieldList(sql.TableName);
            fieldList.PrividerType = CurrPrividerType;
            bool isGxsj = false;
            foreach (var item in sql.Fields)
            {
                if (item.Key == "OBJECTID")
                    continue;// “OBJECTID”是自增树列，会出现 SQL 语句无法更新标识列 2019/07/29
                bool IsPkField = false;
                if (item.Key.ToUpper() == "GXSJ")
                    isGxsj = true;//有没有修改时间
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
                        if (item.Key.ToUpper() == "GXSJ")
                            fieldList.AddNonPrimaryField(item.Key, DateTime.Now);
                        else
                            fieldList.AddNonPrimaryField(item.Key, date);
                    }
                    else
                        fieldList.AddNonPrimaryField(item.Key, item.Value);
                }
                continue;
            }
            if (DicTableToTableNameFields[sql.TableName].ContainsKey("GXSJ"))
            {
                if (isGxsj == false)
                    fieldList.AddNonPrimaryField("GXSJ", DateTime.Now);
            }
            return SqlFieldList.GetUpdateSqlString(fieldList);
        }


        public List<string> GetDelSqlList(string tableName, object[] ids)
        {
            string tableName1 = tableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(tableName))
                tableName1 = TableToTableNameDic[tableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")"); ;
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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
            conn.Dispose();
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
            conn.Dispose();
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
            filters.TableName = tableName1;
            string whereStr = GetWhereString(filters);
            string strSql = string.Empty;
            strSql = "delete from " + filters.TableName + " where " + whereStr;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(strSql, conn);
            conn.Dispose();
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
            conn.Dispose();
            conn.Close();
            return table;
        }
        /// <summary>
        /// 执行sql语句，返回dictionary数组，没有结果时
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public Dictionary<string, object>[] ExecuteSqlToDic(string sqlList)
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
                    conn.Dispose();
                    conn.Close();
                    return dic;
                }
            }
            catch
            {
                throw; // <-------------------
            }
            conn.Dispose();
            conn.Close();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> ExecuteSqlToList(string sqlList)
        {
            var re = ExecuteSqlToDic(sqlList);
            if (re != null)
            {
                return re.ToList();
            }
            else
                return new List<Dictionary<string, object>>();
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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
        internal virtual List<Column> GetColumnsFromTableName(string tableName, System.Data.Common.DbConnection conn = null)
        {
            return null;
        }

        public List<string> GetTableNames()
        {
            return GetTableNamesFromService();
        }

        public List<Column> GetColumnsFromTable(string tableName, System.Data.Common.DbConnection conn = null)
        {
            return GetColumnsFromTableName(tableName, conn);
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少filter过滤条件)");
            var queryResult = new QueryFilterResult();
            string str = string.Format(filter.Value, objs);
            var table = ExecuteSqlToDataTable(str);
            if (table != null)
            {
                queryResult.TotalCount = table.Rows.Count;
                var list = ModelTypeConvert<object>.ConvertToModelFromType(filter.NameSpaceName, filter.ModelName, table, false);
                queryResult.Result = list.ToArray();

                if (queryResult.TotalCount > 1000)
                {
                    //DBClassHelper.WriteLog("GetQueryResult - SQL 语句查询不规范：" + str + ",数据条数为：" + queryResult.TotalCount);
                    DBLogHelper.WarnLog($"SQL 语句查询不规范(数据行超过1000)：{str},数据条数为：{queryResult.TotalCount}");
                }
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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


        public Dictionary<string, object> ConvertDataRow(DataRow row, bool isConvert = true)
        {
            var dic = new Dictionary<string, object>();
            var dicShapes = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(row.Table.TableName) == false && DicTableToShapeFields.ContainsKey(row.Table.TableName))
                dicShapes = DicTableToShapeFields[row.Table.TableName];
            foreach (DataColumn item in row.Table.Columns)
            {
                if (dicShapes.ContainsKey(item.ColumnName))
                {
                    var value = row[item.ColumnName];
                    if (item.DataType == typeof(SqlGeometry))
                    {
                        var geom = value as SqlGeometry;
                        var thGeom = new THGeometry() { WKT = geom.ToString() };
                        dic.Add(item.ColumnName, thGeom);
                    }
                    else
                        if (item.DataType == typeof(string))
                    {
                        if (value == null || string.IsNullOrEmpty(value.ToString()))
                            dic.Add(item.ColumnName, null);
                        else
                        {
                            var thGeom = new THGeometry() { WKT = value.ToString() };
                            dic.Add(item.ColumnName, thGeom);
                        }
                    }
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少filter过滤条件)");
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
            filter.TableName = tableName1;

            string whereStr = GetWhereString(filter);
            string strSql = string.Empty;

            strSql = "update " + filter.TableName + " set " + fieldName + " = 1 " + " where " + whereStr;
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(strSql, conn);
            conn.Dispose();
            conn.Close();
        }

        public Dictionary<string, object>[] GetQueryStatResultN(SqlModel filter, params object[] objs)
        {
            if (filter == null)
                throw new Exception("违法输入方式！请联系开发人员！(缺少filter过滤条件)");
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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

        public int GetQueryResultNCount(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
            filter.TableName = tableName1;
            var count = GetQueryResultCount(filter);
            return count;
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


        /// <summary>
        /// 数据库批量新增数据sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dicList"></param>
        /// <param name="isconvertPart"></param>
        public void AddObjects(string tableName, Dictionary<string, object>[] dicList, bool isconvertPart = false)
        {
            var sqlList = new List<string>();
            var sqlTableInfoList = GetSqlList(tableName, dicList, DataBaseKyFieldTableDic, isconvertPart);
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo2(item);
                sqlList.Add(sql);
            }
            if (sqlList.Count > 0)
            {
                var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
                DBClassHelper.ExecuteTrans(sqlList.ToArray(), conn);
                //  conn.Close();
            }
        }
        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sqlList"></param>
        public void UpdateSQLObjects(List<string> sqlList)
        {
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            if (sqlList.Count > 0)
            {
                DBClassHelper.ExecuteTrans(sqlList.ToArray(), conn);
            }
            //conn.Close();
        }

        /// <summary>
        /// 单条新增返回sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dic"></param>
        /// <param name="isconvertPart"></param>
        /// <returns></returns>
        public string AddObject(string tableName, Dictionary<string, object> dic, bool isconvertPart = false)
        {
            var sqlTableInfo = GetSql(tableName, dic, DataBaseKyFieldTableDic, isconvertPart);
            var sql = GetSqlFromTableInfo2(sqlTableInfo);
            return sql;
        }
        /// <summary>
        /// 单条更新返回sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sqlList"></param>
        public string UpdateObject(string tableName, Dictionary<string, object> dic, bool isconvertPart = false)
        {
            var sqlTableInfo = GetSql(tableName, dic, DataBaseKyFieldTableDic, isconvertPart);
            var sql = GetSqlFromTableInfo3(sqlTableInfo);
            return sql;
        }

        public void UpdateObjects(string tableName, object[] sqlList)
        {
            if (sqlList == null)
                throw new Exception("不能传空数据！");

            //var sqlTableInfoList = GetSqlList(tableName, sqlList, DataBaseKyFieldTableDic);
            //StringBuilder builder = new StringBuilder();
            //foreach (var item in sqlTableInfoList)
            //{
            //    var sql = GetSqlFromTableInfo(item);
            //    builder.AppendLine(sql);
            //}

        }

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

        public SqlTableInfo GetSql(string tableName, Dictionary<string, object> model, Dictionary<string, string> DataBaseKyFieldTableDic, bool ispart = false)
        {
            var sql = new SqlTableInfo();
            sql.TableName = tableName;
            if (DataBaseKyFieldTableDic.ContainsKey(tableName))
                sql.KeyFieldName = DataBaseKyFieldTableDic[tableName];
            var diclist = model.Where(m => DicTableToTableNameFields[tableName].ContainsKey(m.Key)).ToDictionary(m => m.Key, n => n.Value);
            if (ispart == false)
            {
                //var noExists = DicTableToTableNameFields[tableName].Where(m => diclist.ContainsKey(m.Key) == false).Select(m => m.Key).ToArray();
                //foreach (var field in noExists)
                //    diclist.Add(field, null);
            }
            sql.Fields = diclist;
            return sql;
        }

        public Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter, ref int count)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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

        /// <summary>
        /// 根据filter表名和条件 更新objs里的字段
        /// </summary>
        /// <param name="objs">要更新的字段和值</param>
        /// <param name="filter">存储表名和更新条件</param>
        /// <returns></returns>
        public int UpdateObjects(Dictionary<string, object> objs, QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！缺少表（" + filter.TableName + ")");
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
            conn.Dispose();
            conn.Close();
            return count;
        }


        public QueryFilterResultDic GetQueryResultDic(QueryPageFilter filter)
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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

        public virtual void UpdateSystemTable(string TableName, List<Column> colomns)
        {

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
            conn.Dispose();
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

        public void BatchInsert<T>(string TableName, List<T> modelList)
        {
            if (modelList.Count == 0)
                return;
            var model = modelList.FirstOrDefault();
            DataTable dt = GetTableSchema(model);
            var propertyInfos = model.GetType().GetProperties();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            if (conn is SqlConnection)
            {
                var sqlconn = conn as SqlConnection;
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
                bulkCopy.DestinationTableName = TableName;
                bulkCopy.BatchSize = modelList.Count;
                foreach (var obj in modelList)
                {
                    DataRow dr = dt.NewRow();
                    foreach (var property in propertyInfos)
                    {
                        dr[property.Name] = property.GetValue(obj);
                    }
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(dt);
                }
            }
            conn.Dispose();
            conn.Close();
        }

        /// <summary>
        /// 查询SQL语句
        /// </summary>
        /// <param name="sqlList"></param>
        internal virtual DataTable GetTableSchema<T>(T model)
        {
            DataTable dt = new DataTable();

            var propertyInfos = model.GetType().GetProperties();
            IList<DataColumn> dcList = new List<DataColumn>();
            foreach (var property in propertyInfos)
            {
                dcList.Add(new DataColumn(property.Name, typeof(object)));
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
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
            filter.TableName = tableName1;
            var result = GetOneQueryResultFromDB(filter);
            var queryResult = new QueryFilterResult();
            queryResult.TotalCount = result.TotalCount;
            queryResult.TableName = filter.TableName;
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null && result.Result.Rows.Count > 0)
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
            conn.Dispose();
            conn.Close();
            return result;
        }

        public T[] GetObjResult<T>(QueryPageFilter filter) where T : class, new()
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
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

        public List<T> GetObjList<T>(QueryPageFilter filter) where T : class, new()
        {
            string tableName1 = filter.TableName;
            if (TableToTableNameDic != null && TableToTableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableToTableNameDic[filter.TableName];
            if (!DataBaseKyFieldTableDic.ContainsKey(tableName1))
                throw new Exception("违法输入方式！请联系开发人员！(缺少" + tableName1 + ")");
            filter.TableName = tableName1;
            var result = GetQueryResultFromDB(filter);
            List<T> list = new List<T>();
            if (filter.IsReturnCount != true)
            {
                if (result.Result != null)
                {
                    list = ModelTypeConvert<T>.ConvertToModel(result.Result);
                }
            }
            return list;
        }

        public T GetQueryObj<T>(QueryPageFilter filter) where T : class, new()
        {
            filter = filter.Page(1, 1);
            var reArr = GetObjResult<T>(filter);
            if (reArr.Length > 0)
                return reArr.First();
            return null;
        }

        public T GetObj<T>(QueryPageFilter filter) where T : class, new()
        {
            filter = filter.Page(1, 1);
            var reArr = GetObjList<T>(filter);
            if (reArr.Count > 0)
                return reArr.First();
            return null;
        }

        public virtual void ExecSqlListWithTrans(List<string> sqlList)
        {
            var sqls = sqlList.Where(m => string.IsNullOrEmpty(m) == false).ToList();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            System.Data.IDbCommand cmd = conn.CreateCommand();
            using (DbTransaction trans = conn.BeginTransaction())
            {
                cmd.Transaction = trans;
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
                finally
                {
                    conn.Dispose();
                    conn.Close();
                }
            }

        }

        public List<Column> GetColumnsFromTable(string tableName)
        {
            return GetColumnsFromTable(tableName, null);
        }
        internal Dictionary<string, Dictionary<string, string>> m_DicTableToShapeFields;
        public Dictionary<string, Dictionary<string, string>> DicTableToShapeFields
        {
            get { return m_DicTableToShapeFields; }
        }

        internal Dictionary<string, string> m_TableTypeDic;
        public Dictionary<string, string> TableTypeDic
        {
            get { return m_TableTypeDic; }
        }

        public void ReInitSqlTable()
        {
            GetSqlTableInfo();
        }

        public bool TranExecuteAll(TranExecuteModel model)
        {
            List<string> sqlList = model.SqlList;
            //要执行的sql语句
            var sqls = sqlList.Where(m => string.IsNullOrEmpty(m) == false).ToList();
            //要更新的表（字典格式）
            List<SqlTableInfo> sqlTableInfoList = new List<SqlTableInfo>();
            foreach (var updateDicModel in model.UpdateDicModel)
            {
                var sqlTableInfoArr = GetSqlList(updateDicModel.TableName, updateDicModel.UpdateList.ToArray(), DataBaseKyFieldTableDic);
                sqlTableInfoList.AddRange(sqlTableInfoArr);
            }
            //要更新的表（模型）
            var sqlTableInfoArr2 = ModelTypeConvert<object>.GetSqlList(model.UpdateModelList.ToArray(), DataBaseKyFieldTableDic);
            sqlTableInfoList.AddRange(sqlTableInfoArr2);
            //所有要执行的sql语句
            foreach (var item in sqlTableInfoList)
            {
                var sql = GetSqlFromTableInfo(item);
                sqls.Add(sql);
            }

            //日志记录key
            string logKey = Guid.NewGuid().ToString();
            DBLogHelper.InfoLog($"DataBase logKey:{logKey}。事务执行，sqlList：{JsonConvert.SerializeObject(sqls)}");

            //开始执行sql语句
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            IDbCommand cmd = conn.CreateCommand();
            using (DbTransaction trans = conn.BeginTransaction())
            {
                cmd.Transaction = trans;
                int wrongIndex = 0;
                string wrongSql = string.Empty;
                try
                {
                    DBLogHelper.InfoLog($"DataBase logKey:{logKey}。开启执行事务，开始时间：{DateTime.Now}");
                    foreach (var item in sqls)
                    {
                        wrongIndex++;
                        wrongSql = item;
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    //只有全部没问题，事务才会提交
                    trans.Commit();
                    DBLogHelper.InfoLog($"DataBase logKey:{logKey}。结束执行事务，结束时间：{DateTime.Now}");
                    return true;
                }
                catch (Exception ex)
                {
                    //捕获到异常，事务回滚
                    trans.Rollback();
                    DBLogHelper.ErrorLog(ex, $"DataBase logKey:{logKey}。事务执行sql失败,错误的sql序号:{wrongIndex},sql:{wrongSql}。");
                    throw;
                }
                finally
                {
                    trans.Dispose();
                    cmd.Dispose();
                    conn.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行update sql语句，返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteUpdateSql(string sql)
        {
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            int count = DBClassHelper.Execute(sql, conn);
            return count;
        }
    }
}
