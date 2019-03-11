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
    public partial class ZT_YYZDB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ZT_YYZDB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sSYY;//
        protected string zDLX;//
        protected string zDMC;//
        protected string xSM;//
        protected bool? sFXS;//
        protected int? zDCD;//
        protected bool? sFWZJ;//
        protected bool? sFWK;//
        protected bool? sFKBJ;//
        protected bool? sFZDCJ;//
        protected string mJZ;//
        protected string mJBS;//
        protected bool? sFWMRZD;//
        protected string mRZ;//
        protected int? xSSX;//
        protected bool? sFWCXZD;//
        protected string bZ;//
        protected DateTime? cJSJ;//
        protected bool? sFSC;//
        protected DateTime? gXSJ;//
        protected string cZRID;//
        protected string gLBM;//
        protected string gLBZDMC;//
        protected string gLXX;//
        protected string gXSM;//
        protected string mRDW;//
        protected string mRYS;//
        protected bool? sFWGLZD;//
        protected bool? sFJLLS;//
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
        public string SSYY
        {
            set { sSYY = value; }
            get { return sSYY; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string ZDLX
        {
            set { zDLX = value; }
            get { return zDLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string ZDMC
        {
            set { zDMC = value; }
            get { return zDMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XSM
        {
            set { xSM = value; }
            get { return xSM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFXS
        {
            set { sFXS = value; }
            get { return sFXS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? ZDCD
        {
            set { zDCD = value; }
            get { return zDCD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWZJ
        {
            set { sFWZJ = value; }
            get { return sFWZJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWK
        {
            set { sFWK = value; }
            get { return sFWK; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFKBJ
        {
            set { sFKBJ = value; }
            get { return sFKBJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFZDCJ
        {
            set { sFZDCJ = value; }
            get { return sFZDCJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MJZ
        {
            set { mJZ = value; }
            get { return mJZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MJBS
        {
            set { mJBS = value; }
            get { return mJBS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWMRZD
        {
            set { sFWMRZD = value; }
            get { return sFWMRZD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MRZ
        {
            set { mRZ = value; }
            get { return mRZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public int? XSSX
        {
            set { xSSX = value; }
            get { return xSSX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWCXZD
        {
            set { sFWCXZD = value; }
            get { return sFWCXZD; }
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
        public bool? SFSC
        {
            set { sFSC = value; }
            get { return sFSC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime? GXSJ
        {
            set { gXSJ = value; }
            get { return gXSJ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string CZRID
        {
            set { cZRID = value; }
            get { return cZRID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GLBM
        {
            set { gLBM = value; }
            get { return gLBM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GLBZDMC
        {
            set { gLBZDMC = value; }
            get { return gLBZDMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GLXX
        {
            set { gLXX = value; }
            get { return gLXX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string GXSM
        {
            set { gXSM = value; }
            get { return gXSM; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MRDW
        {
            set { mRDW = value; }
            get { return mRDW; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string MRYS
        {
            set { mRYS = value; }
            get { return mRYS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFWGLZD
        {
            set { sFWGLZD = value; }
            get { return sFWGLZD; }
        }
        /// <summary>
        ///  
        /// </summary>
        public bool? SFJLLS
        {
            set { sFJLLS = value; }
            get { return sFJLLS; }
        }
        #endregion


        #region 字段名称

        public const string  ZT_YYZDBTableName  = "ZT_YYZDB";
        public const string  IDFieldName  = "ID";//
        public const string  SSYYFieldName  = "SSYY";//
        public const string  ZDLXFieldName  = "ZDLX";//
        public const string  ZDMCFieldName  = "ZDMC";//
        public const string  XSMFieldName  = "XSM";//
        public const string  SFXSFieldName  = "SFXS";//
        public const string  ZDCDFieldName  = "ZDCD";//
        public const string  SFWZJFieldName  = "SFWZJ";//
        public const string  SFWKFieldName  = "SFWK";//
        public const string  SFKBJFieldName  = "SFKBJ";//
        public const string  SFZDCJFieldName  = "SFZDCJ";//
        public const string  MJZFieldName  = "MJZ";//
        public const string  MJBSFieldName  = "MJBS";//
        public const string  SFWMRZDFieldName  = "SFWMRZD";//
        public const string  MRZFieldName  = "MRZ";//
        public const string  XSSXFieldName  = "XSSX";//
        public const string  SFWCXZDFieldName  = "SFWCXZD";//
        public const string  BZFieldName  = "BZ";//
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  SFSCFieldName  = "SFSC";//
        public const string  GXSJFieldName  = "GXSJ";//
        public const string  CZRIDFieldName  = "CZRID";//
        public const string  GLBMFieldName  = "GLBM";//
        public const string  GLBZDMCFieldName  = "GLBZDMC";//
        public const string  GLXXFieldName  = "GLXX";//
        public const string  GXSMFieldName  = "GXSM";//
        public const string  MRDWFieldName  = "MRDW";//
        public const string  MRYSFieldName  = "MRYS";//
        public const string  SFWGLZDFieldName  = "SFWGLZD";//
        public const string  SFJLLSFieldName  = "SFJLLS";//
        #endregion


    }
}

