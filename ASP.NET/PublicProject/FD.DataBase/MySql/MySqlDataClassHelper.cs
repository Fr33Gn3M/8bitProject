using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FD.DataBase
{
    public class MySqlDataClassHelper : DataClassHelperBase
    {
        public MySqlDataClassHelper(string connstr, string modelNameSpace)
            : base(connstr, modelNameSpace, SqlPrividerType.MySqlClient)
        {
        }

        public MySqlDataClassHelper(string connstr, string modelNameSpace, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
            : base(connstr, modelNameSpace, SqlPrividerType.MySqlClient, dataBaseKyFieldTableDic, tableToTableNameDic)
        {
        }

        private Dictionary<string, string> dicInfo = null;
        private void GetConnection()
        {
            if (dicInfo == null)
            {
                dicInfo = new Dictionary<string, string>();
                var list = CurrConnectionString.Split(';');
                foreach (var item in list)
                {
                    var arr = item.Split('=');
                    if (arr.Length == 2)
                        dicInfo.Add(arr[0].ToUpper(), arr[1].ToLower());
                }
            }

        }
        internal override void GetSqlTableInfo()
        {
            GetConnection();
            m_DataBaseKyFieldTableDic = new Dictionary<string, string>();
            m_TableToTableNameDic = new Dictionary<string, string>();
            m_TableToTableNameFields = new Dictionary<string, List<Column>>();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            string tablesSql = string.Format("select table_name from information_schema.tables  where table_schema ='{0}'", dicInfo["DATABASE"]);
            var keySql = string.Format("SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE table_schema = '{0}'", dicInfo["DATABASE"]);
           // string viewsSql = string.Format("SELECT table_name FROM information_schema.VIEWS WHERE table_schema='{0}'", dicInfo["DATABASE"]);
            //var viewTable = DBClassHelper.ExecuteQueryToDataTable(viewsSql, conn);
            var table = DBClassHelper.ExecuteQueryToDataTable(tablesSql, conn);
            var keytable = DBClassHelper.ExecuteQueryToDataTable(keySql, conn);

            if (table != null)
            {
                if (keytable != null)
                {
                    foreach (DataRow row in keytable.Rows)
                    {
                        var name = row["table_name"].ToString();
                        if (m_DataBaseKyFieldTableDic.ContainsKey(name))
                            continue;
                        var column = row["column_name"].ToString();
                        m_DataBaseKyFieldTableDic.Add(name, column);
                    }
                }
                foreach (DataRow row in table.Rows)
                {
                    var name = row["table_name"].ToString();
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
            //if (viewTable != null)
            //{
            //    foreach (DataRow row in viewTable.Rows)
            //    {
            //        var name = row["table_name"].ToString();
            //        m_TableToTableNameDic.Add(name, name);
            //        if (m_DataBaseKyFieldTableDic.ContainsKey(name) == false)
            //            m_DataBaseKyFieldTableDic.Add(name, "");
            //    }
            //}
            conn.Close();
        }

        //internal override void GetSqlTableInfo()
        //{
        //    GetConnection();
        //    m_DataBaseKyFieldTableDic = new Dictionary<string, string>();
        //    m_TableToTableNameDic = new Dictionary<string, string>();
        //    string tablesSql = "SELECT NAME FROM SYSOBJECTS WHERE TYPE='U' or TYPE ='V'";
        //    string keySql = " SELECT TABLE_NAME,COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE ";
        //    var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
        //    var table = DBClassHelper.ExecuteQueryToDataTable(tablesSql, conn);
        //    var keytable = DBClassHelper.ExecuteQueryToDataTable(keySql, conn);
        //    if (table != null)
        //    {
        //        if (keytable != null)
        //        {
        //            foreach (DataRow row in keytable.Rows)
        //            {
        //                var name = row["TABLE_NAME"].ToString();
        //                var column = row["COLUMN_NAME"].ToString();
        //                if (!m_DataBaseKyFieldTableDic.ContainsKey(name))
        //                m_DataBaseKyFieldTableDic.Add(name, column);
        //            }
        //        }
        //        foreach (DataRow row in table.Rows)
        //        {
        //            var name = row["NAME"].ToString();
        //            m_TableToTableNameDic.Add(name, name);
        //            if (m_DataBaseKyFieldTableDic.ContainsKey(name) == false)
        //                m_DataBaseKyFieldTableDic.Add(name, "");
        //        }
        //    }

        //    conn.Close();
        //}

        internal override List<string> GetTableNamesFromService()
        {
            var tables = new List<string>();
            string tablesSql = string.Format("SELECT table_name FROM information_schema.TABLES WHERE  table_schema='{0}' and TYPE='BASE TABLE' or TYPE ='VIEW'",dicInfo["DATABASE"]);
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var table = DBClassHelper.ExecuteQueryToDataTable(tablesSql, conn);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var name = row["table_name"].ToString();
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
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            string strSql = string.Format(" SELECT column_name,table_name,data_type,character_maximum_length,numeric_scale,is_nullable FROM information_schema.COLUMNS WHERE table_schema='{0}' and TABLE_NAME='{1}' ", dicInfo["DATABASE"], tableName);
            var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
            //if (table == null || table.Rows.Count == 0)
            //{
            //    strSql = string.Format("SELECT c.name, [tablename]=t.name," +
            //                     "[Type]=(select name from sys.types where sys.types.user_type_id=c.user_type_id)," +
            //                     "[Precision] = columnproperty(t.object_id,c.name,'Precision'), c.Scale,c.is_nullable," +
            //                     "c.object_id,c.column_id" +
            //             " FROM sys.columns c JOIN sys.views t ON c.object_id=t.object_id " +
            //             " WHERE t.name='{0}'", tableName);
            //    table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
            //}
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int length=0;
                    var length2 = row["character_maximum_length"];
                    if (length2 != null && length2 != DBNull.Value)
                        length =int.Parse(row["character_maximum_length"].ToString());
                    Column column = new Column
                    {
                        ColumnName = row["column_name"].ToString(),
                        TableName = row["table_name"].ToString(),
                        TypeName = row["data_type"].ToString(),
                        Precision = length,
                       // Scale = byte.Parse(row["numeric_scale"].ToString()),
                        IsNull = row["is_nullable"].ToString()=="YES",
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
