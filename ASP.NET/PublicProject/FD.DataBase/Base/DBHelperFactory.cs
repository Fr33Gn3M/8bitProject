using System.Collections.Generic;

namespace FD.DataBase
{
    public class DBHelperFactory
    {
        public static IDataBaseHelper GetSqlDataClassHelper(SqlPrividerType privider, string constr, string modelNameSpace)
        {
            IDataBaseHelper classHelper = null;
            switch (privider)
            {
                case SqlPrividerType.SqlClient:
                    classHelper = new SqlServerDBHelper(constr, modelNameSpace);
                    break;
                case SqlPrividerType.OracleClient:
                    classHelper = new OracleDBHelper(constr, modelNameSpace);
                    break;
                case SqlPrividerType.Sqlite:
                    classHelper = new SQLiteDBHelper(constr, modelNameSpace);
                    break;
                case SqlPrividerType.MySqlClient:
                    classHelper = new MySqlDBHelper(constr, modelNameSpace);
                    break;
            }
            return classHelper;
        }

        public static IDataBaseHelper GetSqlDataClassHelper(SqlPrividerType privider, string constr, string modelNameSpace, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
        {
            IDataBaseHelper classHelper = null;
            switch (privider)
            {
                case SqlPrividerType.SqlClient:
                    classHelper = new SqlServerDBHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                    break;
                case SqlPrividerType.OracleClient:
                    classHelper = new OracleDBHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                    break;
                case SqlPrividerType.Sqlite:
                    classHelper = new SQLiteDBHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                    break;
                case SqlPrividerType.MySqlClient:
                    classHelper = new MySqlDBHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                    break;
            }
            return classHelper;
        }

        public static string GetSqlPrividerTypeName(SqlPrividerType privider)
        {
            string str = "";
            switch (privider)
            {
                case SqlPrividerType.OracleClient:
                    str = "System.Data.OracleClient";
                    break;
                case SqlPrividerType.SqlClient:
                    str = "System.Data.SqlClient";
                    break;
                case SqlPrividerType.Sqlite:
                    str = "System.Data.SQLite";
                    break;
                case SqlPrividerType.MySqlClient:
                    str = "MySql.Data.MySqlClient";
                    break;
            }
            return str;
        }
    }
}
