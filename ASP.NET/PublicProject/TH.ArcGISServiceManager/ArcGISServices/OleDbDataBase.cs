using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace TH.ArcGISServiceManager
{
    internal static class OleDbDataBase
    {
         
        static string connstr = "";
        static OleDbConnection conn = null;
        static OleDbDataBase()
        {

        }

        public static string ConnectionString
        {
            set { connstr = value;
                conn = new OleDbConnection(connstr);
            }

            get {
               
                return conn.ConnectionString;
            }
        }

        /// <summary>
        /// 获取表数据
        /// </summary>
        /// <param name="commstr"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        
        public static DataTable GetDataTable(string commstr, out Exception ex)
        {
            ex = null;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(commstr, conn);
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ex = ex2;
                return null;
            }
        }

        /// <summary>
        /// 返回单一值
        /// </summary>
        /// <param name="commstr"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string commstr, out Exception ex)
        {
            ex = null;
            object o = null;
            try
            {
                conn.Open();
                OleDbCommand comm = new OleDbCommand(commstr, conn);
                o = comm.ExecuteScalar();
                conn.Close();
                return o;
            }
            catch (Exception ex2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ex = ex2;
                return null;
            }
        }

        /// <summary>
        /// 执行更新、删除操作
        /// </summary>
        /// <param name="commstr"></param>
        /// <param name="ex"></param>
        public static void ExecuteNonQuery(string commstr, out Exception ex)
        {
            ex = null;
            try
            {
                conn.Open();
                OleDbCommand comm = new OleDbCommand(commstr, conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                ex = ex2;
            }
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="content">错误内容</param>
        /// <param name="module">操作模块</param>
        /// <param name="type">操作类型</param>
        public static void WriteSysErrorLog(string content,string module,string type)
        {
            try
            {
                Exception ex = null;
                string OpName="administrator";
                string time = DateTime.Now.ToString();
                string insCommSgr = "insert into Sys_ErrorLog(操作时间,操作内容,操作模块,操作类型,操作人) values(@操作时间,@操作内容,@操作类型,@操作人)";
                //Common.OleDbDataBase.ExecuteNonQuery(insCommSgr, out ex);
                OleDbCommand comm = new OleDbCommand(insCommSgr, conn);
                comm.Parameters.Add(new OleDbParameter("@操作时间", time));
                comm.Parameters.Add(new OleDbParameter("@操作内容", content));
                comm.Parameters.Add(new OleDbParameter("@操作模块", module));
                comm.Parameters.Add(new OleDbParameter("@操作类型", type));
                comm.Parameters.Add(new OleDbParameter("@操作人", OpName));
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                conn.Close();
            }
        }
    }
}