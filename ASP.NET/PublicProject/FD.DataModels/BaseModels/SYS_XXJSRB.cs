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
    public partial class SYS_XXJSRB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_XXJSRB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sSXXID;//
        protected string jSRID;//
        protected bool? sFYD;//
        protected bool? sFSC;//
        protected DateTime? jSSJ;//
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
        public string SSXXID
        {
            set { sSXXID = value; }
            get { return sSXXID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string JSRID
        {
            set { jSRID = value; }
            get { return jSRID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFYD
        {
            set { sFYD = value; }
            get { return sFYD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFSC
        {
            set { sFSC = value; }
            get { return sFSC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? JSSJ
        {
            set { jSSJ = value; }
            get { return jSSJ; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_XXJSRBTableName  = "SYS_XXJSRB";
        public const string  IDFieldName  = "ID";//
        public const string  SSXXIDFieldName  = "SSXXID";//
        public const string  JSRIDFieldName  = "JSRID";//
        public const string  SFYDFieldName  = "SFYD";//
        public const string  SFSCFieldName  = "SFSC";//
        public const string  JSSJFieldName  = "JSSJ";//
        #endregion


    }
}

