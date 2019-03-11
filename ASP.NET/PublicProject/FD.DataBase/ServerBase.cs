using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.DataBase
{
    [DataContract]
    [KnownType(typeof(SqlServer.SqlDbServer))]
    [KnownType(typeof(Oracle.OracleDbServer))]
    public class ServerBase : INotifyPropertyChanged
    {
        private string m_ServiceName;
        [DataMember]
        public string ServiceName
        {
            get { return m_ServiceName; }
            set
            {
                m_ServiceName = value;
                RaisePropertyChanged("ServiceName");
            }
        }
        private string m_IpAddress;
        [DataMember]
        public string IpAddress
        {
            get { return m_IpAddress; }
            set
            {
                m_IpAddress = value;
                RaisePropertyChanged("IpAddress");
            }
        }
        private string m_UserName;

        [DataMember]
        public string UserName
        {
            get { return m_UserName; }
            set
            {
                m_UserName = value;
                RaisePropertyChanged("UserName");
            }
        }
        private string m_Password;
        [DataMember]
        public string Password
        {
            get { return m_Password; }
            set
            {
                m_Password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string m_ProviderName;
        [DataMember]
        public string ProviderName
        {
            get { return m_ProviderName; }
            set
            {
                m_ProviderName = value;
                RaisePropertyChanged("ProviderName");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        public virtual string GetConnectString()
        {
            return null;
        }

        public virtual ObservableCollection<DataBaseBase> GetDatabases()
        {
            return null;
        }

    }

   [DataContract]
    public class ServerBaseCollection
    {
        [DataMember]
        public ServerBase[] Servers { get; set; }
    }

}
