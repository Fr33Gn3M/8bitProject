using System.Runtime.Serialization;

namespace DataBase
{
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
