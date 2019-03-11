using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TH.DataBase
{
    public class TableViewBase
    {
        public TableViewBase()
        {
          
        }

        public TableViewBase(DataBaseBase db,string name)
        {
            Database = db;
            Name = name;
        }

        public DataBaseBase Database { get; set; }

        public string Name { get; set; }

        public string Schema { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Description { get; set; }

        public ObservableCollection<Column> Columns { get { return GetColumns(); } }

        public int RowCount { get { return GetRowCount(); } }

        public virtual ObservableCollection<Column> GetColumns()
        {
            return null;
        }

        public virtual int GetRowCount()
        {
            return 0;
        }

        public virtual string GetQueryAllStringSql()
        {
            return null;
        }

        public virtual string GetDeleteStringSql()
        {
            return null;
        }

        public virtual string GetQueryWorkTimeSql(string fieldName, DateTime value)
        {
            return null;
        }

        public virtual List<string> GetPKColumnName()
        {
            return null;
        }

        public virtual string GetQuerySql(string whereStr)
        {
            if (string.IsNullOrEmpty(whereStr))
                return GetQueryAllStringSql();
            return string.Format("Select * from {0} where {1} ",Name, whereStr);
        }

        public virtual string GetQueryIsNullSql(string fieldName)
        {
            return null;
        }
    }
}
