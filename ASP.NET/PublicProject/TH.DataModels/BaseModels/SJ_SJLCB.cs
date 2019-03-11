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
    public partial class SJ_SJLCB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SJ_SJLCB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string jDID;//
        protected string cLRID;//
        protected string bMID;//
        protected DateTime? cJSJ=DateTime.Now;//
        protected int? xH;//
        protected string sJZT;//
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
        public string CLRID
        {
            set { cLRID = value; }
            get { return cLRID; }
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
        /// <summary>
        ///  
        /// </summary>
        public string SJZT
        {
            set { sJZT = value; }
            get { return sJZT; }
        }
        #endregion


        #region 字段名称

        public const string  SJ_SJLCBTableName  = "SJ_SJLCB";
        public const string  IDFieldName  = "ID";//
        public const string  JDIDFieldName  = "JDID";//
        public const string  CLRIDFieldName  = "CLRID";//
        public const string  BMIDFieldName  = "BMID";//
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  XHFieldName  = "XH";//
        public const string  SJZTFieldName  = "SJZT";//
        #endregion


    }
}

