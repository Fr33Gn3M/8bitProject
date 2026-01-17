using System;

namespace Sys.DataBase
{
    public class SqlField
    {
        private string fieldName;
        public string FieldName
        {
            get { return string.Format("{0}", fieldName); }
        }

        private SqlPrividerType m_SqlPrividerType;
        public SqlPrividerType SqlPrividerType
        {
            get { return m_SqlPrividerType; }
        }

        public string FieldNameOriginal
        {
            get { return fieldName; }
        }

        private object fieldValue;

        public string FieldValue
        {
            get
            {
                if (fieldValue is string)
                {
                    //if (QueryPageFilter.IsSqlFilter(fieldValue) == true)
                    //    throw new Exception("数据中含SQL注入，请误使用！");
                    string str = fieldValue.ToString();
                    if (str.IndexOf("'") > -1)
                    {
                        str = str.Replace("'", "''");
                    }
                    return string.Format("'{0}'", str);
                }
                if (fieldValue is DBNull || fieldValue is DateTime || fieldValue is Guid)
                {
                    return string.Format("'{0}'", fieldValue.ToString());
                }
                if (fieldValue is THArcGeometry)
                {
                    var value = fieldValue as THArcGeometry;
                    var str = "'" + value.DBXMLSerializer<THArcGeometry>() + "'";
                    return str;
                }
                else
                if (fieldValue is THGeometry)
                {
                    var value = fieldValue as THGeometry;
                    if (SqlPrividerType == DataBase.SqlPrividerType.SqlClient)
                    {
                        var str = string.Format(" geometry::STGeomFromText('{0}',{1}).MakeValid() ", value.WKT, value.SRID);
                        return str;
                    }
                    if (SqlPrividerType == DataBase.SqlPrividerType.MySqlClient)
                    {
                        var str = string.Format(" geomFromText('{0}') ", value.WKT);
                        return str;
                    }
                }
                if (fieldValue is Boolean)
                {
                    bool t = (Boolean)fieldValue;
                    return (t ? 1.ToString() : 0.ToString());
                }
                else
                {
                    if (fieldValue == null)
                        return null;
                    return fieldValue.ToString();
                }

            }
        }

        //public SqlField(string fieldName, object fieldValue)
        //{
        //    this.fieldName = fieldName;
        //    this.fieldValue = fieldValue;
        //}

        public SqlField(string fieldName, object fieldValue, SqlPrividerType type)
        {
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
            m_SqlPrividerType = type;
        }


        public string GetKeyEqualsValueString()
        {
            if (FieldValue == null)
                return string.Format(" {0} = null ", FieldName);
            else
                return string.Format(" {0} = {1} ", FieldName, FieldValue);
        }

        public string GetPostgresKeyEqualsValueString()
        {
            string key = FieldName;
            if (key.Contains("[") && key.Contains("]"))
            {
                key = key.Replace("[", "");
                key = key.Replace("]", "");
            }
            return string.Format(" \"{0}\" = {1} ", key, FieldValue);
        }


    }
}
