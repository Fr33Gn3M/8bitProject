using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DataModels
{
    public static class DataBaseTableName
    {
        public const string SJ_SJLCBTableName = "SJ_SJLCB";
        public const string SJ_SJLCJDBTableName = "SJ_SJLCJDB";
        public const string SJ_SJLCJLBTableName = "SJ_SJLCJLB";
        public const string SJ_SJZBTableName = "SJ_SJZB";
        public const string VWSJ_SJLCBTableName = "VWSJ_SJLCB";
        public const string VWSJ_SJZBTableName = "VWSJ_SJZB";
        public const string VWSJ_SJLCJDBTableName = "VWSJ_SJLCJDB";
        public const string VWSJ_SJJDJLBTableName = "VWSJ_SJJDJLB";
        public const string VWSJ_SJLCJLBTableName = "VWSJ_SJLCJLB";
        public const string SYS_DLFWBTableName = "SYS_DLFWB";
        public const string SYS_DLRZBTableName = "SYS_DLRZB";
        public const string SYS_GNJSBTableName = "SYS_GNJSB";
        public const string SYS_GNMKBTableName = "SYS_GNMKB";
        public const string SYS_GSXXBTableName = "SYS_GSXXB";
        public const string SYS_JSGNBTableName = "SYS_JSGNB";
        public const string SYS_PZBTableName = "SYS_PZB";
        public const string SYS_DWBMBTableName = "SYS_DWBMB";
        public const string SYS_LSBMDZBTableName = "SYS_LSBMDZB";
        public const string SYS_YHBTableName = "SYS_YHB";
        public const string SYS_YHJSQXBTableName = "SYS_YHJSQXB";
        public const string SYS_SJZDBTableName = "SYS_SJZDB";
        public const string SYS_XTSZBTableName = "SYS_XTSZB";
        public const string SYS_XXBTableName = "SYS_XXB";
        public const string SYS_XXJSRBTableName = "SYS_XXJSRB";
        public const string ZT_YWYYBTableName = "ZT_YWYYB";
        public const string ZT_YYPZBTableName = "ZT_YYPZB";
        public const string ZT_YYZDBTableName = "ZT_YYZDB";
        public const string XM_XMBTableName = "XM_XMB";
        public const string XM_XMSSQQBTableName = "XM_XMSSQQB";
        public const string XM_XMYHQXBTableName = "XM_XMYHQXB";
        public const string XM_XXSSBJBTableName = "XM_XXSSBJB";
        public const string ZT_YYTCXRBTableName = "ZT_YYTCXRB";
        public const string VWXMYHQXBTableName = "VWXMYHQXB";
        public const string VWYHJSBTableName = "VWYHJSB";
        public const string VWYYMBZDBTableName = "VWYYMBZDB";
        public const string VWZTYWYYPZBTableName = "VWZTYWYYPZB";
        public const string VWZTYYTCPZBTableName = "VWZTYYTCPZB";
        public const string VWXMPQBTableName = "VWXMPQB";
        public const string VWXMYYBTableName = "VWXMYYB";
        public const string VWZTYWYYZDBTableName = "VWZTYWYYZDB";
        public const string VWXMBJXQBTableName = "VWXMBJXQB";
        public const string SYS_DBMBPZBTableName = "SYS_DBMBPZB";
        public const string VWSJ_SJJJCDBTableName = "VWSJ_SJJJCDB";
        public const string VWSYS_XXTSBTableName = "VWSYS_XXTSB";
        public const string VWSJ_SJDYJDBTableName = "VWSJ_SJDYJDB";

        public static Dictionary<string, string> m_DataBaseKyFieldTableDic;
        public static Dictionary<string, string> DataBaseKyFieldTableDic
        {
            get
            {
                if (m_DataBaseKyFieldTableDic == null)
                    InitDict();
                return m_DataBaseKyFieldTableDic;
            }
        }

        public static Dictionary<string, string> m_DataBaseNameSpaceTableDic;
        public static Dictionary<string, string> DataBaseNameSpaceTableDic
        {
            get
            {
                if (m_DataBaseNameSpaceTableDic == null)
                    InitDict();
                return m_DataBaseNameSpaceTableDic;
            }
        }


        public static Dictionary<string, string> m_TableToTableNameDic;
        public static Dictionary<string, string> TableToTableNameDic
        {
            get
            {
                if (m_TableToTableNameDic == null)
                    InitDict();
                return m_TableToTableNameDic;
            }
        }



        private static void InitDict()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("SJ_SJLCB", "ID");
            dic.Add("SJ_SJLCJDB", "ID");
            dic.Add("SJ_SJLCJLB", "ID");
            dic.Add("SJ_SJZB", "ID");
            dic.Add("VWSJ_SJLCB", "");
            dic.Add("VWSJ_SJZB", "");
            dic.Add("VWSJ_SJLCJDB", "");
            dic.Add("VWSJ_SJJDJLB", "");
            dic.Add("VWSJ_SJLCJLB", "");
            dic.Add("SYS_DLFWB", "ID");
            dic.Add("SYS_DLRZB", "ID");
            dic.Add("SYS_GNJSB", "ID");
            dic.Add("SYS_GNMKB", "ID");
            dic.Add("SYS_GSXXB", "ID");
            dic.Add("SYS_JSGNB", "ID");
            dic.Add("SYS_PZB", "ID");
            dic.Add("SYS_DWBMB", "ID");
            dic.Add("SYS_LSBMDZB", "BM");
            dic.Add("SYS_YHB", "ID");
            dic.Add("SYS_YHJSQXB", "ID");
            dic.Add("SYS_SJZDB", "ID");
            dic.Add("SYS_XTSZB", "ID");
            dic.Add("SYS_XXB", "ID");
            dic.Add("SYS_XXJSRB", "ID");
            dic.Add("ZT_YWYYB", "ID");
            dic.Add("ZT_YYPZB", "ID");
            dic.Add("ZT_YYZDB", "ID");
            dic.Add("XM_XMB", "ID");
            dic.Add("XM_XMSSQQB", "ID");
            dic.Add("XM_XMYHQXB", "ID");
            dic.Add("XM_XXSSBJB", "ID");
            dic.Add("ZT_YYTCXRB", "ID");
            dic.Add("VWXMYHQXB", "");
            dic.Add("VWYHJSB", "");
            dic.Add("VWYYMBZDB", "");
            dic.Add("VWZTYWYYPZB", "");
            dic.Add("VWZTYYTCPZB", "");
            dic.Add("VWXMPQB", "");
            dic.Add("VWXMYYB", "");
            dic.Add("VWZTYWYYZDB", "");
            dic.Add("VWXMBJXQB", "");
            dic.Add("SYS_DBMBPZB", "");
            dic.Add("VWSJ_SJJJCDB", "");
            dic.Add("VWSYS_XXTSB", "");
            dic.Add("VWSJ_SJDYJDB", "");
            m_DataBaseKyFieldTableDic = dic;

            var dic1 = new Dictionary<string, string>();
            dic1.Add("SJ_SJLCB", NameSpace);
            dic1.Add("SJ_SJLCJDB", NameSpace);
            dic1.Add("SJ_SJLCJLB", NameSpace);
            dic1.Add("SJ_SJZB", NameSpace);
            dic1.Add("VWSJ_SJLCJLB", NameSpace);
            dic1.Add("VWSJ_SJLCB", NameSpace);
            dic1.Add("VWSJ_SJZB", NameSpace);
            dic1.Add("VWSJ_SJLCJDB", NameSpace);
            dic1.Add("VWSJ_SJJDJLB", NameSpace);
            dic1.Add("SYS_DLFWB", NameSpace);
            dic1.Add("SYS_DLRZB", NameSpace);
            dic1.Add("SYS_GNJSB", NameSpace);
            dic1.Add("SYS_GNMKB", NameSpace);
            dic1.Add("SYS_GSXXB", NameSpace);
            dic1.Add("SYS_JSGNB", NameSpace);
            dic1.Add("SYS_PZB", NameSpace);
            dic1.Add("SYS_DWBMB", NameSpace);
            dic1.Add("SYS_LSBMDZB", NameSpace);
            dic1.Add("SYS_YHB", NameSpace);
            dic1.Add("SYS_YHJSQXB", NameSpace);
            dic1.Add("SYS_SJZDB", NameSpace);
            dic1.Add("SYS_XTSZB", NameSpace);
            dic1.Add("SYS_XXB", NameSpace);
            dic1.Add("SYS_XXJSRB", NameSpace);
            dic1.Add("ZT_YWYYB", NameSpace);
            dic1.Add("ZT_YYPZB", NameSpace);
            dic1.Add("ZT_YYZDB", NameSpace);
            dic1.Add("XM_XMB", NameSpace);
            dic1.Add("XM_XMSSQQB", NameSpace);
            dic1.Add("XM_XMYHQXB", NameSpace);
            dic1.Add("XM_XXSSBJB", NameSpace);
            dic1.Add("ZT_YYTCXRB", NameSpace);
            dic1.Add("VWXMYHQXB", NameSpace);
            dic1.Add("VWYHJSB", NameSpace);
            dic1.Add("VWYYMBZDB", NameSpace);
            dic1.Add("VWZTYWYYPZB", NameSpace);
            dic1.Add("VWZTYYTCPZB", NameSpace);
            dic1.Add("VWXMPQB", NameSpace);
            dic1.Add("VWXMYYB", NameSpace);
            dic1.Add("VWZTYWYYZDB", NameSpace);
            dic1.Add("VWXMBJXQB", NameSpace);
            dic1.Add("SYS_DBMBPZB", NameSpace);
            dic1.Add("VWSJ_SJJJCDB", NameSpace);
            dic1.Add("VWSYS_XXTSB", NameSpace);
            dic1.Add("VWSJ_SJDYJDB", NameSpace);
            m_DataBaseNameSpaceTableDic = dic1;

            var dic2 = new Dictionary<string, string>();
            dic2.Add("SJ_SJLCB", "SJ_SJLCB");
            dic2.Add("SJ_SJLCJDB", "SJ_SJLCJDB");
            dic2.Add("SJ_SJLCJLB", "SJ_SJLCJLB");
            dic2.Add("SJ_SJZB", "SJ_SJZB");
            dic2.Add("VWSJ_SJLCB", "VWSJ_SJLCB");
            dic2.Add("VWSJ_SJZB", "VWSJ_SJZB");
            dic2.Add("VWSJ_SJLCJDB", "VWSJ_SJLCJDB");
            dic2.Add("VWSJ_SJJDJLB", "VWSJ_SJJDJLB");
            dic2.Add("VWSJ_SJLCJLB", "VWSJ_SJLCJLB");
            dic2.Add("SYS_DLFWB", "SYS_DLFWB");
            dic2.Add("SYS_DLRZB", "SYS_DLRZB");
            dic2.Add("SYS_GNJSB", "SYS_GNJSB");
            dic2.Add("SYS_GNMKB", "SYS_GNMKB");
            dic2.Add("SYS_GSXXB", "SYS_GSXXB");
            dic2.Add("SYS_JSGNB", "SYS_JSGNB");
            dic2.Add("SYS_PZB", "SYS_PZB");
            dic2.Add("SYS_DWBMB", "SYS_DWBMB");
            dic2.Add("SYS_LSBMDZB", "SYS_LSBMDZB");
            dic2.Add("SYS_YHB", "SYS_YHB");
            dic2.Add("SYS_YHJSQXB", "SYS_YHJSQXB");
            dic2.Add("SYS_SJZDB", "SYS_SJZDB");
            dic2.Add("SYS_XTSZB", "SYS_XTSZB");
            dic2.Add("SYS_XXB", "SYS_XXB");
            dic2.Add("SYS_XXJSRB", "SYS_XXJSRB");
            dic2.Add("ZT_YWYYB", "ZT_YWYYB");
            dic2.Add("ZT_YYPZB", "ZT_YYPZB");
            dic2.Add("ZT_YYZDB", "ZT_YYZDB");
            dic2.Add("XM_XMB", "XM_XMB");
            dic2.Add("XM_XMSSQQB", "XM_XMSSQQB");
            dic2.Add("XM_XMYHQXB", "XM_XMYHQXB");
            dic2.Add("XM_XXSSBJB", "XM_XXSSBJB");
            dic2.Add("ZT_YYTCXRB", "ZT_YYTCXRB");
            dic2.Add("VWXMYHQXB", "VWXMYHQXB");
            dic2.Add("VWYHJSB", "VWYHJSB");
            dic2.Add("VWYYMBZDB", "VWYYMBZDB");
            dic2.Add("VWZTYWYYPZB", "VWZTYWYYPZB");
            dic2.Add("VWZTYYTCPZB", "VWZTYYTCPZB");
            dic2.Add("VWXMPQB", "VWXMPQB");
            dic2.Add("VWXMYYB", "VWXMYYB");
            dic2.Add("VWZTYWYYZDB", "VWZTYWYYZDB");
            dic2.Add("VWXMBJXQB", "VWXMBJXQB");
            dic2.Add("SYS_DBMBPZB", "SYS_DBMBPZB");
            dic2.Add("VWSJ_SJJJCDB", "VWSJ_SJJJCDB");
            dic2.Add("VWSYS_XXTSB", "VWSYS_XXTSB");
            dic2.Add("VWSJ_SJDYJDB", "VWSJ_SJDYJDB");
            m_TableToTableNameDic = dic2;
        }
        public const string NameSpace = "FD.DataModels";
    }
}
