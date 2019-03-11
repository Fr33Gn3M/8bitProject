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
    public partial class VWYYMBZDB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VWYYMBZDB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string bZ;//
        protected string sXMC;//
        protected string mBZD;//
        protected string lXMC;//
        protected int? xH;//
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
        public string BZ
        {
            set { bZ = value; }
            get { return bZ; }
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
        public string MBZD
        {
            set { mBZD = value; }
            get { return mBZD; }
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
        public int? XH
        {
            set { xH = value; }
            get { return xH; }
        }
        #endregion


        #region 字段名称

        public const string  VWYYMBZDBTableName  = "VWYYMBZDB";
        public const string  IDFieldName  = "ID";//
        public const string  BZFieldName  = "BZ";//
        public const string  SXMCFieldName  = "SXMC";//
        public const string  MBZDFieldName  = "MBZD";//
        public const string  LXMCFieldName  = "LXMC";//
        public const string  XHFieldName  = "XH";//
        #endregion


    }
}

