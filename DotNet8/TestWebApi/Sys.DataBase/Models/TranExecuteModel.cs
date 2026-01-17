using System.Collections.Generic;

namespace Sys.DataBase.Models
{
    /// <summary>
    /// 数据库事务批量执行的模型
    /// 只有模型里的所有sql语句和要更新的表都没问题，才会提交事务
    /// </summary>
    public class TranExecuteModel
    {

        public TranExecuteModel()
        {
            SqlList = new List<string>();
            UpdateModelList = new List<object>();
            UpdateDicModel = new List<TranUpdateDicModel>();
        }

        /// <summary>
        /// 需要执行的sql语句数组（一般是删除语句或条件更新语句）
        /// </summary>
        public List<string> SqlList { get; set; }

        /// <summary>
        /// 需要更新的表数据模型
        /// </summary>
        public List<object> UpdateModelList { get; set; }

        /// <summary>
        /// 需要更新的表（包含表名和更新数据的字典列表）
        /// </summary>
        public List<TranUpdateDicModel> UpdateDicModel { get; set; }
    }

    public class TranUpdateDicModel
    {
        public string TableName { get; set; }

        public List<Dictionary<string, object>> UpdateList { get; set; }
    }
}
