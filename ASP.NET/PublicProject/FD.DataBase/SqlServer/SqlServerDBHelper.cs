using System.Collections.Generic;

namespace FD.DataBase
{
    public class SqlServerDBHelper : DataBaseHelper
    {
        public SqlServerDBHelper(string connstr, string modelNameSpace)
           : base(connstr, modelNameSpace, SqlPrividerType.SqlClient)
        {
        }

        public SqlServerDBHelper(string connstr, string modelNameSpace, Dictionary<string, string> dataBaseKeyFieldDic, Dictionary<string, string> tableNameDic)
            : base(connstr, modelNameSpace, SqlPrividerType.SqlClient, dataBaseKeyFieldDic, tableNameDic)
        {
        }
    }
}
