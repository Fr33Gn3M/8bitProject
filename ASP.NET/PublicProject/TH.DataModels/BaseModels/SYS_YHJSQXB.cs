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
    public partial class SYS_YHJSQXB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_YHJSQXB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string yHBS;//
        protected string jSID;//
        protected string mS;//
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
        public string YHBS
        {
            set { yHBS = value; }
            get { return yHBS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string JSID
        {
            set { jSID = value; }
            get { return jSID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MS
        {
            set { mS = value; }
            get { return mS; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_YHJSQXBTableName  = "SYS_YHJSQXB";
        public const string  IDFieldName  = "ID";//
        public const string  YHBSFieldName  = "YHBS";//
        public const string  JSIDFieldName  = "JSID";//
        public const string  MSFieldName  = "MS";//
        #endregion


    }
}

