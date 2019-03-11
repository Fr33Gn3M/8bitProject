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
    public partial class VWXMPQB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VWXMPQB()
        { 
        }

        #region 受保护的字段
        protected string mC;//
        protected string xMID;//
        protected string sSPQID;//
        #endregion


        #region 共有属性
        /// <summary>
        ///  
        /// </summary>
        public string MC
        {
            set { mC = value; }
            get { return mC; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XMID
        {
            set { xMID = value; }
            get { return xMID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string SSPQID
        {
            set { sSPQID = value; }
            get { return sSPQID; }
        }
        #endregion


        #region 字段名称

        public const string  VWXMPQBTableName  = "VWXMPQB";
        public const string  MCFieldName  = "MC";//
        public const string  XMIDFieldName  = "XMID";//
        public const string  SSPQIDFieldName  = "SSPQID";//
        #endregion


    }
}

