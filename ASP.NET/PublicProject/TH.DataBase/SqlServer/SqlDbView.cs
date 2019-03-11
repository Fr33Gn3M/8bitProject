using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace TH.DataBase.SqlServer
{
    public class SqlDbView : TableViewBase
    {
        public SqlDbView()
            : base()
        {

        }

        public SqlDbView(DataBaseBase db, string name)
            : base(db, name)
        {

        }
        public override ObservableCollection<Column> GetColumns()
        {
            ObservableCollection<Column> columns = new ObservableCollection<Column>();
            var conn = DBClassHelper.OpenConnect(this.Database);
            string strSql = string.Format("SELECT c.name, [tablename]=t.name," +
                                  "[Type]=(select name from sys.types where sys.types.user_type_id=c.user_type_id)," +
                                  "[Precision] = columnproperty(t.object_id,c.name,'Precision'), c.Scale,c.is_nullable," +
                                  "c.object_id,c.column_id" +
                          " FROM sys.columns c join sys.views t on c.object_id=t.object_id " +
                          " WHERE t.name='{0}'", Name);
            using (IDataReader sdr = DBClassHelper.ExecuteQuery(strSql, conn))
            {
                while (sdr.Read())
                {
                    Column column = new Column
                    {
                        ColumnName = sdr.GetString(0),
                        TableName = sdr.GetString(1),
                        TypeName = sdr.GetString(2),
                        Precision = sdr.GetInt32(3),
                        Scale = sdr.GetByte(4),
                        IsNull = sdr.GetBoolean(5),
                        Description = GetColumnDescription(sdr.GetInt32(6), sdr.GetInt32(7))
                    };
                    columns.Add(column);
                }
                sdr.Close();
                conn.Close();
                return columns;
            }
        }

        protected string GetColumnDescription(int objectId, int columnId)
        {
            string description = null;
            var conn = DBClassHelper.OpenConnect(this.Database);
            string strSql = string.Format("SELECT value FROM sys.extended_properties WHERE class=1 and major_id={0} and minor_id={1} and name='MS_Description'", objectId, columnId);
            object result = DBClassHelper.ExecuteQuery(strSql, conn);
            if (!(result is DBNull) && result != null)
                description = result.ToString();
            conn.Close();
            return description;
        }

        public override string GetQueryAllStringSql()
        {
            return string.Format("select * from {0} ", Name);
        }

        public override string GetDeleteStringSql()
        {
            return null;
        }

        public override string GetQueryWorkTimeSql(string fieldName, DateTime value)
        {
            return string.Format("Select * from {0} where {1} > {2}", Name, fieldName, value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public override string GetQuerySql(string whereStr)
        {
            return base.GetQuerySql(whereStr);
        }

        public override string GetQueryIsNullSql(string fieldName)
        {
            return string.Format("Select * from {0} where {1} is null ", Name, fieldName);
        }
    }
}
