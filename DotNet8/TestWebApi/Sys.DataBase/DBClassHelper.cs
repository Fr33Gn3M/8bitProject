using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Sys.DataBase.Common;

namespace Sys.DataBase
{
    /// <summary>
    /// 支持连接池的数据库访问类
    /// </summary>
    public class DBClassHelper
    {
        private static ConfigurationManagerHelper _configManager;

        public DBClassHelper(ConfigurationManagerHelper configManager)
        {
            _configManager = configManager;
            CommandTimeout = _configManager.GetClassLibrarySetting<int>("CommandTimeout");
        }

        //数据库语句执行超时时间，设置为0则使用默认值30
        private static int CommandTimeout = 30;

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="connName">连接名</param>
        /// <returns></returns>
        public static DbConnection OpenConnect(string connectionStr, string providerName)
        {
            DbConnection Conn;
            DbProviderFactory f = DbProviderFactories.GetFactory(providerName);
            Conn = f.CreateConnection();
            //得到连接字符串
            Conn.ConnectionString = connectionStr;
            Conn.Open();
            return Conn;
        }

        /// <summary>
        /// 执行查询返回DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns>成功返回DataTable,失败则返回 null</returns>
        static public DataTable ExecuteQueryToDataTable(string sql, DbConnection Conn, int? commandTimeout = null)
        {
            DataTable dt = new DataTable();
            IDataReader reader = ExecuteQuery(sql, Conn, commandTimeout);
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// 执行查询返回DataReader
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">连接对象</param>
        /// <returns>成功时返回Reader对象，失败时返回null</returns>
        static public IDataReader ExecuteQuery(string sql, DbConnection Conn, int? commandTimeout = null)
        {
            IDataReader reader = null;
            if (Conn == null)
            {
                return null;
            }
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                IDbCommand cmd = Conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = commandTimeout == null ? CommandTimeout : (int)commandTimeout;
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                DBLogHelper.ErrorLog(ex, "sql =" + sql);
                return null;
            }

        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">数据库连接对象</param>
        /// <returns>返回受影响行数</returns>
        static public int Execute(string sql, DbConnection Conn)
        {
            if (Conn == null)
            {
                DBLogHelper.WarnLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn):连接对象为空!");
            }
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.CommandText = sql;
            try
            {
                var count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception ex)
            {
                DBLogHelper.ErrorLog(ex, "sql =" + sql);
                return 0;
            }
            finally
            {
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">数据库连接对象</param>
        /// <returns>返回受影响行数</returns>
        static public int ExecuteWithTrans(string sql, DbConnection Conn, DbTransaction tran)
        {
            if (Conn == null)
            {
                DBLogHelper.WarnLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn):连接对象为空!");
                return 0;
            }
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = tran;
            cmd.CommandText = sql;
            try
            {
                var count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception ex)
            {
                DBLogHelper.ErrorLog(ex, "sql =" + sql);
                return 0;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">数据库连接对象</param>
        /// <param name="param">参数</param>
        /// <returns>返回受影响行数</returns>
        static public int Execute(string sql, DbConnection Conn, DbParameter[] param)
        {
            if (Conn == null)
            {
                DBLogHelper.WarnLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn, System.Data.Common.DbParameter[] param):连接对象为空!");
                return 0;
            }
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.CommandText = sql;
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DBLogHelper.ErrorLog(ex, "sql=" + sql);
                return 0;
            }
        }

        /// <summary>
        /// 执行一个事务
        /// </summary>
        /// <param name="sqls">Sql语句组</param>
        /// <returns>成功时返回true</returns>
        static public bool ExecuteTrans(string[] sqls, DbConnection Conn)
        {
            IDbTransaction myTrans;
            if (Conn == null)
            {
                DBLogHelper.WarnLog("DBClassHelper.ExecuteTrans(string[] sqls):连接对象为空!");
                return false;
            }
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = CommandTimeout;
            myTrans = Conn.BeginTransaction();
            cmd.Transaction = myTrans;
            var wrongsql = string.Empty;
            try
            {

                foreach (string sql in sqls)
                {
                    if (sql != null)
                    {
                        wrongsql = sql;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                DBLogHelper.ErrorLog(ex, "sql =" + wrongsql);
                return false;
            }
            finally
            {
                Conn.Dispose();
                Conn.Close();
            }
            return true;
        }

        internal static void SqlWarnLog(string sql)
        {
            if (sql.ToLower().Contains("insert") && sql.ToLower().Contains("tcc_sstcb"))
            {
                StackFrame[] stacks = new StackTrace().GetFrames();
                var stacksStr = ToString(stacks);
                DBLogHelper.WarnLog($"insert sstcb sql:{sql}, stacks:{stacksStr}");
            }
        }
        private static string ToString(StackFrame[] stacks)
        {
            string result = string.Empty;
            foreach (StackFrame stack in stacks)
            {
                result += string.Format("{0} {1} {2} {3}\r\n", stack.GetFileName(),
                    stack.GetFileLineNumber(),
                    stack.GetFileColumnNumber(),
                    stack.GetMethod().ToString());
            }
            return result;
        }
    }

}
