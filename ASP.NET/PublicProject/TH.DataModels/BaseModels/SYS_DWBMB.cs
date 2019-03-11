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
    public partial class SYS_DWBMB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_DWBMB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sJBMID;//
        protected string bMJC;//
        protected string bMMC;//
        protected string bZ;//
        protected DateTime? cJSJ;//
        protected int? jBS;//
        protected int? xH;//
        protected bool? sFSC;//
        protected bool? sFSGS;//
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
        public string SJBMID
        {
            set { sJBMID = value; }
            get { return sJBMID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string BMJC
        {
            set { bMJC = value; }
            get { return bMJC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string BMMC
        {
            set { bMMC = value; }
            get { return bMMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string BZ
        {
            set { bZ = value; }
            get { return bZ; }
        }
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
        public int? JBS
        {
            set { jBS = value; }
            get { return jBS; }
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
        public bool? SFSC
        {
            set { sFSC = value; }
            get { return sFSC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFSGS
        {
            set { sFSGS = value; }
            get { return sFSGS; }
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

        public const string  SYS_DWBMBTableName  = "SYS_DWBMB";
        public const string  IDFieldName  = "ID";//
        public const string  SJBMIDFieldName  = "SJBMID";//
        public const string  BMJCFieldName  = "BMJC";//
        public const string  BMMCFieldName  = "BMMC";//
        public const string  BZFieldName  = "BZ";//
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  JBSFieldName  = "JBS";//
        public const string  XHFieldName  = "XH";//
        public const string  SFSCFieldName  = "SFSC";//
        public const string  SFSGSFieldName  = "SFSGS";//
        public const string  SSGSFieldName  = "SSGS";//
        #endregion


    }
}

