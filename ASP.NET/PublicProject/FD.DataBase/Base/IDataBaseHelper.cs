using System.Collections.Generic;

namespace FD.DataBase
{
    public interface IDataBaseHelper
    {
        /// <summary>
        /// 当前数据库连接信息
        /// </summary>
        string CurrConnectionString { get; }
        /// <summary>
        /// 数据库配置类型
        /// </summary>
        SqlPrividerType CurrPrividerType { get; }
        /// <summary>
        /// 转换模型的命名空间
        /// </summary>
        string ModelNameSpace { get; }
        /// <summary>
        /// 数据库表与主键对照表
        /// </summary>
        Dictionary<string, string> DateBaseKeyFieldDic { get; }
        /// <summary>
        /// 数据库表名与临时表名的对照表
        /// </summary>
        Dictionary<string, string> TableNameDic { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        //Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter);
        //T[] GetObjResult<T>(QueryPageFilter filter) where T : class, new();
        //T GetQueryObj<T>(QueryPageFilter filter) where T : class, new();
        ///// <summary>
        ///// 执行SQL语句
        ///// </summary>
        ///// <param name="sqlList"></param>
        //void ExecSqlListWithTrans(List<string> sqlList);
        //Dictionary<string, object>[] ExecuteSqlToDic(string sqlList);
        //void BatchInsert(string TableName, Dictionary<string, object>[] dicList);
        //Dictionary<string, object> GetQueryDic(QueryPageFilter filter);

    }
}
