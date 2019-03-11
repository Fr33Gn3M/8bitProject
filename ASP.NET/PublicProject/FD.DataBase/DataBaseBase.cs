using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.DataBase
{
    public class DataBaseBase : INotifyPropertyChanged
    {
        public DataBaseBase() 
        { }
        public DataBaseBase(ServerBase server,string name)
        {
            m_Server = server;
            Name = name;
        }

        private ServerBase m_Server;
        [DataMember]
        public ServerBase Server
        {
            get { return m_Server; }
            set
            {
                m_Server = value;
                RaisePropertyChanged("Server");
            }
        }

        public string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                RaisePropertyChanged("Name");
            }
        }


        public virtual string GetConnectString()
        {
            return null;
        }

        public virtual ObservableCollection<TableViewBase> GetTables()
        {
            return null;
        }

        public virtual ObservableCollection<TableViewBase> GetViews()
        {
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }
}
