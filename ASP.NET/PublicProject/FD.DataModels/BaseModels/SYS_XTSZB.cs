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
    public partial class SYS_XTSZB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_XTSZB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sXM;//
        protected string sXMC;//
        protected string sXZ;//
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
        public string SXM
        {
            set { sXM = value; }
            get { return sXM; }
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
        #endregion


        #region 字段名称

        public const string  SYS_XTSZBTableName  = "SYS_XTSZB";
        public const string  IDFieldName  = "ID";//
        public const string  SXMFieldName  = "SXM";//
        public const string  SXMCFieldName  = "SXMC";//
        public const string  SXZFieldName  = "SXZ";//
        #endregion


    }
}

