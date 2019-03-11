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
    public partial class VWXMYYB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VWXMYYB()
        { 
        }

        #region 受保护的字段
        protected string yYBT;//
        protected string xMID;//
        protected string sSBJID;//
        #endregion


        #region 共有属性
        /// <summary>
        ///  
        /// </summary>
        public string YYBT
        {
            set { yYBT = value; }
            get { return yYBT; }
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
        public string SSBJID
        {
            set { sSBJID = value; }
            get { return sSBJID; }
        }
        #endregion


        #region 字段名称

        public const string  VWXMYYBTableName  = "VWXMYYB";
        public const string  YYBTFieldName  = "YYBT";//
        public const string  XMIDFieldName  = "XMID";//
        public const string  SSBJIDFieldName  = "SSBJID";//
        #endregion


    }
}

