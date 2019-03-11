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
    public partial class SJ_SJLCJLB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SJ_SJLCJLB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string jDID;//
        protected string sJZT;//
        protected string bMID;//
        protected string cLRID;//
        protected DateTime? cLSJ=DateTime.Now;//
        protected string bZ;//
        protected string tPLJ;//
        protected string lCLX;//
        protected string cLQK;//
        protected DateTime? cJSJ=DateTime.Now;//
        protected int? xH;//
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
        public string BMID
        {
            set { bMID = value; }
            get { return bMID; }
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
        public DateTime? CLSJ
        {
            set { cLSJ = value; }
            get { return cLSJ; }
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
        public string LCLX
        {
            set { lCLX = value; }
            get { return lCLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string CLQK
        {
            set { cLQK = value; }
            get { return cLQK; }
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
        public int? XH
        {
            set { xH = value; }
            get { return xH; }
        }
        #endregion


        #region 字段名称

        public const string  SJ_SJLCJLBTableName  = "SJ_SJLCJLB";
        public const string  IDFieldName  = "ID";//
        public const string  JDIDFieldName  = "JDID";//
        public const string  SJZTFieldName  = "SJZT";//
        public const string  BMIDFieldName  = "BMID";//
        public const string  CLRIDFieldName  = "CLRID";//
        public const string  CLSJFieldName  = "CLSJ";//
        public const string  BZFieldName  = "BZ";//
        public const string  TPLJFieldName  = "TPLJ";//
        public const string  LCLXFieldName  = "LCLX";//
        public const string  CLQKFieldName  = "CLQK";//
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  XHFieldName  = "XH";//
        #endregion


    }
}

