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
    public partial class SYS_SJZDB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_SJZDB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sSBM;//
        protected string zDMC;//
        protected string zDM;//
        protected string zDLX;//
        protected int? zDCD;//
        protected bool? sFKWK;//
        protected bool? sFWGJZ;//
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
        public string SSBM
        {
            set { sSBM = value; }
            get { return sSBM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string ZDMC
        {
            set { zDMC = value; }
            get { return zDMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string ZDM
        {
            set { zDM = value; }
            get { return zDM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string ZDLX
        {
            set { zDLX = value; }
            get { return zDLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? ZDCD
        {
            set { zDCD = value; }
            get { return zDCD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFKWK
        {
            set { sFKWK = value; }
            get { return sFKWK; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWGJZ
        {
            set { sFWGJZ = value; }
            get { return sFWGJZ; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_SJZDBTableName  = "SYS_SJZDB";
        public const string  IDFieldName  = "ID";//
        public const string  SSBMFieldName  = "SSBM";//
        public const string  ZDMCFieldName  = "ZDMC";//
        public const string  ZDMFieldName  = "ZDM";//
        public const string  ZDLXFieldName  = "ZDLX";//
        public const string  ZDCDFieldName  = "ZDCD";//
        public const string  SFKWKFieldName  = "SFKWK";//
        public const string  SFWGJZFieldName  = "SFWGJZ";//
        #endregion


    }
}

