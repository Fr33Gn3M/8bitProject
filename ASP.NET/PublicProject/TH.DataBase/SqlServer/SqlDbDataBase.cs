using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TH.DataBase.SqlServer
{
    public class SqlDbDataBase : DataBaseBase
    {
        public SqlDbDataBase()
            : base()
        {

        }

        public SqlDbDataBase(ServerBase server, string name)
            : base(server, name)
        {

        }

        public override string GetConnectString()
        {
            var builder = new SqlConnectionStringBuilder(Server.GetConnectString());
            builder.InitialCatalog = Name;
            return builder.ToString();
        }


        public override ObservableCollection<TableViewBase> GetTables()
        {
            var tables = new ObservableCollection<TableViewBase>();
            var conn = DBClassHelper.OpenConnect(this);
            string strSql = "SELECT t.object_id,t.name as tablename,s.name as schemaname,t.create_date,t.modify_date FROM sys.tables t JOIN sys.schemas s ON t.schema_id=s.schema_id";
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(strSql, conn))
            {
                while (sdr.Read())
                {
                    int objectId = sdr.GetInt32(0);
                    string name = sdr.GetString(1);
                    string schema = sdr.GetString(2);
                    DateTime createDate = sdr.GetDateTime(3);
                    DateTime modifyDate = sdr.GetDateTime(4);
                    string description = GetTableDescription(objectId);
                    tables.Add(new SqlDbTable(this, name)
                    {
                        Schema = schema,
                        CreateDate = createDate,
                        ModifyDate = modifyDate,
                        Description = description
                    });
                }
                sdr.Close();
                conn.Close();
                return tables;
            }
        }

        private string GetTableDescription(int objectId)
        {
            string description = null;
            string strSql = string.Format("SELECT value FROM sys.extended_properties WHERE class=1 and major_id={0} and minor_id=0 and name='MS_Description'", objectId);
            var conn = DBClassHelper.OpenConnect(this);
            object result = DBClassHelper.ExecuteQuery(strSql, conn);
            if (!(result is DBNull) && result != null)
                description = result.ToString();
            conn.Close();
            return description;
        }

        public override ObservableCollection<TableViewBase> GetViews()
        {
            var views = new ObservableCollection<TableViewBase>();
            string strSql = "SELECT v.object_id,v.name as tablename,s.name as schemaname,v.create_date,v.modify_date FROM sys.views v JOIN sys.schemas s ON v.schema_id=s.schema_id";
            var conn = DBClassHelper.OpenConnect(this);
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(strSql, conn))
            {
                while (sdr.Read())
                {
                    int objectId = sdr.GetInt32(0);
                    string name = sdr.GetString(1);
                    string schema = sdr.GetString(2);
                    DateTime createDate = sdr.GetDateTime(3);
                    DateTime modifyDate = sdr.GetDateTime(4);
                    string description = GetTableDescription(objectId);
                    views.Add(new SqlDbView(this, name)
                    {
                        Schema = schema,
                        CreateDate = createDate,
                        ModifyDate = modifyDate,
                        Description = description
                    });
                }
                sdr.Close();
                conn.Close();
                return views;
            }
        }
    }
}
