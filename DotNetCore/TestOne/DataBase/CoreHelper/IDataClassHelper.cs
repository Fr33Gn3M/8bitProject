using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
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
        Dictionary<string, string> TableTypeDic { get; }
        Dictionary<string, List<Column>> TableToTableNameFields { get; }
        Dictionary<string, Dictionary<string, string>> DicTableToTableNameFields { get; }

        Dictionary<string, object>[] GetQueryResultN(QueryPageFilter filter);
    }
}
