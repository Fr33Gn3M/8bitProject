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
    public partial class ZT_YYTCXRB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ZT_YYTCXRB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string xRLX;//
        protected string xRQ;//
        protected int? tMD;//
        protected string zJ;//
        protected string yYCJ;//
        protected string xXK;//
        protected string sSYYID;//
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
        public string XRLX
        {
            set { xRLX = value; }
            get { return xRLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XRQ
        {
            set { xRQ = value; }
            get { return xRQ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? TMD
        {
            set { tMD = value; }
            get { return tMD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string ZJ
        {
            set { zJ = value; }
            get { return zJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string YYCJ
        {
            set { yYCJ = value; }
            get { return yYCJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XXK
        {
            set { xXK = value; }
            get { return xXK; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSYYID
        {
            set { sSYYID = value; }
            get { return sSYYID; }
        }
        #endregion


        #region 字段名称

        public const string  ZT_YYTCXRBTableName  = "ZT_YYTCXRB";
        public const string  IDFieldName  = "ID";//
        public const string  XRLXFieldName  = "XRLX";//
        public const string  XRQFieldName  = "XRQ";//
        public const string  TMDFieldName  = "TMD";//
        public const string  ZJFieldName  = "ZJ";//
        public const string  YYCJFieldName  = "YYCJ";//
        public const string  XXKFieldName  = "XXK";//
        public const string  SSYYIDFieldName  = "SSYYID";//
        #endregion


    }
}

