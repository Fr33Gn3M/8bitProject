using DDTek.Oracle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.DataBase.Oracle
{
    [DataContract]
  public  class OracleDbServer:ServerBase
    {
        //private OracleConnectionType m_ConnectionType;
        //[DataMember]
        //public OracleConnectionType ConnectionType
        //{
        //    get { return m_ConnectionType; }
        //    set
        //    {
        //        m_ConnectionType = value;
        //        RaisePropertyChanged("IpAddress");

        //    }
        //}

        //private string m_NetServiceName;
        //[DataMember]
        //public string NetServiceName
        //{
        //    get { return m_NetServiceName; }
        //    set
        //    {
        //        m_NetServiceName = value;
        //        RaisePropertyChanged("NetServiceName");

        //    }
        //}

        //private string m_Port;
        //[DataMember]
        //public string Port
        //{
        //    get { return m_Port; }
        //    set
        //    {
        //        m_Port = value;
        //        RaisePropertyChanged("Port");
        //    }
        //}

        private string m_ServiceNameOrSID;
        [DataMember]
        public string ServiceNameOrSID
        {
            get { return m_ServiceNameOrSID; }
            set
            {
                m_ServiceNameOrSID = value;
                RaisePropertyChanged("ServiceNameOrSID");
            }
        }

        //private OleServiceType m_ServiceType;
        //[DataMember]
        //public OleServiceType ServiceType
        //{
        //    get { return m_ServiceType; }
        //    set
        //    {
        //        m_ServiceType = value;
        //        RaisePropertyChanged("ServiceType");
        //    }
        //}

        public override string GetConnectString()
        {
            var builder = new OracleConnectionStringBuilder();
            
            builder.DataSource = ServiceNameOrSID;
            builder.UserID = UserName;
            builder.Password = Password;

            //return "Provider=OraOLEDB.Oracle;"+builder.ToString();
            return builder.ConnectionString;
        }

        public override ObservableCollection<DataBaseBase> GetDatabases()
        {
            var databases = new ObservableCollection<DataBaseBase>();
            var conn = DBClassHelper.OpenConnect(this);
            string sql = "select distinct owner from all_all_tables  ";
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(sql, conn))
            {
                while (sdr.Read())
                {
                    string databaseName = sdr.GetString(0);
                    databases.Add(new OracleDbDataBase(this, databaseName));
                }
                sdr.Close();
            }
            return databases;
        }

    }
}
