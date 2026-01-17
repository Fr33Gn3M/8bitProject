using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.Model
{
    public class DbFieldInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 字段中文注释
        /// </summary>
        public string NameCN { get; internal set; }

        /// <summary>
        /// 字段类型的DbType映射
        /// </summary>
        public DbType DbType { get; internal set; }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; internal set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; internal set; }

        /// <summary>
        /// 字符最大长度
        /// </summary>
        public int MaxLength { get; internal set; }

        /// <summary>
        /// 数据库查询出来的字段DataType转DbType
        /// </summary>
        /// <param name="dataType">数据库查询出来的字段DataType</param>
        /// <returns>DbType</returns>
        public DbType DataTypeTransform(string dataType)
        {
            switch(dataType)
            {
                case "id": return DbType.UInt32;
                case "varchar": return DbType.String;
                default:return DbType.String;
            }
        }
    }
}
