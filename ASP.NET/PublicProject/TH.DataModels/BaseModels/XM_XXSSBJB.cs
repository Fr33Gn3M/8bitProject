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
    public partial class XM_XXSSBJB:DBBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public XM_XXSSBJB()
        { 
        }

        #region 受保护的字段
        protected string iD;//
        protected string sSBJID;//
        protected string xMID;//
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
        public string SSBJID
        {
            set { sSBJID = value; }
            get { return sSBJID; }
        }
        /// <summary>
        ///  
        /// </summary>
        public string XMID
        {
            set { xMID = value; }
            get { return xMID; }
        }
        #endregion


        #region 字段名称

        public const string  XM_XXSSBJBTableName  = "XM_XXSSBJB";
        public const string  IDFieldName  = "ID";//
        public const string  SSBJIDFieldName  = "SSBJID";//
        public const string  XMIDFieldName  = "XMID";//
        #endregion


    }
}

