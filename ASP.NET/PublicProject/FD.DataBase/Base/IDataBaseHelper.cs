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

        Dictionary<string, object>[] GetQueryResult(QueryPageFilter filter, ref int count);

    }
}
