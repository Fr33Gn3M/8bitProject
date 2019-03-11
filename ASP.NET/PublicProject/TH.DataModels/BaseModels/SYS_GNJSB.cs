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
    public partial class SYS_GNJSB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_GNJSB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string jSMC;//
        protected string jSMS;//
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
        public string JSMC
        {
            set { jSMC = value; }
            get { return jSMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string JSMS
        {
            set { jSMS = value; }
            get { return jSMS; }
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

        public const string  SYS_GNJSBTableName  = "SYS_GNJSB";
        public const string  IDFieldName  = "ID";//
        public const string  JSMCFieldName  = "JSMC";//
        public const string  JSMSFieldName  = "JSMS";//
        public const string  SSGSFieldName  = "SSGS";//
        #endregion


    }
}

