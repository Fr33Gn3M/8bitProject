     using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DAL
{

    [DataContract]
    public enum AuthenticationType
    {
        [EnumMember]
        SQLService,
        [EnumMember]
        Windows
    }

    [DataContract]
    public enum OleServiceType
    {
        [EnumMember]
        ServiceName,
        [EnumMember]
        SID,
        [EnumMember]
        None

    }

    [DataContract]
    public enum OracleConnectionType
    {
        [EnumMember]
        Basic,
        [EnumMember]
        TNS
    }


    public enum SqlPrividerType
    {
        [EnumMember]
        SqlClient,
        [EnumMember]
        OracleClient,
        [EnumMember]
        Sqlite,
        [EnumMember]
        MySqlClient
    }
  
}
