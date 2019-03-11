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
    public partial class XM_XMB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public XM_XMB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string xMMC;//
        protected string xMBH;//
        protected string xMZT;//
        protected DateTime? cJSJ;//
        protected string cZRID;//
        protected bool? sFSC;//
        protected bool? sFGD;//
        protected DateTime? gXSJ;//
        protected string fZRID;//
        protected string xMMS;//
        protected string xMFL;//
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
        public string XMMC
        {
            set { xMMC = value; }
            get { return xMMC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XMBH
        {
            set { xMBH = value; }
            get { return xMBH; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XMZT
        {
            set { xMZT = value; }
            get { return xMZT; }
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
        public string CZRID
        {
            set { cZRID = value; }
            get { return cZRID; }
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
        public bool? SFGD
        {
            set { sFGD = value; }
            get { return sFGD; }
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
        public string FZRID
        {
            set { fZRID = value; }
            get { return fZRID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XMMS
        {
            set { xMMS = value; }
            get { return xMMS; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XMFL
        {
            set { xMFL = value; }
            get { return xMFL; }
        }
        #endregion


        #region 字段名称

        public const string  XM_XMBTableName  = "XM_XMB";
        public const string  IDFieldName  = "ID";//
        public const string  XMMCFieldName  = "XMMC";//
        public const string  XMBHFieldName  = "XMBH";//
        public const string  XMZTFieldName  = "XMZT";//
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  CZRIDFieldName  = "CZRID";//
        public const string  SFSCFieldName  = "SFSC";//
        public const string  SFGDFieldName  = "SFGD";//
        public const string  GXSJFieldName  = "GXSJ";//
        public const string  FZRIDFieldName  = "FZRID";//
        public const string  XMMSFieldName  = "XMMS";//
        public const string  XMFLFieldName  = "XMFL";//
        #endregion


    }
}

