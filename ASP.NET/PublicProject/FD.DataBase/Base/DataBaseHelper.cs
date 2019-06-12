using System.Collections.Generic;

namespace FD.DataBase
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public DataBaseHelper(string connstr, string modelNameSpace, SqlPrividerType prividerType)
        {
            CurrPrividerType = prividerType;
            CurrConnectionString = connstr;
            ModelNameSpace = modelNameSpace;
            GetSqlTableInfo();
        }

        public DataBaseHelper(string connstr, string modelNameSpace, SqlPrividerType prividerType, Dictionary<string, string> dataBaseKeyFieldDic, Dictionary<string, string> tableNameDic)
        {
            CurrPrividerType = prividerType;
            CurrConnectionString = connstr;
            DateBaseKeyFieldDic = dataBaseKeyFieldDic;
            TableNameDic = tableNameDic;
            ModelNameSpace = modelNameSpace;
        }

        public SqlPrividerType CurrPrividerType { get; internal set; }
        public string CurrConnectionString { get; internal set; }
        public string ModelNameSpace { get; internal set; }
        public Dictionary<string, string> DateBaseKeyFieldDic { get; internal set; }  //DataBaseKyFieldTableDic
        public Dictionary<string, string> TableNameDic { get; internal set; }     //TableToTableNameDic

        internal virtual void GetSqlTableInfo()
        {

        }



        //public Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter)
        //{
        //    string tableName1 = filter.TableName;
        //    if (TableNameDic != null && TableNameDic.ContainsKey(filter.TableName))
        //        tableName1 = TableNameDic[filter.TableName];
        //    if (!DateBaseKeyFieldDic.ContainsKey(tableName1))
        //        throw new Exception("找不到目标表或视图！请尝试重启服务端！");
        //    filter.TableName = tableName1;
        //    var result = GetQueryResultFromDB(filter);
        //    var queryResult = new QueryFilterResult();
        //    queryResult.TotalCount = result.TotalCount;
        //    queryResult.TableName = filter.TableName;
        //    if (filter.IsReturnCount != true)
        //    {
        //        if (result.Result != null)
        //        {
        //            var dic = new Dictionary<string, object>[result.Result.Rows.Count];
        //            int index = 0;
        //            foreach (DataRow row in result.Result.Rows)
        //            {
        //                var dd = ConvertDataRow(row, true);
        //                dic[index] = dd;
        //                index++;
        //            }
        //            return dic;
        //        }
        //    }
        //    return null;
        //}
    }
}
