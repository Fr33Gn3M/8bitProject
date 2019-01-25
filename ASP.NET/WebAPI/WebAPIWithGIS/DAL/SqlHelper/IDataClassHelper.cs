using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IDataClassHelper
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
        Dictionary<string, string> DataBaseKyFieldTableDic { get; }
        /// <summary>
        /// 数据库表名与临时表名的对照表
        /// </summary>
        Dictionary<string, string> TableToTableNameDic { get; }
        /// <summary>
        /// 所有字段信息
        /// </summary>
        Dictionary<string, List<Column>> TableToTableNameFields { get; }
        Dictionary<string, Dictionary<string, string>> DicTableToTableNameFields { get; }
        /// <summary>
        /// 数据库查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        QueryFilterResult GetQueryResult(QueryPageFilter filter);
        /// <summary>
        /// 数据组查询
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        QueryFilterResult[] GetQueryResults(QueryPageFilter[] filters);
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="sqlList">更新模型</param>
        /// <param name="isconvertPart">是否为部分类中的字段（为true时不会生成数据库字段查询）</param>
        void UpdateObjects(object[] sqlList, bool isconvertPart = false);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sqlList"></param>
        void ExecuteSqlList(List<string> sqlList);
        void ExecuteSqlListSQL(List<string> sqlList);
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids">标识ID列表</param>
        void DeleteObjects<T>(object[] Ids);
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="Ids">标识ID列表</param>
        void DeleteObjects(string tableName, object[] Ids);
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="filter"></param>
        void DeleteObjects(QueryPageFilter filter);
        /// <summary>
        /// SQL语句查询反回表
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        DataTable ExecuteSqlToDataTable(string sqlList);
        /// <summary>
        /// SQL语句查询反回表
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        Dictionary<string, object>[] ExecuteSqlToDic(string sqlList);
        /// <summary>
        /// 得到模型的类型
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Type GetArrayType(string tableName);
        /// <summary>
        /// 通过列得到SQL语句
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        List<string> GetSqlListFromDataRow(DataRow[] sqlList);
        /// <summary>
        /// 通过表得到SQL语句
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<string> GetSqlListFromDataTable(DataTable dt);

        List<string> GetSqlListFromModels(object[] sqlList, bool isconvertPart = false);

        List<string> GetTableNames();
        List<Column> GetColumnsFromTable(string tableName);
        QueryFilterResult GetQueryResult(SqlModel filter, params object[] objs);
        Dictionary<string, object>[] GetQueryStatResult(SqlModel filter, params object[] objs);
        Dictionary<string, object>[] GetQueryStatResultN(SqlModel filter, params object[] objs);
        /// <summary>
        /// 根据条件查询数据库返回字典数组
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns>字典数组结果集</returns>
        Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter);
        /// <summary>
        /// 根据条件查询数据库返回字典（唯一值，多用于根据ID查询）
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns>字典结果</returns>
        Dictionary<string, object> GetQueryDic(QueryPageFilter filter);
        /// <summary>
        /// 根据条件查询数据库返回字典数组
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="count">查询结果数</param>
        /// <returns>字典数组结果集</returns>
        Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter,ref int count);
        void UpdateObjects(string tableName, Dictionary<string, object>[] dicList, bool isconvertPart = false);
        void UpdateObjects(Dictionary<string, object> objs, QueryPageFilter filter);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        QueryFilterResultDic[] GetQueryFieldResults(QueryPageFilter[] filters);
        void UpdateDelObjects(QueryPageFilter filters, string fieldName);
        QueryFilterResultDic GetQueryResultDic(QueryPageFilter filter);
        void UpdateSystemTable(string TableName, List<Column> colomns);

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="dicList">数据列表</param>
        void BatchInsert(string TableName, Dictionary<string, object>[] dicList);

        void batchUpdata(string tablename, Dictionary<string, object>[] updateList);
    }
}
