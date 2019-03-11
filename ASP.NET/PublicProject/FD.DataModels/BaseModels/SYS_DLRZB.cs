/*---------------------------------------------------------------------------------
-----------------该段代码由代码生成器自动生成-----------------------
-----------------作者：天海图汇------------------------------------------------------
-----------------联系作者：hzm@skyseainfotech.com----------------------------------
-----------------时间：2018/5/15 10:00:49------------------------------------------
----------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FD.DataModels
{
    public partial class SYS_DLRZB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_DLRZB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string yHID;//
        protected DateTime? dLSJ;//
        protected string iP;//
        protected string sBID;//
        protected int? dKH;//
        protected DateTime? tCSJ;//
        protected bool? sFZX;//
        protected string sBLX;//
        #endregion


        #region 共有属性
        /// <summary>
        ///  
        /// </summary>
        public string ID
        {
            set { iD = value; }
            get { return iD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string YHID
        {
            set { yHID = value; }
            get { return yHID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? DLSJ
        {
            set { dLSJ = value; }
            get { return dLSJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string IP
        {
            set { iP = value; }
            get { return iP; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SBID
        {
            set { sBID = value; }
            get { return sBID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? DKH
        {
            set { dKH = value; }
            get { return dKH; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? TCSJ
        {
            set { tCSJ = value; }
            get { return tCSJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFZX
        {
            set { sFZX = value; }
            get { return sFZX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SBLX
        {
            set { sBLX = value; }
            get { return sBLX; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_DLRZBTableName  = "SYS_DLRZB";
        public const string  IDFieldName  = "ID";//
        public const string  YHIDFieldName  = "YHID";//
        public const string  DLSJFieldName  = "DLSJ";//
        public const string  IPFieldName  = "IP";//
        public const string  SBIDFieldName  = "SBID";//
        public const string  DKHFieldName  = "DKH";//
        public const string  TCSJFieldName  = "TCSJ";//
        public const string  SFZXFieldName  = "SFZX";//
        public const string  SBLXFieldName  = "SBLX";//
        #endregion


    }
}

