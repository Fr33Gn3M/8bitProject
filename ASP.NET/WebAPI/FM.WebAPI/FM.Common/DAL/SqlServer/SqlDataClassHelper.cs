using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SqlDataClassHelper : DataClassHelperBase
    {
        public SqlDataClassHelper(string connstr, string modelNameSpace)
            : base(connstr, modelNameSpace, SqlPrividerType.SqlClient)
        {
        }

        public SqlDataClassHelper(string connstr, string modelNameSpace, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
            : base(connstr, modelNameSpace, SqlPrividerType.SqlClient, dataBaseKyFieldTableDic, tableToTableNameDic)
        {
        }

        internal override void GetSqlTableInfo()
        {
            m_DataBaseKyFieldTableDic = new Dictionary<string, string>();
            m_TableToTableNameDic = new Dictionary<string, string>();
            m_TableToTableNameFields = new Dictionary<string, List<Column>>();
            m_DicTableToTableNameFields = new Dictionary<string, Dictionary<string,string>>();
            string tablesSql = "SELECT NAME FROM SYSOBJECTS WHERE TYPE='U' or TYPE ='V'";
            string keySql = " SELECT TABLE_NAME,COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE ";
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(tablesSql, conn);
            var keytable = DBClassHelper.ExecuteQueryToDataTable(keySql, conn);
            if (table != null)
            {
                if (keytable != null)
                {
                    foreach (DataRow row in keytable.Rows)
                    {
                        var name = row["TABLE_NAME"].ToString();
                        var column = row["COLUMN_NAME"].ToString();
                        if (!m_DataBaseKyFieldTableDic.ContainsKey(name))
                            m_DataBaseKyFieldTableDic.Add(name, column);
                    }
                }
                foreach (DataRow row in table.Rows)
                {
                    var name = row["NAME"].ToString();
                    m_TableToTableNameDic.Add(name, name);
                    if (m_DataBaseKyFieldTableDic.ContainsKey(name) == false)
                        m_DataBaseKyFieldTableDic.Add(name, "");
                }
            }


            foreach (var item in m_TableToTableNameDic)
            {
                if (!m_TableToTableNameFields.ContainsKey(item.Key))
                {
                    var cols = GetColumnsFromTableName(item.Key);
                    m_TableToTableNameFields.Add(item.Key, cols);
                    var list = cols.ToDictionary(m => m.ColumnName, n => n.TypeName);
                    m_DicTableToTableNameFields.Add(item.Key, list);
                }
            }
            conn.Close();
        }

        internal override List<string> GetTableNamesFromService()
        {
            var tables = new List<string>();
            string tablesSql = "SELECT NAME FROM SYSOBJECTS WHERE TYPE='U' or TYPE ='V'";
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
            if (m_TableToTableNameFields != null && m_TableToTableNameFields.ContainsKey(tableName))
                return m_TableToTableNameFields[tableName];
            List<Column> columns = new List<Column>();
            try
            {
                var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
                string strSql = string.Format("SELECT c.name, [tablename]=t.name,cast(ep.[value]  as varchar(100)) AS [desc]," +
                                     "[Type]=(select name from sys.types where sys.types.user_type_id=c.user_type_id)," +
                                     "[Precision] = columnproperty(t.object_id,c.name,'Precision'), c.Scale,c.is_nullable," +
                                     "c.object_id,c.column_id" +
                             " FROM sys.columns c JOIN sys.tables t ON c.object_id=t.object_id   LEFT JOIN sys.extended_properties AS ep   ON ep.major_id = c.object_id AND ep.minor_id = c.column_id" +
                             " WHERE t.name='{0}'", tableName);
                var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
                if (table == null || table.Rows.Count == 0)
                {
                    strSql = string.Format("SELECT c.name, [tablename]=t.name,null as [desc], " +
                                     "[Type]=(select name from sys.types where sys.types.user_type_id=c.user_type_id)," +
                                     "[Precision] = columnproperty(t.object_id,c.name,'Precision'), c.Scale,c.is_nullable," +
                                     "c.object_id,c.column_id" +
                             " FROM sys.columns c JOIN sys.views t ON c.object_id=t.object_id " +
                             " WHERE t.name='{0}'", tableName);
                    table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
                }
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Column column = new Column
                        {
                            ColumnName = row["name"].ToString(),
                            TableName = row["tablename"].ToString(),
                            TypeName = row["Type"].ToString(),
                            Label = row["desc"].ToString(),
                            Precision = int.Parse(row["Precision"].ToString()),
                            Scale = byte.Parse(row["Scale"].ToString()),
                            IsNull = bool.Parse(row["is_nullable"].ToString()),
                        };
                        if (m_DataBaseKyFieldTableDic.ContainsKey(tableName) && m_DataBaseKyFieldTableDic[tableName] == column.ColumnName)
                            column.IsPrimaryKey = true;
                        columns.Add(column);
                    }
                }
            }
            catch
            {
                
            }
            return columns;
        }

        public override void ExecuteSqlList(List<string> sqlList)
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

                strSql = "select top " + filters.PageSize + fields + " from " + filters.TableName +
                   " where Id Not In ( select top " + index + " Id from " + filters.TableName + " where " + whereStr + orderBy + ") and  ( " + whereStr + " ) " + orderBy;
            }
            else
            {
                strSql = "select  " + fields + " from " + filters.TableName + " where " + whereStr + orderBy;
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

            //string whereStr = string.Empty;
            //whereStr = GetWhereString(filters);
            //FilterQueryResult result = new FilterQueryResult();
            //string orderBy = GetOrderByString(filters);
            //string sql = "select count(*) from " + filters.TableName + " where " + whereStr;
            //string strSql = string.Empty;
            //if (filters.IsPage == true)
            //{
            //    var index = filters.PageSize * (filters.PageIndex - 1);
            //    if (index < 0)
            //        index = 0;

            //    strSql = "select top " + filters.PageSize + " * from " + filters.TableName +
            //       " where Id Not In ( select top " + index + " Id from " + filters.TableName + " where " + whereStr + orderBy + ") and  ( " + whereStr + " ) " + orderBy;
            //}
            //else
            //    strSql = "select * from " + filters.TableName + " where " + whereStr + orderBy;
            //var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            //var tableCount = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
            //int count = int.Parse(tableCount.Rows[0][0].ToString());
            //if (filters.IsReturnCount == false)
            //{
            //    var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
            //    result.Result = table;
            //}
            //result.TotalCount = count;
            //conn.Close();
            //return result;
        }
    }
}
