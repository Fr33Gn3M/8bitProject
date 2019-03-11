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
    public partial class SYS_JSGNB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_JSGNB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string jSID;//
        protected string gNID;//
        protected string mS;//
        protected string xTMC;//
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
        public string JSID
        {
            set { jSID = value; }
            get { return jSID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GNID
        {
            set { gNID = value; }
            get { return gNID; }
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
        public string XTMC
        {
            set { xTMC = value; }
            get { return xTMC; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_JSGNBTableName  = "SYS_JSGNB";
        public const string  IDFieldName  = "ID";//
        public const string  JSIDFieldName  = "JSID";//
        public const string  GNIDFieldName  = "GNID";//
        public const string  MSFieldName  = "MS";//
        public const string  XTMCFieldName  = "XTMC";//
        #endregion


    }
}

