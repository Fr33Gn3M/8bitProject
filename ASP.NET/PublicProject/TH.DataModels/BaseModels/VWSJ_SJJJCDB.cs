/*---------------------------------------------------------------------------------
-----------------该段代码由代码生成器自动生成-----------------------
-----------------作者：天海图汇------------------------------------------------------
-----------------联系作者：hzm@skyseainfotech.com----------------------------------
-----------------时间：2019/3/8 11:58:13------------------------------------------
----------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.DataModels
{
    public partial class VWSJ_SJJJCDB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VWSJ_SJJJCDB()
        { 
        }

        #region 受保护的字段
        protected string cDLX;//
        protected string jJCDMS;//
        protected bool? jJCDSFSC;//
        protected int? wCLTXSJJG;//
        protected int? zDCLSJJG;//
        protected string dZ;//
        protected string iD;//
        protected DateTime? jSSJ;//
        protected string mS;//
        protected string sBBMID;//
        protected string sBRID;//
        protected DateTime? sBSJ;//
        protected bool? sFSC;//
        protected bool? sFWJ;//
        protected string sJBH;//
        protected string sJJJCDID;//
        protected string sJLXID;//
        protected string sJLY;//
        protected string tPLJ;//
        protected double? x;//
        protected double? y;//
        protected DateTime? zHCLSJQX;//
        #endregion


        #region 共有属性
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
        public string JJCDMS
        {
            set { jJCDMS = value; }
            get { return jJCDMS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? JJCDSFSC
        {
            set { jJCDSFSC = value; }
            get { return jJCDSFSC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? WCLTXSJJG
        {
            set { wCLTXSJJG = value; }
            get { return wCLTXSJJG; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? ZDCLSJJG
        {
            set { zDCLSJJG = value; }
            get { return zDCLSJJG; }
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
        public string ID
        {
            set { iD = value; }
            get { return iD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? JSSJ
        {
            set { jSSJ = value; }
            get { return jSSJ; }
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
        public string SBBMID
        {
            set { sBBMID = value; }
            get { return sBBMID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SBRID
        {
            set { sBRID = value; }
            get { return sBRID; }
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
        public bool? SFSC
        {
            set { sFSC = value; }
            get { return sFSC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWJ
        {
            set { sFWJ = value; }
            get { return sFWJ; }
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
        public string SJJJCDID
        {
            set { sJJJCDID = value; }
            get { return sJJJCDID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SJLXID
        {
            set { sJLXID = value; }
            get { return sJLXID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SJLY
        {
            set { sJLY = value; }
            get { return sJLY; }
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
        public DateTime? ZHCLSJQX
        {
            set { zHCLSJQX = value; }
            get { return zHCLSJQX; }
        }
        #endregion


        #region 字段名称

        public const string  VWSJ_SJJJCDBTableName  = "VWSJ_SJJJCDB";
        public const string  CDLXFieldName  = "CDLX";//
        public const string  JJCDMSFieldName  = "JJCDMS";//
        public const string  JJCDSFSCFieldName  = "JJCDSFSC";//
        public const string  WCLTXSJJGFieldName  = "WCLTXSJJG";//
        public const string  ZDCLSJJGFieldName  = "ZDCLSJJG";//
        public const string  DZFieldName  = "DZ";//
        public const string  IDFieldName  = "ID";//
        public const string  JSSJFieldName  = "JSSJ";//
        public const string  MSFieldName  = "MS";//
        public const string  SBBMIDFieldName  = "SBBMID";//
        public const string  SBRIDFieldName  = "SBRID";//
        public const string  SBSJFieldName  = "SBSJ";//
        public const string  SFSCFieldName  = "SFSC";//
        public const string  SFWJFieldName  = "SFWJ";//
        public const string  SJBHFieldName  = "SJBH";//
        public const string  SJJJCDIDFieldName  = "SJJJCDID";//
        public const string  SJLXIDFieldName  = "SJLXID";//
        public const string  SJLYFieldName  = "SJLY";//
        public const string  TPLJFieldName  = "TPLJ";//
        public const string  XFieldName  = "X";//
        public const string  YFieldName  = "Y";//
        public const string  ZHCLSJQXFieldName  = "ZHCLSJQX";//
        #endregion


    }
}

