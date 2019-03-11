using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FD.DataBase
{
   public class SqlHelperFactory
    {
       public static IDataClassHelper GetSqlDataClassHelper(SqlPrividerType privider,string constr ,string modelNameSpace)
       {
           IDataClassHelper classHelper = null; 
           switch (privider)
           {
               case SqlPrividerType.SqlClient:
                   classHelper = new SqlDataClassHelper(constr, modelNameSpace);
                   break;
               case SqlPrividerType.OracleClient:
                   classHelper = new OracleDataClassHelper(constr, modelNameSpace);
                   break;
               case SqlPrividerType.Sqlite:
                   classHelper = new SqliteDataClassHelper(constr, modelNameSpace);
                   break;
               case SqlPrividerType.MySqlClient:
                   classHelper = new MySqlDataClassHelper(constr, modelNameSpace);
                   break;
           }
           return classHelper;
       }

       public static IDataClassHelper GetSqlDataClassHelper(SqlPrividerType privider, string constr, string modelNameSpace, Dictionary<string, string> dataBaseKyFieldTableDic, Dictionary<string, string> tableToTableNameDic)
       {
           IDataClassHelper classHelper = null;
           switch (privider)
           {
               case SqlPrividerType.SqlClient:
                   classHelper = new SqlDataClassHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                   break;
               case SqlPrividerType.OracleClient:
                   classHelper = new OracleDataClassHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                   break;
               case SqlPrividerType.Sqlite:
                   classHelper = new SqliteDataClassHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
                   break;
               case SqlPrividerType.MySqlClient:
                   classHelper = new MySqlDataClassHelper(constr, modelNameSpace, dataBaseKyFieldTableDic, tableToTableNameDic);
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
