using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FD.DataBase
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public DataBaseHelper(string connstr, string modelNameSpace, SqlPrividerType prividerType)
        {
            CurrPrividerType = prividerType;
            CurrConnectionString = connstr;
            ModelNameSpace = modelNameSpace;
            GetSqlTableInfo();
        }

        public DataBaseHelper(string connstr, string modelNameSpace, SqlPrividerType prividerType, Dictionary<string, string> dataBaseKeyFieldDic, Dictionary<string, string> tableNameDic)
        {
            CurrPrividerType = prividerType;
            CurrConnectionString = connstr;
            DateBaseKeyFieldDic = dataBaseKeyFieldDic;
            TableNameDic = tableNameDic;
            ModelNameSpace = modelNameSpace;
        }

        public SqlPrividerType CurrPrividerType { get; internal set; }
        public string CurrConnectionString { get; internal set; }
        public string ModelNameSpace { get; internal set; }
        public Dictionary<string, string> DateBaseKeyFieldDic { get; internal set; }  //DataBaseKyFieldTableDic
        public Dictionary<string, string> TableNameDic { get; internal set; }     //TableToTableNameDic

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="count"></param>
        /// <returns>dictionary数组</returns>
        public Dictionary<string, object>[] GetQueryResult(QueryPageFilter filter, ref int count)
        {
            string tableName1 = filter.TableName;
            if (TableNameDic != null && TableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableNameDic[filter.TableName];
            if (!DateBaseKeyFieldDic.ContainsKey(tableName1))
                throw new Exception("表名不存在主键，请尝试重启服务！");
            filter.TableName = tableName1;
            var result = GetQueryResult(filter);
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
        /// DataTable转Dictionary
        /// </summary>
        /// <param name="row"></param>
        /// <param name="isConvert"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ConvertDataRow(DataRow row, bool isConvert = true)
        {
            var dic = new Dictionary<string, object>();
            foreach (DataColumn item in row.Table.Columns)
            {
                if (item.DataType == typeof(SqlGeometry))
                {
                    var value = row[item.ColumnName];
                    var geom = value as SqlGeometry;
                    dic.Add(item.ColumnName, geom);
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

        internal virtual void GetSqlTableInfo()
        {

        }

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="filters">过滤条件filter对象</param>
        /// <returns>FilterQueryResult结果对象</returns>
        internal virtual FilterQueryResult GetQueryResult(QueryPageFilter filters)
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
            var conn = DBCoreHelper.OpenConnect(CurrConnectionString, DBHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var tableCount = DBCoreHelper.ExecuteQueryToDataTable(sql, conn);
            int count = int.Parse(tableCount.Rows[0][0].ToString());
            if (filters.IsReturnCount == false)
            {
                var table = DBCoreHelper.ExecuteQueryToDataTable(strSql, conn);
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

        /// <summary>
        /// 得到sql与或门条件
        /// </summary>
        /// <param name="filter"></param>
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

        /// <summary>
        /// 得到sql空间条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal virtual string GetSqlSign(SpatialQueryFilter filter)
        {
            var str = string.Format("{0}.MakeValid().{1}(geometry::STGeomFromText('{2}',{3}).MakeValid())={4}", filter.FieldName, filter.Sign, filter.Geometry.WKT, filter.Geometry.SRID, filter.IsTrue == true ? 1 : 0);
            return str;
        }

        /// <summary>
        /// 得到sql基础条件
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
        /// 得到条件右边的值
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
        /// 得到查询字段
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
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
    }
}
