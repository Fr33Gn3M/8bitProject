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
    public partial class SYS_DBMBPZB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_DBMBPZB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string aPPLBPZ;//
        protected string aPPBJPZ;//
        protected string pCLBPZ;//
        protected string pCBJPZ;//
        protected string sSBM;//
        protected string sSZT;//
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
        public string APPLBPZ
        {
            set { aPPLBPZ = value; }
            get { return aPPLBPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string APPBJPZ
        {
            set { aPPBJPZ = value; }
            get { return aPPBJPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string PCLBPZ
        {
            set { pCLBPZ = value; }
            get { return pCLBPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string PCBJPZ
        {
            set { pCBJPZ = value; }
            get { return pCBJPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSBM
        {
            set { sSBM = value; }
            get { return sSBM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSZT
        {
            set { sSZT = value; }
            get { return sSZT; }
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

        public const string  SYS_DBMBPZBTableName  = "SYS_DBMBPZB";
        public const string  IDFieldName  = "ID";//
        public const string  APPLBPZFieldName  = "APPLBPZ";//
        public const string  APPBJPZFieldName  = "APPBJPZ";//
        public const string  PCLBPZFieldName  = "PCLBPZ";//
        public const string  PCBJPZFieldName  = "PCBJPZ";//
        public const string  SSBMFieldName  = "SSBM";//
        public const string  SSZTFieldName  = "SSZT";//
        public const string  SSGSFieldName  = "SSGS";//
        #endregion


    }
}

