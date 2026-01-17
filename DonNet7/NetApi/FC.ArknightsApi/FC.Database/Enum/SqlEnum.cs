using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.Enum
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum SqlType
    {
        MySql,
        Oracle,
        SqlServer
    }

    /// <summary>
    /// sql简单操作符
    /// </summary>
    public enum SqlOperator
    {
        IsNuLL,
        IsNotNuLL,
        Like,// like
        NotLike,// not like
        LeftLike,// like
        RightLike,// like
        Equal,// =
        NoEqual,// <>
        MoreThan,// >
        LessThan,// <
        MoreEqualThan,// >=
        LessEqualThan,// <=
        In,//包含
        NotIn,//不包含,
        Exists,
        NotExists,
        EqualField
    }

    /// <summary>
    /// sql order by关键字
    /// </summary>
    public enum SqlOrderBy
    {
        Asc,
        Desc
    }

    /// <summary>
    /// sql逻辑运算符
    /// </summary>
    public enum SqlLogicOperator
    {
        And,
        Or
    }
}
