using System.Data.Common;
using System.Data;
using MySqlConnector;

namespace FC.Database.BaseHelper
{
    internal class DBHelper
    {
        private static DBHelper instance;

        public static DBHelper Instance
        {
            get
            {
                if(instance == null)
                    return instance = new DBHelper();
                else
                    return instance;
            }
        }
    
        internal List<Dictionary<string, object>> Execute(DbConnection dbConnection, string sql, List<MySqlParameter> mySqlParameters)
        {
            dbConnection.Open();
            try
            {
                using var cmd = dbConnection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(mySqlParameters.ToArray());
                var result = ReadAll(cmd.ExecuteReader());
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { dbConnection.Close(); }
        }

        internal List<Dictionary<string, object>> BatchExecute(DbConnection dbConnection, string sql, List<MySqlParameter> mySqlParameters)
        {
            dbConnection.Open();
            try
            {
                using var cmd = dbConnection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(mySqlParameters.ToArray());
                var result = ReadAll(cmd.ExecuteReader());
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { dbConnection.Close(); }
        }

        /// <summary>
        /// 数据库语句执行
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>执行结果 字典列表</returns>
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

        /// <summary>
        /// DataReader转Dictionary字典
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Dictionary字典</returns>
        private Dictionary<string, object> DataReaderToDic(IDataReader dataReader)
        {
            var dic = new Dictionary<string, object>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                dic.Add(dataReader.GetName(i), dataReader.GetValue(i));
            }
            return dic;
        }

    }
}
