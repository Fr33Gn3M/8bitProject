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
    public partial class SYS_XXB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SYS_XXB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string xXBT;//
        protected string xXNR;//
        protected DateTime? cJSJ;//
        protected string xXLX;//
        protected string xXPZ;//
        protected string tSR;//
        protected string tSRID;//
        protected bool? sFSC;//
        protected string sBLX;//
        protected string xXLY;//
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
        public string XXBT
        {
            set { xXBT = value; }
            get { return xXBT; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XXNR
        {
            set { xXNR = value; }
            get { return xXNR; }
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
        public string XXLX
        {
            set { xXLX = value; }
            get { return xXLX; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XXPZ
        {
            set { xXPZ = value; }
            get { return xXPZ; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string TSR
        {
            set { tSR = value; }
            get { return tSR; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string TSRID
        {
            set { tSRID = value; }
            get { return tSRID; }
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
        public string SBLX
        {
            set { sBLX = value; }
            get { return sBLX; }
        }/// <summary>
         ///  
         /// </summary>
        public string XXLY
        {
            set { xXLY = value; }
            get { return xXLY; }
        }
        #endregion


        #region 字段名称

        public const string  SYS_XXBTableName  = "SYS_XXB";
        public const string  IDFieldName  = "ID";//
        public const string  XXBTFieldName  = "XXBT";//
        public const string  XXNRFieldName  = "XXNR";//
        public const string  CJSJFieldName  = "CJSJ";//
        public const string  XXLXFieldName  = "XXLX";//
        public const string  XXPZFieldName  = "XXPZ";//
        public const string  TSRFieldName  = "TSR";//
        public const string  TSRIDFieldName  = "TSRID";//
        public const string  SBLXFieldName  = "SFSC";//
        public const string  XXLYFieldName = "SFSC";//
        #endregion


    }
}

