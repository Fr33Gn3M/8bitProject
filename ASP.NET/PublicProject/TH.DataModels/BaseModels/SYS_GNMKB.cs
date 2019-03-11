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
    public partial class SYS_GNMKB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_GNMKB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sJGNID;//
        protected string gNMC;//
        protected string gNBT;//
        protected string gNLX;//
        protected string gNJKXX;//
        protected int? jBS;//
        protected int? xH;//
        protected string bZ;//
        protected string tPLJ;//
        protected string xTMC;//
        protected bool? sFKF;//
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
        public string SJGNID
        {
            set { sJGNID = value; }
            get { return sJGNID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GNMC
        {
            set { gNMC = value; }
            get { return gNMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GNBT
        {
            set { gNBT = value; }
            get { return gNBT; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GNLX
        {
            set { gNLX = value; }
            get { return gNLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GNJKXX
        {
            set { gNJKXX = value; }
            get { return gNJKXX; }
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
        public string BZ
        {
            set { bZ = value; }
            get { return bZ; }
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
        public string XTMC
        {
            set { xTMC = value; }
            get { return xTMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFKF
        {
            set { sFKF = value; }
            get { return sFKF; }
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

        public const string  SYS_GNMKBTableName  = "SYS_GNMKB";
        public const string  IDFieldName  = "ID";//
        public const string  SJGNIDFieldName  = "SJGNID";//
        public const string  GNMCFieldName  = "GNMC";//
        public const string  GNBTFieldName  = "GNBT";//
        public const string  GNLXFieldName  = "GNLX";//
        public const string  GNJKXXFieldName  = "GNJKXX";//
        public const string  JBSFieldName  = "JBS";//
        public const string  XHFieldName  = "XH";//
        public const string  BZFieldName  = "BZ";//
        public const string  TPLJFieldName  = "TPLJ";//
        public const string  XTMCFieldName  = "XTMC";//
        public const string  SFKFFieldName  = "SFKF";//
        public const string  SSGSFieldName  = "SSGS";//
        #endregion


    }
}

