using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.DataBase.SqlServer
{
    [DataContract]
    public class SqlDbServer : ServerBase
    {
        private AuthenticationType m_Authentication;
        [DataMember]
        public AuthenticationType Authentication
        {
            get { return m_Authentication; }
            set
            {
                m_Authentication = value;
                RaisePropertyChanged("Authentication");
            }
        }

        public override string GetConnectString()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = IpAddress;
            if (Authentication == AuthenticationType.Windows)
            {
                builder.IntegratedSecurity=true;
            }
            else
            {
                builder.UserID = UserName;
                builder.Password = Password;
            }
            return builder.ToString();
        }

        public override ObservableCollection<DataBaseBase> GetDatabases()
        {
            var databases = new ObservableCollection<DataBaseBase>();
           var conn = DBClassHelper.OpenConnect(this);
           string sql =  "SELECT dtb.name AS [Name] FROM sys.databases AS dtb ORDER BY [Name] ASC";
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(sql,conn))
            {
                while (sdr.Read())
                {
                    string databaseName = sdr.GetString(0);
                    if (!databaseName.Equals("master", StringComparison.OrdinalIgnoreCase) &&
                        !databaseName.Equals("model", StringComparison.OrdinalIgnoreCase) &&
                        !databaseName.Equals("msdb", StringComparison.OrdinalIgnoreCase) &&
                        !databaseName.Equals("tempdb", StringComparison.OrdinalIgnoreCase))
                    {
                        databases.Add(new SqlDbDataBase(this, databaseName));
                    }
                }
                sdr.Close();
            }
            conn.Close();
            return databases;
        }
    }

}
