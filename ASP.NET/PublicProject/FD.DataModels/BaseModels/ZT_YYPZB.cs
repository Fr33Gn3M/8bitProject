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
    public partial class ZT_YYPZB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ZT_YYPZB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sSYYID;//
        protected string sSXT;//
        protected string lBPZ;//
        protected string sTPZ;//
        protected string bJPZ;//
        protected string dTPZ;//
        protected string yYTB;//
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
        public string SSYYID
        {
            set { sSYYID = value; }
            get { return sSYYID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSXT
        {
            set { sSXT = value; }
            get { return sSXT; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string LBPZ
        {
            set { lBPZ = value; }
            get { return lBPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string STPZ
        {
            set { sTPZ = value; }
            get { return sTPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string BJPZ
        {
            set { bJPZ = value; }
            get { return bJPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string DTPZ
        {
            set { dTPZ = value; }
            get { return dTPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string YYTB
        {
            set { yYTB = value; }
            get { return yYTB; }
        }
        #endregion


        #region 字段名称

        public const string  ZT_YYPZBTableName  = "ZT_YYPZB";
        public const string  IDFieldName  = "ID";//
        public const string  SSYYIDFieldName  = "SSYYID";//
        public const string  SSXTFieldName  = "SSXT";//
        public const string  LBPZFieldName  = "LBPZ";//
        public const string  STPZFieldName  = "STPZ";//
        public const string  BJPZFieldName  = "BJPZ";//
        public const string  DTPZFieldName  = "DTPZ";//
        public const string  YYTBFieldName  = "YYTB";//
        #endregion


    }
}

