using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace FD.DataBase
{
    public class AccessHelper
    {

        // string s = Application.StartupPath.Replace(@"\bin\Debug", "") + @"\Data\#test.mdb";  
        ////JonseTool.AccessHelper.ConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + s + @";Persist Security Info=false;User Id=admin;Password=";  
        //JonseTool.AccessHelper.ConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + s + @";";  

        public static string _sConnString = string.Empty;
        public static string ConnString
        {
            get { return _sConnString; }
            set { _sConnString = value; }
        }

        public static void SetConnString4(string filePath)
        {
            string str = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";";
            ConnString = str;
        }

        public static void SetConnString(string filePath)
        {
            string str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + @";";
            ConnString = str;
        }

        public static void SetConnString(string provider, string filePath)
        {
            string str = @"Provider=" + provider + @";Data Source=" + filePath + @";";
            ConnString = str;
        }

        public static bool TestConn(string sConnStr = "")
        {
            OleDbConnection myConn = null;
            bool bResult = false;
            try
            {
                if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;
                myConn = new OleDbConnection(_sConnString);
                myConn.Open();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (myConn != null && myConn.State.ToString() == "Open")
                    bResult = true;
            }

            myConn.Close();

            return bResult;
        }

        //public static bool TestConn(string sConnStr = "")
        //{
        //    OleDbConnection myConn = null;
        //    bool bResult = false;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;
        //        myConn = new OleDbConnection(_sConnString);
        //        if (myConn.State != ConnectionState.Closed)
        //        {
        //            myConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (myConn != null && myConn.State.ToString() == "Open")
        //            bResult = true;
        //    }

        //    myConn.Close();

        //    return bResult;
        //}
        public static DataTable QueryDatas(out string sErr, string sql)
        {
            DataTable dt = new DataTable();
            sErr = string.Empty;
            try
            {
                DataRow dr;
                OleDbConnection accConn = new OleDbConnection(_sConnString);
                accConn.Open();
                OleDbCommand odCommand = accConn.CreateCommand();
                odCommand.CommandText = sql;
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据   
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] =
                        odrReader[odrReader.GetName(i)].ToString();
                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 C#操作Access之读取mdb  
                odrReader.Close();
                accConn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
                return dt;
            }
        }

        public static DataTable QueryAllTableName(string sConnStr = "")
        {
            if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;
            OleDbConnection accConn = null;
            try
            {
                accConn = new OleDbConnection(sConnStr);
                accConn.Open();
                var table = accConn.GetSchema("tables");
                accConn.Close();
                return table;
            }
            catch (Exception ex)
            {
                accConn.Close();
                return null;
            }
        }
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="mdbPath"></param>
        /// <param name="SetName"></param>
        /// <returns></returns>
        public static DataTable setTableFromMdb(string mdbPath, string SetName)
        {
            string errorStr = null;
            AccessHelper.SetConnString4(mdbPath);
            string sqlStr = "select * from  " + SetName + ";";
            var table = AccessHelper.GetDataTable(out errorStr, sqlStr, null);
            return table;
        }
        public static string[] SetMdbTable(string mdbPath)
        {
            List<string> MDBTables = new List<string>();
            //SetConnString(mdbPath);
            //var alltable = AccessHelper.QueryAllTableName();
            //var alltablelist = alltable.Rows;
            //foreach (DataRow atable in alltablelist)
            //{
            //    var type = atable["TABLE_TYPE"].ToString();
            //    if (type == "TABLE")
            //        MDBTables.Add(atable["TABLE_NAME"].ToString());
            //}
            string errorStr = null;
            AccessHelper.SetConnString4(mdbPath);
            var SName = "GDB_GeomColumns";
            string sqlStr = "select TableName from  " + SName + ";";
            var table = AccessHelper.GetDataTable(out errorStr, sqlStr);
            var list = table.Rows;
            foreach (DataRow i in list)
            {
                var type = i["TableName"].ToString();
                MDBTables.Add(type);
            }
            return MDBTables.ToArray();
        }

        public static bool ExcuteSql(string sql, string sConnStr = null)
        {
            if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;
            OleDbConnection accConn = null;
            OleDbCommand accCmd = null;
            try
            {
                accConn = new OleDbConnection(sConnStr);
                accConn.Open();
                accCmd = accConn.CreateCommand();
                accCmd.CommandText = sql;
                accCmd.ExecuteNonQuery();
                accCmd.Dispose();
                accConn.Dispose();
                return true;
            }
            catch
            {
                accCmd.Dispose();
                accConn.Dispose();
                return false;
            }
        }

 
        public static DataTable GetDataTable(out string sErr, string sSQL, string sConnStr = "", params OleDbParameter[] cmdParams)
        {
            DataTable dt = null;
            sErr = string.Empty;

            if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;

            OleDbConnection accConn = null;
            try
            {
                accConn = new OleDbConnection(sConnStr);
                OleDbCommand accCmd = new OleDbCommand(sSQL, accConn);
                accConn.Open();

                if (cmdParams != null)
                {
                    foreach (OleDbParameter parm in cmdParams)
                        accCmd.Parameters.Add(parm);
                }

                OleDbDataAdapter adapter = new OleDbDataAdapter(accCmd);
                dt = new DataTable();
                adapter.Fill(dt);
                accConn.Close();
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }
            return dt;
        }

        // 取dataset    
        public static DataSet GetDataSet(out string sError, string sSQL, string sConnStr = "", params OleDbParameter[] cmdParams)
        {
            DataSet ds = null;
            sError = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;

                OleDbConnection conn = new OleDbConnection(sConnStr);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = sSQL;

                if (cmdParams != null)
                {
                    foreach (OleDbParameter parm in cmdParams)
                        cmd.Parameters.Add(parm);
                }

                OleDbDataAdapter dapter = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                dapter.Fill(ds);
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return ds;
        }

        // 取某个单一的元素     
        public static object GetSingle(out string sError, string sSQL, string sConnStr)
        {
            DataTable dt = GetDataTable(out sError, sSQL, sConnStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0];
            }

            return null;
        }

        // 取最大的ID    
        public static Int32 GetMaxID(out string sError, string sKeyField, string sTableName, string sConnStr = null)
        {
            DataTable dt = GetDataTable(out sError, "select iif(isnull(max([" + sKeyField + "])),0,max([" + sKeyField + "])) as MaxID from [" + sTableName + "]", sConnStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }

            return 0;
        }

        // 执行 insert,update,delete 动作，也可以使用事务    
        public static bool UpdateData(out string sError, string sSQL, string sConnStr = "", OleDbParameter[] cmdParams = null, bool bUseTransaction = false)
        {
            int iResult = 0;
            sError = string.Empty;
            if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;

            if (!bUseTransaction)
            {
                try
                {
                    OleDbConnection conn = new OleDbConnection(sConnStr);
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sSQL;

                    if (cmdParams != null)
                    {
                        foreach (OleDbParameter parm in cmdParams)
                            cmd.Parameters.Add(parm);
                    }

                    iResult = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    iResult = -1;
                }
            }
            else // 使用事务    
            {
                OleDbTransaction trans = null;
                try
                {
                    OleDbConnection conn = new OleDbConnection(sConnStr);
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    trans = conn.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sSQL;

                    if (cmdParams != null)
                    {
                        foreach (OleDbParameter parm in cmdParams)
                            cmd.Parameters.Add(parm);
                    }

                    cmd.Transaction = trans;
                    iResult = cmd.ExecuteNonQuery();
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    iResult = -1;
                    trans.Rollback();
                }
            }

            return iResult > 0;
        }


        public static DataTable QueryAllTableName2(string mdbPath)
        {
            AccessHelper.SetConnString4(mdbPath);
            var sConnStr = _sConnString;
            OleDbConnection accConn = null;
            try
            {
                accConn = new OleDbConnection(sConnStr);
                accConn.Open();
                var table = accConn.GetSchema("tables");
                accConn.Close();
                return table;
            }
            catch (Exception ex)
            {
                accConn.Close();
                return null;
            }
        }

        // 执行 insert,update,delete 动作，也可以使用事务    
        public static bool UpdateData(out string sError, DataTable dtable, string tableName, string sConnStr = "")
        {
            int iResult = 0;
            sError = string.Empty;
            if (string.IsNullOrEmpty(sConnStr)) sConnStr = _sConnString;

            try
            {
                OleDbConnection conn = new OleDbConnection(sConnStr);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from " + tableName + " where 1=0 ", conn);   //建立空表结构
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da); //根据dt修改的情况自动生成updateCommand传递给dataAdapter
                iResult = da.Update(dtable);   //dtable已经初始化
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                iResult = -1;
            }
            return iResult > 0;
        }
    }

}

