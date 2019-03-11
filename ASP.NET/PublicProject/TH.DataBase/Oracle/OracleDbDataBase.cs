using DDTek.Oracle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace TH.DataBase.Oracle
{
    public class OracleDbDataBase : DataBaseBase
    {
        public OracleDbDataBase()
            : base()
        {
 
        }

        public OracleDbDataBase(ServerBase server, string name)
            : base(server, name)
        {

        }
        public override string GetConnectString()
        {
            var builder = new OracleConnectionStringBuilder(Server.GetConnectString());
            return builder.ToString();
        }

        //'U_JG'
        public override ObservableCollection<TableViewBase> GetTables()
        {
            var tables = new ObservableCollection<TableViewBase>();
            var conn = DBClassHelper.OpenConnect(this);
            string strSql = "select table_name from all_all_tables where owner='"+Name+"'";
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(strSql, conn))
            {
                while (sdr.Read())
                {
                    string name = sdr.GetString(0);
                    //string schema = sdr.GetString(2);
                   // DateTime createDate = sdr.GetDateTime(3);
                   // DateTime modifyDate = sdr.GetDateTime(1);
                    tables.Add(new OracleDbTable(this, name)
                    {
                       // Schema = schema,
                       // CreateDate = createDate,
                       // ModifyDate = modifyDate,
                    });
                }
                sdr.Close();
                return tables;
            }
        }

        public override ObservableCollection<TableViewBase> GetViews()
        {
            var views = new ObservableCollection<TableViewBase>();
            string strSql = "select view_Name from all_views  where owner='" + Name + "'";
            var conn = DBClassHelper.OpenConnect(this);
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(strSql, conn))
            {
                while (sdr.Read())
                {
                    string name = sdr.GetString(0);
                 //   string schema = sdr.GetString(2);
                  //  DateTime createDate = sdr.GetDateTime(3);
                    //DateTime modifyDate = sdr.GetDateTime(1);
                    views.Add(new OracleDbView(this, name)
                    {
                       // Schema = schema,
                       // CreateDate = createDate,
                       // ModifyDate = modifyDate,
                    });
                }
                sdr.Close();
                return views;
            }
        }
    }
}
