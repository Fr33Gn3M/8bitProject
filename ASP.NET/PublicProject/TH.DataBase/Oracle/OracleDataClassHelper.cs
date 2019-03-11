using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Data.Common;


namespace TH.DataBase
{
    public class OracleDataClassHelper:DataClassHelperBase
    {
          public OracleDataClassHelper(string connstr, string modelNameSpace)
            : base(connstr, modelNameSpace, SqlPrividerType.OracleClient)
        {
        }

          public OracleDataClassHelper(string connstr, string modelNameSpace, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
            : base(connstr, modelNameSpace, SqlPrividerType.OracleClient,dataBaseKyFieldTableDic,tableToTableNameDic)
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
                          dicInfo.Add(arr[0].ToUpper(), arr[1].ToUpper());
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
            string tablesSql = string.Format("select table_name from all_all_tables  where owner ='{0}'", dicInfo["USER ID"]);
            var keySql = string.Format("select cu.Column_name,cu.table_name from all_cons_columns cu, all_constraints au where cu.constraint_name = au.constraint_name and au.constraint_type = 'P' and au.owner = '{0}'", dicInfo["USER ID"]);
            string viewsSql = string.Format("select view_name from all_views  where owner ='{0}'", dicInfo["USER ID"]);
            var viewTable = DBClassHelper.ExecuteQueryToDataTable(viewsSql, conn);
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
                        var column = row["Column_name"].ToString();
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
            if (viewTable != null)
            {
                foreach (DataRow row in viewTable.Rows)
                {
                    var name = row["view_name"].ToString();
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
            GetConnection();
            var tables = new List<string>();
            string tablesSql = string.Format("select table_name from all_all_tables  where owner ='{0}'", dicInfo["USER ID"]);
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
            GetConnection();
            List<Column> columns = new List<Column>();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            string strSql = string.Format("SELECT  column_name,table_name,data_type,data_length,nullable FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = '{0}' AND OWNER ='{1}' ORDER BY COLUMN_ID", tableName, dicInfo["USER ID"]);
            var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
            if (table != null&&table.Rows.Count>0)
            {
                foreach (DataRow row in table.Rows)
                {
                    bool isnull = true;
                    isnull = row["nullable"].ToString().ToLower() == "Y" ? true : false;
                    Column column = new Column
                    {
                        ColumnName = row["column_name"].ToString(),
                        TableName = row["table_name"].ToString(),
                        TypeName = row["data_type"].ToString(),
                        Precision = int.Parse(row["data_length"].ToString()),
                        //Scale = byte.Parse(row["Scale"].ToString()),
                        IsNull = isnull,
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

                var index2 = filters.PageSize * (filters.PageIndex);
                strSql = string.Format("select " + fields + " from (select A.*,ROWNUM RN from {0}  A where ROWNUM<={1} and {2} {3} ) where RN >{4}", filters.TableName, index2, whereStr, orderBy, index);
                //strSql = "select top " + filters.PageSize + " * from " + filters.TableName +
                //   " where Id Not In ( select top " + index + " Id from " + filters.TableName + " where " + whereStr + orderBy + ") and  ( " + whereStr + " ) " + orderBy;
            }
            else
                strSql = "select " + fields + " from " + filters.TableName + " where " + whereStr + orderBy;
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
