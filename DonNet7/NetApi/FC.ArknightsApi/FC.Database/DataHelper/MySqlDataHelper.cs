using FC.Database.BaseHelper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.DataHelper
{
    internal class MySqlDataHelper : IDataHelper
    {
        public MySqlDataHelper(string connectionString) 
        {
            Connection = new MySqlConnection(connectionString);
        }

        public Dictionary<string, object> Get(string resource, int id)
        {
            Connection.Open();
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM `{resource}` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = ReadAll(cmd.ExecuteReader());
            Connection.Close();
            return result.Count > 0 ? result.First() : null;
        }


        private List<Dictionary<string, object>> ReadAll(DbDataReader reader)
        {
            var list = new List<Dictionary<string, object>>();
            using (reader)
            {
                while (reader.Read())
                {
                    var dic = DataReaderToDic(reader);
                    list.Add(dic);
                }
            }
            return list;
        }

        private Dictionary<string,object> DataReaderToDic(IDataReader dataReader)
        {
            var dic = new Dictionary<string, object>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                dic.Add(dataReader.GetName(i), dataReader.GetValue(i));
            }
            return dic;
        }

        public MySqlConnection Connection { get; }
    }
}
