using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FD.HistoryDataManager
{
    public class HistoryDiffModel
    {
        public string XGR { get; set; }                          //修改人
        public string BM { get; set; }                           //部门
        public string GXSJ { get; set; }                         //更新时间
        public string ZBM { get; set; }                          //主表名
        public Dictionary<string, object> XGZD { get; set; }     //修改字段
        public List<SubTableDiffModel> CBCYList;                 //从表操作List
    }
    public class SubTableDiffModel
    {
        public string XSBM { get; set; }                         //显示从表表名
        public List<SubTableFieldDiffModel> CBZDCYList;          //从表操作List
    }
    public class SubTableFieldDiffModel
    {
        public string CAOZUO { get; set; }                       //操作（新增，编辑，删除）
        public string LX { get; set; }                           //类型（类似联系人负责人）
        public Dictionary<string, object> XGZD { get; set; }     //修改字段
    }
}
