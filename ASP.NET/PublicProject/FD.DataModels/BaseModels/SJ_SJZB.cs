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

namespace FD.DataModels
{
    public partial class SJ_SJZB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SJ_SJZB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sJLY;//
        protected string sJBH;//
        protected string sJLXID;//
        protected string dZ;//
        protected double? x;//
        protected double? y;//
        protected string mS;//
        protected string tPLJ;//
        protected string sBRID;//
        protected DateTime? sBSJ=DateTime.Now;//
        protected DateTime? jSSJ;//
        protected bool? sFWJ=false;//
        protected string sJJJCDID;//
        protected DateTime? zHCLSJQX;//
        protected bool? sFSC=false;//
        protected string sBBMID;//
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
        public string SJLY
        {
            set { sJLY = value; }
            get { return sJLY; }
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
        public string SJLXID
        {
            set { sJLXID = value; }
            get { return sJLXID; }
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
        public string MS
        {
            set { mS = value; }
            get { return mS; }
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
        public DateTime? JSSJ
        {
            set { jSSJ = value; }
            get { return jSSJ; }
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
        public string SJJJCDID
        {
            set { sJJJCDID = value; }
            get { return sJJJCDID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? ZHCLSJQX
        {
            set { zHCLSJQX = value; }
            get { return zHCLSJQX; }
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
        public string SBBMID
        {
            set { sBBMID = value; }
            get { return sBBMID; }
        }
        #endregion


        #region 字段名称

        public const string  SJ_SJZBTableName  = "SJ_SJZB";
        public const string  IDFieldName  = "ID";//
        public const string  SJLYFieldName  = "SJLY";//
        public const string  SJBHFieldName  = "SJBH";//
        public const string  SJLXIDFieldName  = "SJLXID";//
        public const string  DZFieldName  = "DZ";//
        public const string  XFieldName  = "X";//
        public const string  YFieldName  = "Y";//
        public const string  MSFieldName  = "MS";//
        public const string  TPLJFieldName  = "TPLJ";//
        public const string  SBRIDFieldName  = "SBRID";//
        public const string  SBSJFieldName  = "SBSJ";//
        public const string  JSSJFieldName  = "JSSJ";//
        public const string  SFWJFieldName  = "SFWJ";//
        public const string  SJJJCDIDFieldName  = "SJJJCDID";//
        public const string  ZHCLSJQXFieldName  = "ZHCLSJQX";//
        public const string  SFSCFieldName  = "SFSC";//
        public const string  SBBMIDFieldName  = "SBBMID";//
        #endregion


    }
}

