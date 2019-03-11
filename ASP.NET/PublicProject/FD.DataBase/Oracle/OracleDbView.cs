using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace TH.DataBase.Oracle
{
    public class OracleDbView : TableViewBase
    {
        public OracleDbView()
            : base()
        {

        }

        public OracleDbView(DataBaseBase db, string name)
            : base(db, name)
        {

        }
        public override ObservableCollection<Column> GetColumns()
        {
            ObservableCollection<Column> columns = new ObservableCollection<Column>();
            var conn = DBClassHelper.OpenConnect(this.Database.Server);
            string strSql = string.Format("SELECT  column_name,table_name,data_type,data_length,nullable FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = '{0}' AND OWNER='{1}' ORDER BY COLUMN_ID", Name, Database.Name);
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
                        IsNull = sdr.GetString(4) == "Y" ? true : false,
                    };
                    columns.Add(column);
                }
                sdr.Close();
                return columns;
            }
        }

        public override string GetQueryAllStringSql()
        {
            return string.Format("select * from {0} ", Database.Name+"."+ Name);
        }

        public override string GetQueryWorkTimeSql(string fieldName, DateTime value)
        {
            return string.Format("Select * from {0} where  {1} > TO_DATE('{2}','yyyy-MM-dd')", Database.Name + "." + Name, fieldName, value.ToString("yyyy-MM-dd"));
        }

        public override string GetQuerySql(string whereStr)
        {
          return   base.GetQuerySql(whereStr);
        }


        public override string GetQueryIsNullSql(string fieldName)
        {
            return string.Format("Select * from {0} where {1} is null ", Name, fieldName);
        }
    }
}
