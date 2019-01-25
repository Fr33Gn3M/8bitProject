using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

namespace DAL
{
    /// <summary>
    /// 支持连接池的数据库访问类
    /// </summary>
    public class DBClassHelper
    {
        public DBClassHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        ///// <summary>
        // /// 得到连接字符串
        ///// </summary>
        // /// <returns>连接字符串</returns>
        // static private string getConnString(string key)
        // {
        //     string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[key].ToString();
        //     if (connStr == null || connStr == "")
        //     {
        //         DBClassHelper.ErrLog("DBClassHelper.getConnString(string key):["+key+"]所指定的连接类型为空");
        //     }
        //     return connStr;
        // }

        //public static string GetConnectionEntityString(ref string prividerName)
        //{
        //    var conn = System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString;
        //    //    var esb = new System.Data.EntityClient.EntityConnectionStringBuilder(conn);
        //    //    prividerName = esb.Provider;
        //    //    return esb.ProviderConnectionString;
        //    //}
        //    return conn;
        //}

        //public static string GetConnectionEntityString(ref string prividerName, string connStr)
        //{
        //    if (string.IsNullOrEmpty(connStr))
        //        return GetConnectionEntityString(ref prividerName);
        //    var conn = System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
        //    //var esb = new System.Data.EntityClient.EntityConnectionStringBuilder(conn);
        //    //prividerName = esb.Provider;
        //    //return esb.ProviderConnectionString;
        //    return conn;
        //}


        //public static string GetConnectionString(ref string prividerName, string connStr)
        //{
        //    string conn = "";
        //    if (string.IsNullOrEmpty(connStr))
        //        conn = GetConnectionEntityString(ref prividerName);
        //    conn = System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
        //    var esb = new System.Data.EntityClient.EntityConnectionStringBuilder(conn);
        //    prividerName = esb.Provider;
        //    return esb.ProviderConnectionString;
        //}

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="connName">连接名</param>
        /// <returns></returns>
        public static System.Data.Common.DbConnection OpenConnect(string connectionStr, string providerName)
        {
            System.Data.Common.DbConnection Conn;

            //if (providerName.Contains("SQLite"))
            //    Conn = new System.Data.SQLite.SQLiteConnection();
            //else
            //    if (providerName.Contains("OracleClient"))
            //        Conn = new DDTek.Oracle.OracleConnection();
            //    else
            //        if (providerName.Contains("MySqlClient"))
            //            Conn = new MySql.Data.MySqlClient.MySqlConnection();
            //        else
            //        {
            System.Data.Common.DbProviderFactory f = System.Data.Common.DbProviderFactories.GetFactory(providerName);
            Conn = f.CreateConnection();
            //          }
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
        static public System.Data.DataTable ExecuteQueryToDataTable(string sql, System.Data.Common.DbConnection Conn)
        {
            //if (Conn is System.Data.SQLite.SQLiteConnection)
            //{
            //    var cmd = Conn.CreateCommand() as System.Data.SQLite.SQLiteCommand;
            //    cmd.CommandText = sql;
            //    cmd.CommandTimeout = 180;
            //    var da = new System.Data.SQLite.SQLiteDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    return dt;
            //}
            //if (Conn is DDTek.Oracle.OracleConnection)
            //{
            //    var cmd = Conn.CreateCommand() as DDTek.Oracle.OracleCommand;
            //    cmd.CommandText = sql;
            //    cmd.CommandTimeout = 180;
            //    var da = new DDTek.Oracle.OracleDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    return dt;
            //}
            //else
            //{
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.IDataReader reader = ExecuteQuery(sql, Conn);
                dt.Load(reader);
                return dt;
            //}

        }//ExecuteQueryToDataTable(string sql)


        /// <summary>
        /// 执行查询返回DataReader
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">连接对象</param>
        /// <returns>成功时返回Reader对象，失败时返回null</returns>
        static public System.Data.IDataReader ExecuteQuery(string sql, System.Data.Common.DbConnection Conn)
        {
            System.Data.IDataReader reader = null;
            if (Conn == null)
            {
                return null;
            }
            try
            {
                if (Conn.State == System.Data.ConnectionState.Closed)
                {
                    Conn.Open();
                }
                System.Data.IDbCommand cmd = Conn.CreateCommand();
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("死锁"))
                {
                    WriteLog(ex.Message + " 再做列锁循环！ExecuteQuery");
                    System.Threading.Thread.Sleep(200);
                    return ExecuteQuery(sql, Conn);
                }
                else
                {
                    DBClassHelper.ErrLog("DBClassHelper.ExecuteQuery(string sql, System.Data.Common.DbConnection Conn):" + ex.Message + ";  \n sql =" + sql);
                }
                return null;
            }

        }//ExecuteQuery(string sql)

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">数据库连接对象</param>
        /// <returns>返回受影响行数</returns>
        static public int Execute(string sql, System.Data.Common.DbConnection Conn)
        {
            if (Conn == null)
            {
                DBClassHelper.ErrLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn):连接对象为空!");
                //return 0;
            }
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                Conn.Open();
            }
            System.Data.IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = 180;
            cmd.CommandText = sql;
            try
            {
                var count = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return count;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                if (ex.Message.Contains("死锁"))
                {
                    WriteLog(ex.Message + " 再做列锁循环！Execute");
                    System.Threading.Thread.Sleep(200);
                    return Execute(sql, Conn);
                }
                else
                {
                    DBClassHelper.ErrLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn):" + ex.Message + "/nsql=" + sql);
                    return 0;
                }
            }
        }//Execute(string sql)

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">数据库连接对象</param>
        /// <returns>返回受影响行数</returns>
        static public int ExecuteWithTrans(string sql, System.Data.Common.DbConnection Conn, System.Data.Common.DbTransaction tran)
        {
            if (Conn == null)
            {
                DBClassHelper.ErrLog("DBClassHelper.ExecuteWithTrans(string sql, System.Data.Common.DbConnection Conn):连接对象为空!");
                return 0;
            }
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                Conn.Open();
            }
            System.Data.IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = 180;
            cmd.Transaction = tran;
            cmd.CommandText = sql;
            try
            {
                var count = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return count;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                if (ex.Message.Contains("死锁"))
                {
                    WriteLog(ex.Message + " 再做列锁循环！ExecuteWithTrans");
                    System.Threading.Thread.Sleep(200);
                    return Execute(sql, Conn);
                }
                else
                {
                    DBClassHelper.ErrLog("DBClassHelper.ExecuteWithTrans(string sql, System.Data.Common.DbConnection Conn):" + ex.Message + "/nsql=" + sql);
                    return 0;
                }
            }
        }//Execute(string sql)

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="Conn">数据库连接对象</param>
        /// <param name="param">参数</param>
        /// <returns>返回受影响行数</returns>
        static public int Execute(string sql, System.Data.Common.DbConnection Conn, System.Data.Common.DbParameter[] param)
        {
            if (Conn == null)
            {
                DBClassHelper.ErrLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn, System.Data.Common.DbParameter[] param):连接对象为空!");
                return 0;
            }
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                Conn.Open();
            }
            System.Data.IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = 180;
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
                DBClassHelper.ErrLog("DBClassHelper.Execute(string sql, System.Data.Common.DbConnection Conn, System.Data.Common.DbParameter[] param):" + ex.Message + "/nsql=" + sql);
                return 0;
            }
        }//Execute(string sql,System.Data.IDataParameter[] param)

        /// <summary>
        /// 执行一个事务
        /// </summary>
        /// <param name="sqls">Sql语句组</param>
        /// <returns>成功时返回true</returns>
        static public bool ExecuteTrans(string[] sqls, System.Data.Common.DbConnection Conn)
        {
            System.Data.IDbTransaction myTrans;
            if (Conn == null)
            {
                DBClassHelper.ErrLog("DBClassHelper.ExecuteTrans(string[] sqls):连接对象为空!");
                return false;
            }
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                Conn.Open();
            }
            System.Data.IDbCommand cmd = Conn.CreateCommand();
            cmd.CommandTimeout = 180;
            myTrans = Conn.BeginTransaction();
            cmd.Transaction = myTrans;
            try
            {
                foreach (string sql in sqls)
                {
                    if (sql != null)
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                myTrans.Commit();
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                DBClassHelper.ErrLog("DBClassHelper.ExecuteTrans(string[] sqls):" + ex.Message);
                return false;
            }
            return true;
        }//Execute(string sql)

        public static void WriteLog(string ErrInfo)
        {
            var folderName = "SQLLogs";
            var dir = new System.IO.DirectoryInfo(folderName);
            if (dir.Exists == false)
                dir.Create();
            var sqllog = System.IO.Path.Combine(dir.FullName, string.Format("SQLLog{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            var logText = string.Empty;
            var str = string.Format(@"/r/n数据错误日志 《{0}》/r/n", DateTime.Now.ToString("yyyyMMdd hhmmss"));
            if (System.IO.File.Exists(sqllog))
                logText = System.IO.File.ReadAllText(sqllog);
            System.IO.File.WriteAllText(sqllog, logText + str + ErrInfo);
        }
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="ErrInfo">错误信息</param>
        public static void ErrLog(string ErrInfo)
        {
            WriteLog(ErrInfo);
            throw new Exception("数据库存储出错：" + ErrInfo);
            //KT.NetLogManager.LogManagerRepo logManager= new NetLogManager.LogManagerRepo();
            //logManager.Log("systemErrorLog",NetLogManager.LogLevel.Debug,ErrInfo);
            //string fileName = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"];
            //string isWrite = System.Configuration.ConfigurationManager.AppSettings["ErrorLog"];
            //string dataSourceFile = System.Configuration.ConfigurationManager.AppSettings["DataSourceFile"];
            //if(string.IsNullOrEmpty(fileName))
            //    fileName ="ErrorLog";
            //if (string.IsNullOrEmpty(isWrite))
            //    isWrite = "no";
            //if (isWrite.Trim().ToLower() == "yes")
            //{
            //    //将错误信息写入系统日志
            //   //System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
            //    //log.Source = "NewFrame";
            //    //log.WriteEntry(ErrInfo);
            //    //将错误信息写入记录文件
            //    // StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, fileName), true);
            //    StreamWriter sw = new StreamWriter(Path.Combine(dataSourceFile, fileName), true);
            //    sw.WriteLine(System.DateTime.Now);
            //    sw.WriteLine(ErrInfo);
            //    sw.WriteLine();
            //    sw.Close();
            //  }
        }



    }

}
