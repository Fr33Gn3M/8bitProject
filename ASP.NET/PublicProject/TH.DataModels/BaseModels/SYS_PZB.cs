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

namespace TH.DataModels
{
    public partial class SYS_PZB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_PZB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sXMC;//
        protected string sXZ;//
        protected string bZ;//
        protected bool? sFMR;//
        protected int? xH;//
        protected string lXMC;//
        protected string sJID;//
        protected string sSGS;//
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
        public string SXMC
        {
            set { sXMC = value; }
            get { return sXMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SXZ
        {
            set { sXZ = value; }
            get { return sXZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string BZ
        {
            set { bZ = value; }
            get { return bZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFMR
        {
            set { sFMR = value; }
            get { return sFMR; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? XH
        {
            set { xH = value; }
            get { return xH; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string LXMC
        {
            set { lXMC = value; }
            get { return lXMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SJID
        {
            set { sJID = value; }
            get { return sJID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSGS
        {
            set { sSGS = value; }
            get { return sSGS; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_PZBTableName  = "SYS_PZB";
        public const string  IDFieldName  = "ID";//
        public const string  SXMCFieldName  = "SXMC";//
        public const string  SXZFieldName  = "SXZ";//
        public const string  BZFieldName  = "BZ";//
        public const string  SFMRFieldName  = "SFMR";//
        public const string  XHFieldName  = "XH";//
        public const string  LXMCFieldName  = "LXMC";//
        public const string  SJIDFieldName  = "SJID";//
        public const string  SSGSFieldName  = "SSGS";//
        #endregion


    }
}

