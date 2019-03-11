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
    public partial class VWSJ_SJLCB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VWSJ_SJLCB()
        { 
        }

        #region 受保护的字段
        protected DateTime? cJSJ;//
        protected string cLRID;//
        protected string iD;//
        protected string jDID;//
        protected string sJZT;//
        protected int? xH;//
        protected string lXMC;//
        protected string jJCDMS;//
        protected string mS;//
        protected double? x;//
        protected double? y;//
        protected string tPLJ;//
        protected string cLRSSBM;//
        protected string cLRXM;//
        protected int? dQLCXH;//
        protected string sJBH;//
        protected string sBRSSBM;//
        protected string sBRXM;//
        protected string dZ;//
        protected string cDLX;//
        protected DateTime? sBSJ;//
        protected string sBRSJH;//
        protected string sJID;//
        #endregion


        #region 共有属性
        /// <summary>
        ///  
        /// </summary>
        public DateTime? CJSJ
        {
            set { cJSJ = value; }
            get { return cJSJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string CLRID
        {
            set { cLRID = value; }
            get { return cLRID; }
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
        public string JDID
        {
            set { jDID = value; }
            get { return jDID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SJZT
        {
            set { sJZT = value; }
            get { return sJZT; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? XH
        {
            set { xH = value; }
            get { return xH; }
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
        public string JJCDMS
        {
            set { jJCDMS = value; }
            get { return jJCDMS; }
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
        public double? X
        {
            set { x = value; }
            get { return x; }
        }
        /// <summary>
        ///  
        /// </summary>
        public double? Y
        {
            set { y = value; }
            get { return y; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string TPLJ
        {
            set { tPLJ = value; }
            get { return tPLJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string CLRSSBM
        {
            set { cLRSSBM = value; }
            get { return cLRSSBM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string CLRXM
        {
            set { cLRXM = value; }
            get { return cLRXM; }
        }
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
        public string SJBH
        {
            set { sJBH = value; }
            get { return sJBH; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SBRSSBM
        {
            set { sBRSSBM = value; }
            get { return sBRSSBM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SBRXM
        {
            set { sBRXM = value; }
            get { return sBRXM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string DZ
        {
            set { dZ = value; }
            get { return dZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string CDLX
        {
            set { cDLX = value; }
            get { return cDLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? SBSJ
        {
            set { sBSJ = value; }
            get { return sBSJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SBRSJH
        {
            set { sBRSJH = value; }
            get { return sBRSJH; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SJID
        {
            set { sJID = value; }
            get { return sJID; }
        }
        #endregion


        #region 字段名称

        public const string  VWSJ_SJLCBTableName  = "VWSJ_SJLCB";
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  CLRIDFieldName  = "CLRID";//
        public const string  IDFieldName  = "ID";//
        public const string  JDIDFieldName  = "JDID";//
        public const string  SJZTFieldName  = "SJZT";//
        public const string  XHFieldName  = "XH";//
        public const string  LXMCFieldName  = "LXMC";//
        public const string  JJCDMSFieldName  = "JJCDMS";//
        public const string  MSFieldName  = "MS";//
        public const string  XFieldName  = "X";//
        public const string  YFieldName  = "Y";//
        public const string  TPLJFieldName  = "TPLJ";//
        public const string  CLRSSBMFieldName  = "CLRSSBM";//
        public const string  CLRXMFieldName  = "CLRXM";//
        public const string  DQLCXHFieldName  = "DQLCXH";//
        public const string  SJBHFieldName  = "SJBH";//
        public const string  SBRSSBMFieldName  = "SBRSSBM";//
        public const string  SBRXMFieldName  = "SBRXM";//
        public const string  DZFieldName  = "DZ";//
        public const string  CDLXFieldName  = "CDLX";//
        public const string  SBSJFieldName  = "SBSJ";//
        public const string  SBRSJHFieldName  = "SBRSJH";//
        public const string  SJIDFieldName  = "SJID";//
        #endregion


    }
}

