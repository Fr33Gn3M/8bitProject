using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataBase
{
    public class MySqlDataClassHelper
    {
        /*
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
            m_DicTableToTableNameFields = new Dictionary<string, Dictionary<string, string>>();
            m_TableTypeDic = new Dictionary<string, string>();
            var conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
            string tablesSql = string.Format("select table_name,table_type from information_schema.tables  where table_schema ='{0}'", dicInfo["DATABASE"]);
            var keySql = string.Format("SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE table_schema = '{0}'", dicInfo["DATABASE"]);
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
                    var type = row["table_type"].ToString().Trim();
                    m_TableTypeDic.Add(name, type);
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

        internal override List<Column> GetColumnsFromTableName(string tableName, System.Data.Common.DbConnection conn = null)
        {
            if (m_TableToTableNameFields != null && m_TableToTableNameFields.ContainsKey(tableName))
                return m_TableToTableNameFields[tableName];
            List<Column> columns = new List<Column>();
            if(conn==null)
             conn = DBClassHelper.OpenConnect(CurrConnectionString, SqlHelperFactory.GetSqlPrividerTypeName(CurrPrividerType));
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
                        int.TryParse(row["character_maximum_length"].ToString(),out length);
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

        /// <summary>
        /// 还没有实现
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="colomns"></param>
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
                    string typeName = item.TypeName;
                    string isNullStr;
                    GetDataType(item, ref typeName, ref len);
                    if (item.Default != null)//修改数据库默认值，删除约束，添加约束（添加常量默认值必须加‘ ’；例  'GAAJ'）
                    {
                        if (item.Default == "'auto'")
                        {
                            String sql2 = string.Format(@" ALTER TABLE {0} ADD {1} NOT NULL AUTO_INCREMENT;", TableName, item.ColumnName);
                            builder2.AppendLine(sql2);
                            continue;
                        }
                        else
                        {
                            def = "'" + item.Default + "'";
                            string sql2 = string.Format(" alter table {0} alter {1} drop default; ", TableName, item.ColumnName);
                            string sql3 = string.Format(@"alter table {0} alter {1} set default {2};", TableName, def, item.ColumnName);
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
                        @" if exists(select * from information_schema.COLUMNS where id=tablename='{0}' and column_name='{1}') 
                                    alter table {0} alter column  {1}  {2}{3} {4} ;  else   alter table {0}  add   {1}  {2}{3} {4} ", TableName, item.ColumnName, typeName, len, isNullStr);
                    var str = string.Format("ALTER TABLE {0} MODIFY column  {2} {3} COMMENT '{1}';", TableName, item.Label, item.ColumnName, typeName);
                    builder.AppendLine(sql);
                    descBuilder.AppendLine(str);
                }
            }
            else
            {
                var keyCol = colomns.Where(m => m.IsPrimaryKey == true).FirstOrDefault();
                if (keyCol == null)
                    return;
                   // throw new Exception("缺少主键！");
                builder.AppendLine(string.Format("CREATE TABLE {0} (", TableName));
                foreach (var item in colomns)
                {
                    string defs = string.Empty;
                    if (item.Default != null)
                    {
                        defs = " DEFAULT '" + item.Default + "'";
                    }
                    string len = "";
                    string typeName = item.TypeName;
                    GetDataType(item, ref typeName, ref len);
                    if (item.Default != null && item.Default == "'auto'")
                    {
                        builder.AppendLine(string.Format("{0}  NOT NULL AUTO_INCREMENT,", item.ColumnName));
                    }
                    else
                    {
                        builder.AppendLine(string.Format("{0}   {1}{2}  {3} null  {4} COMMENT  '{5}',", item.ColumnName, typeName, len, (item.IsNull ? "" : " not "), defs, item.Label == null ? "" : item.Label));
                    }
                }
                builder.AppendLine(string.Format("   PRIMARY KEY  (`{0}`)  USING BTREE", keyCol.ColumnName));
                builder.AppendLine(") ENGINE = InnoDB AUTO_INCREMENT = 1 ROW_FORMAT = Dynamic;");
            }
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
                conn.Close();
            }
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
                var keyField = m_DataBaseKyFieldTableDic[filters.TableName];
                if (string.IsNullOrEmpty(keyField))
                    keyField = "ID";
                strSql = "select " + fields + " from " + filters.TableName +
                          " where " + keyField + " Not In ( select " + keyField + " from " + filters.TableName + " where " + whereStr + orderBy + " limit 0," + index + " ) and  ( " + whereStr + " ) " + orderBy + " limit 0," + filters.PageSize;
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
                result.Result.TableName = filters.TableName;
            }
            result.TotalCount = count;
            conn.Close();
            return result;

        }

        internal override string GetQueryFields(QueryPageFilter filters)
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
                    builder.Append( "astext(" +filters.ReturnFieldNames[i] + ")  " + filters.ReturnFieldNames[i]);
                else
                    builder.Append(filters.ReturnFieldNames[0]);
            }
            return builder.ToString();
        }

        internal override string GetFieldFromTable(String tableName)
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
                        builder.Append("astext(" + col.ColumnName + ")  " + col.ColumnName);
                    else
                        builder.Append(col.ColumnName);
                }
            }
            if (builder.Length > 0)
                return builder.ToString();
            return " * ";
        }

        internal virtual string GetSqlSign(SpatialQueryFilter filter)
        {
            var str = string.Format("{1}({0},geomFromText('{2}'))={3}", filter.FieldName, GetSign(filter.Sign), filter.Geometry.WKT, filter.IsTrue);
            return str;
        }

        private void GetDataType(Column item, ref string typeName, ref string len)
        {
            typeName = item.TypeName.ToLower();
            if (item.Precision == 0) len = "";
            else if (item.Precision == -1) len = "";
            else len = string.Format("({0})", item.Precision);
            switch (item.TypeName.ToLower())
            {
                case "float":
                case "decimal":
                    typeName = "decimal";
                    len = "(10,2)";
                    break;
                case "datetime":
                case "date":
                case "datetime2":
                    typeName = "date";
                    len = "";
                    break;
                case "nvarchar":
                    typeName = "varchar";
                    break;
            }
            if (typeName == "varchar" && string.IsNullOrEmpty(len))
                len = "(50)";
        }


        internal string GetSign(SpatialSign sign)
        {
            var str = "ST_Contains";
            switch (sign)
            {
                case SpatialSign.STContains:
                    str = "ST_Contains";
                    break;
                case SpatialSign.STCrosses:
                    str = "ST_Crosses";
                    break;
                case SpatialSign.STDisjoint:
                    str = "ST_Disjoint";
                    break;
                case SpatialSign.STEquals:
                    str = "ST_Equals";
                    break;
                case SpatialSign.STOverlaps:
                    str = "ST_Overlaps";
                    break;
                case SpatialSign.STIntersects:
                    str = "ST_Intersects";
                    break;
                case SpatialSign.STTouches:
                    str = "ST_Touches";
                    break;
                case SpatialSign.STWithin:
                    str = "ST_Within";
                    break;
            }
            return str;
        }
        */
    }
}
