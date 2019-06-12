using System;
using System.Collections.Generic;
using System.Data;

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

        internal virtual void GetSqlTableInfo()
        {

        }


        public Dictionary<string, object>[] GetQueryResult(QueryPageFilter filter, ref int count)
        {
            string tableName1 = filter.TableName;
            if (TableNameDic != null && TableNameDic.ContainsKey(filter.TableName))
                tableName1 = TableNameDic[filter.TableName];
            if (!DateBaseKeyFieldDic.ContainsKey(tableName1))
                throw new Exception("表名不存在主键，请尝试重启服务！");
            filter.TableName = tableName1;
            var result = GetQueryResultByFilter(filter);
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
        /// 查询数据库
        /// </summary>
        /// <param name="filters">过滤条件filter对象</param>
        /// <returns>结果对象</returns>
        internal virtual FilterQueryResult GetQueryResultByFilter(QueryPageFilter filters)
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
    }
}
