/*---------------------------------------------------------------------------------
-----------------该段代码由代码生成器自动生成-----------------------
-----------------作者：天海图汇------------------------------------------------------
-----------------联系作者：hzm@skyseainfotech.com----------------------------------
-----------------时间：2019/3/8 11:34:40------------------------------------------
----------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.DataModels
{
    public partial class VWSJ_SJLCJDB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VWSJ_SJLCJDB()
        { 
        }

        #region 受保护的字段
        protected int? dQLCXH;//
        protected string iD;//
        protected int? jBS;//
        protected string lX;//
        protected string sJID;//
        protected string zHCLRID;//
        protected string xPR;//
        protected string xPRBM;//
        protected string xPRXM;//
        protected string xPBMJC;//
        #endregion


        #region 共有属性
        /// <summary>
        ///  
        /// </summary>
        public int? DQLCXH
        {
            set { dQLCXH = value; }
            get { return dQLCXH; }
        }
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
        public int? JBS
        {
            set { jBS = value; }
            get { return jBS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string LX
        {
            set { lX = value; }
            get { return lX; }
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
        public string ZHCLRID
        {
            set { zHCLRID = value; }
            get { return zHCLRID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XPR
        {
            set { xPR = value; }
            get { return xPR; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XPRBM
        {
            set { xPRBM = value; }
            get { return xPRBM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XPRXM
        {
            set { xPRXM = value; }
            get { return xPRXM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XPBMJC
        {
            set { xPBMJC = value; }
            get { return xPBMJC; }
        }
        #endregion


        #region 字段名称

        public const string  VWSJ_SJLCJDBTableName  = "VWSJ_SJLCJDB";
        public const string  DQLCXHFieldName  = "DQLCXH";//
        public const string  IDFieldName  = "ID";//
        public const string  JBSFieldName  = "JBS";//
        public const string  LXFieldName  = "LX";//
        public const string  SJIDFieldName  = "SJID";//
        public const string  ZHCLRIDFieldName  = "ZHCLRID";//
        public const string  XPRFieldName  = "XPR";//
        public const string  XPRBMFieldName  = "XPRBM";//
        public const string  XPRXMFieldName  = "XPRXM";//
        public const string  XPBMJCFieldName  = "XPBMJC";//
        #endregion


    }
}

