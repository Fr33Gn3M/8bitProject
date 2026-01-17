using System.Collections.Generic;

namespace FD.DataBase
{
    public class MySqlDBHelper : DataBaseHelper
    {
        public MySqlDBHelper(string connstr, string modelNameSpace)
           : base(connstr, modelNameSpace, SqlPrividerType.SqlClient)
        {
        }

        public MySqlDBHelper(string connstr, string modelNameSpace, Dictionary<string, string> dataBaseKeyFieldDic, Dictionary<string, string> tableNameDic)
            : base(connstr, modelNameSpace, SqlPrividerType.SqlClient, dataBaseKeyFieldDic, tableNameDic)
        {
        }
    }
}
