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
    public partial class SYS_DLFWB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_DLFWB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string fWMC;//
        protected string fWBT;//
        protected string sSFWFZ;//
        protected string qQFS;//
        protected string qQCS;//
        protected string fWDZ;//
        protected string mS;//
        protected string fHSJLX;//
        protected bool? sFRZ;//
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
        public string FWMC
        {
            set { fWMC = value; }
            get { return fWMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string FWBT
        {
            set { fWBT = value; }
            get { return fWBT; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSFWFZ
        {
            set { sSFWFZ = value; }
            get { return sSFWFZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string QQFS
        {
            set { qQFS = value; }
            get { return qQFS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string QQCS
        {
            set { qQCS = value; }
            get { return qQCS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string FWDZ
        {
            set { fWDZ = value; }
            get { return fWDZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MS
        {
            set { mS = value; }
            get { return mS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string FHSJLX
        {
            set { fHSJLX = value; }
            get { return fHSJLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFRZ
        {
            set { sFRZ = value; }
            get { return sFRZ; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_DLFWBTableName  = "SYS_DLFWB";
        public const string  IDFieldName  = "ID";//
        public const string  FWMCFieldName  = "FWMC";//
        public const string  FWBTFieldName  = "FWBT";//
        public const string  SSFWFZFieldName  = "SSFWFZ";//
        public const string  QQFSFieldName  = "QQFS";//
        public const string  QQCSFieldName  = "QQCS";//
        public const string  FWDZFieldName  = "FWDZ";//
        public const string  MSFieldName  = "MS";//
        public const string  FHSJLXFieldName  = "FHSJLX";//
        public const string  SFRZFieldName  = "SFRZ";//
        #endregion


    }
}

