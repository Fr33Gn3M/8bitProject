using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FD.DataBase
{
    public class SqliteDataClassHelper : DataClassHelperBase
    {
        public SqliteDataClassHelper(string connstr, string modelNameSpace)
            : base(connstr, modelNameSpace, SqlPrividerType.Sqlite)
        {
        }

        public SqliteDataClassHelper(string connstr, string modelNameSpace, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
            : base(connstr, modelNameSpace, SqlPrividerType.Sqlite,dataBaseKyFieldTableDic, tableToTableNameDic)
        {
        }

        internal override void GetSqlTableInfo()
        {
            m_DataBaseKyFieldTableDic = new Dictionary<string, string>();
            m_TableToTableNameDic = new Dictionary<string, string>();
            string tablesSql = "select NAME from sqlite_master where type='table' and type='view' order by NAME;";
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(tablesSql, conn);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var name = row["NAME"].ToString();
                    m_TableToTableNameDic.Add(name, name);
                    string sql = "PRAGMA table_info([" + name + "]) ";
                    var keyTable = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
                    var rows = keyTable.Select("pk=1");
                    if (rows.Length > 0)
                    {
                        var keyName = rows[0]["name"].ToString();
                        m_DataBaseKyFieldTableDic.Add(name, keyName);
                    }
                    else
                        m_DataBaseKyFieldTableDic.Add(name, "");
                }
            }
            conn.Close();
        }

        internal override List<string> GetTableNamesFromService()
        {
            var tables = new List<string>();
            string tablesSql = "select NAME from sqlite_master where type='table' order by NAME;";
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(tablesSql, conn);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var name = row["NAME"].ToString();
                    tables.Add(name);
                }
            }
            return tables;
        }

        internal override List<Column> GetColumnsFromTableName(string tableName)
        {
            List<Column> columns = new List<Column>();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            string sql = "PRAGMA table_info([" + tableName + "]) ";
            var table = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    Column column = new Column
                    {
                        ColumnName = row["name"].ToString(),
                        TableName = row["tablename"].ToString(),
                        TypeName = row["Type"].ToString(),
                        Precision = int.Parse(row["Precision"].ToString()),
                        Scale = byte.Parse(row["Scale"].ToString()),
                        IsNull = bool.Parse(row["is_nullable"].ToString()),
                    };
                    if (m_DataBaseKyFieldTableDic.ContainsKey(tableName) && m_DataBaseKyFieldTableDic[tableName] == column.ColumnName)
                        column.IsPrimaryKey = true;
                    columns.Add(column);
                }
            }
            return columns;
        }

        internal override FilterQueryResult GetQueryResultFromDB(QueryPageFilter filters)
        {
            FilterQueryResult result = new FilterQueryResult();
            string whereStr = GetWhereString(filters);
            string orderBy = GetOrderByString(filters);
            var fields = GetQueryFields(filters);
            string sql = "select count(*) from " + filters.TableName + " where " + whereStr;
            string strSql = string.Empty;
            if (filters.IsPage == true)
            {
                var index = filters.PageSize * (filters.PageIndex - 1);
                if (index < 0)
                    index = 0;
                strSql = "select  " + fields + " from " + filters.TableName +
                       " where Id Not In ( select Id from " + filters.TableName + " where " + whereStr + orderBy + " limit 0," + index + " ) and  ( " + whereStr + " ) " + orderBy + " limit 0," + filters.PageSize;
            }
            else
                strSql = "select  " + fields + " from " + filters.TableName + " where " + whereStr + orderBy;
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
     
    }
}
