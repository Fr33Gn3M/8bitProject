using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataBase
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
            m_TableTypeDic = new Dictionary<string, string>();
            string tablesSql = "SELECT NAME,TYPE FROM SYSOBJECTS WHERE TYPE='U' or TYPE ='V'";
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
                    var type = row["TYPE"].ToString().Trim();
                    m_TableToTableNameDic.Add(name, name);
                    m_TableTypeDic.Add(name, type);
                    if (m_DataBaseKyFieldTableDic.ContainsKey(name) == false)
                        m_DataBaseKyFieldTableDic.Add(name, "");
                }
            }


            foreach (var item in m_TableToTableNameDic)
            {
                if (!m_TableToTableNameFields.ContainsKey(item.Key))
                {
                    var cols = GetColumnsFromTableName(item.Key, conn);
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



        internal override List<Column> GetColumnsFromTableName(string tableName, System.Data.Common.DbConnection conn=null)
        {
            if (m_TableToTableNameFields != null && m_TableToTableNameFields.ContainsKey(tableName))
                return m_TableToTableNameFields[tableName];
            if(conn==null)
             conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            List<Column> columns = new List<Column>();
            try
            {
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
        /*
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
                finally
                {
                    conn.Close();
                }
            }
        }
        */
        /*
        public override void UpdateSystemTable(string TableName, List<Column> colomns)
        {
            string def = string.Empty;
            var builder = new StringBuilder();
            var builder2 = new StringBuilder();
            var descBuilder = new StringBuilder();
            if (TableToTableNameDic.ContainsKey(TableName))
            {
                int i = 1;
                foreach (var item in colomns)
                {
                    i++;
                    string len = "";
                    string isNullStr;
                    if (item.Precision == 0) len = "";
                    else if (item.Precision == -1) len = "(MAX)";
                    else len = string.Format("({0})", item.Precision);

                    if (item.TypeName == "varchar" && string.IsNullOrEmpty(len)) len = "(50)";
                    if (item.Default != null && !string.IsNullOrEmpty(item.Default))//修改数据库默认值，删除约束，添加约束（添加常量默认值必须加‘ ’；例  'GAAJ'）
                    {
                        if (item.Default == "'auto'")
                        {

                            //String sql2 = string.Format(@" ALTER TABLE {0} ADD {1} INT IDENTITY (1,1);", TableName, item.ColumnName);
                            //builder2.AppendLine(sql2);
                            continue;

                        }
                        else
                        {
                            def = "((" + item.Default + "))";
                            string sql2 = string.Format(@"IF EXISTS(select a.name as 用户表,b.name as 字段名,d.name as 字段默认值约束 from sysobjects a
                                                      inner join syscolumns b on (a.id=b.id)inner join syscomments c on ( b.cdefault=c.id )
                                                      inner join sysobjects d on (c.id=d.id) 
                                                       where a.name='{0}'and b.name='{1}') 
                                                       declare @tablename{2} varchar(30)
                                                       declare @fieldname{2} varchar(50)
                                                       declare @sql{2} varchar(300)
 
                                                       set @tablename{2}='{0}'
                                                       set @fieldname{2}='{1}'
                                                       set @sql{2}=''
                                                       select @sql{2}=@sql{2}+' 
                                                       alter table ['+a.name+'] drop constraint ['+d.name+']'   
                                                       from sysobjects a   
                                                       join syscolumns b on a.id=b.id   
                                                       join syscomments c on b.cdefault=c.id   
                                                       join sysobjects d on c.id=d.id   
                                                       where a.name=@tablename{2} and b.name=@fieldname{2}
                                                       exec(@sql{2})", TableName, item.ColumnName, i);

                            string sql3 = string.Format(@" ALTER TABLE {0}  ADD DEFAULT {1} FOR {2} WITH VALUES", TableName, def, item.ColumnName);

                            builder2.AppendLine(sql2 + sql3);
                        }
                    }
                    if (item.IsPrimaryKey == true)
                    {
                        isNullStr = "NOT NULL";
                    }
                    else
                    {
                        isNullStr = "NULL";
                    }
                    string sql = string.Format(
                        @" if exists(select * from syscolumns where id=object_id('{0}') and name='{1}') 
                                    alter table {0} alter column  {1}  {2}{3} {4} ;  else   alter table {0}  add   {1}  {2}{3} {4} ", TableName, item.ColumnName, item.TypeName, len, isNullStr);
                    var str = string.Format("if exists(SELECT c.name,p.value FROM sys.extended_properties p ,sys.columns c " +
                        "WHERE p.major_id=OBJECT_ID('{0}') and p.major_id=c.object_id and p.minor_id=c.column_id and c.name = '{2}') " +
                        "EXECUTE sp_updateextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{0}', N'column', N'{2}';" +
                        "else EXECUTE sp_addextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{0}', N'column', N'{2}';", TableName, item.Label, item.ColumnName);
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
                    string defs = string.Empty;
                    if (item.Default != null)
                    {
                        defs = " DEFAULT((" + item.Default + "))";
                    }
                    string len = "";
                    if (item.Precision == 0) len = "";
                    else if (item.Precision == -1) len = "(MAX)";
                    else len = string.Format("({0})", item.Precision);

                    if (item.TypeName == "varchar" && string.IsNullOrEmpty(len)) len = "(50)";
                    if (item.Default != null && item.Default == "'auto'")
                    {
                        builder.AppendLine(string.Format("{0}  INT IDENTITY (1,1),", item.ColumnName));
                    }
                    else
                    {
                        builder.AppendLine(string.Format("{0}   {1}{2}  {3} null  {4},", item.ColumnName, item.TypeName, len, (item.IsNull ? "" : " not "), defs));
                    }

                    var str = string.Format("EXECUTE sp_addextendedproperty N'MS_Description', '{1}', N'user', N'dbo', N'table', N'{0}', N'column', N'{2}'", TableName, item.Label, item.ColumnName);
                    descBuilder.AppendLine(str);
                }
                builder.AppendLine(string.Format(" constraint PK_{0} primary key clustered ({1})); ", TableName, keyCol.ColumnName));
            }
            // builder.AppendLine(descBuilder.ToString());
            var strSql = builder.ToString();
            var descSql = descBuilder.ToString();
            var defSql = builder2.ToString();
            if (!string.IsNullOrEmpty(strSql))
            {
                var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
                int count = DBClassHelper.Execute(strSql, conn);
                if (!string.IsNullOrEmpty(descSql))
                {
                    DBClassHelper.Execute(descSql, conn);
                }
                if (!string.IsNullOrEmpty(defSql))
                {
                    DBClassHelper.Execute(defSql, conn);
                }
                GetSqlTableInfo();
                conn.Close();
            }
        }
        */
        internal override FilterQueryResult GetQueryResultFromDB(QueryPageFilter filters)
        {
            FilterQueryResult result = new FilterQueryResult();
            string whereStr = GetWhereString(filters);
            string orderBy = GetOrderByString(filters);
            string groupBy = GetGroupByStrings(filters);
            var fields = GetQueryFields(filters);
            string sql = "select count(*) from " + filters.TableName + " where " + whereStr;
            string strSql = string.Empty;
            if (filters.IsPage == true)
            {
                var index = filters.PageSize * (filters.PageIndex - 1);
                if (index < 0)
                    index = 0;
                var keyField = m_DataBaseKyFieldTableDic[filters.TableName];
                var allFieldsArr = m_DicTableToTableNameFields[filters.TableName];
                var queryFieldsArr = fields.Replace(" ","").Split(',');

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

                var pageSize = filters.PageSize;
                if (pageSize == 0)
                    pageSize = 1;
                //分页查询优化，FZP
                strSql = string.Format("select {0} from {1} where {2} {3} offset {4} rows fetch next {5} rows only", fields, filters.TableName, whereStr, orderBy, index, pageSize);
            }
            else
            {
                strSql = "select  " + fields + " from " + filters.TableName + " where " + whereStr + groupBy + orderBy;
            }
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            var tableCount = DBClassHelper.ExecuteQueryToDataTable(sql, conn);
            int count = int.Parse(tableCount.Rows[0][0].ToString());
            if (filters.IsReturnCount == false)
            {
                var table = DBClassHelper.ExecuteQueryToDataTable(strSql, conn);
                result.Result = table;
                result.Result.TableName = filters.TableName;
            }
            result.TotalCount = count;
            conn.Close();
            return result;
        }
    }
}
