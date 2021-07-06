using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase
{
    /// <summary>
    /// KeyValueList 的摘要说明。
    /// </summary>
    public class SqlFieldList
    {
        public SqlPrividerType PrividerType = SqlPrividerType.SqlClient;
        private ArrayList NonPrimaryFieldList = new ArrayList();

        public SqlFieldList(string TableName)
        {
            tableName = TableName;
        }

        private SqlField primaryField;
        public SqlField PrimaryField
        {
            set { primaryField = value; }
            get { return primaryField; }
        }

        private SqlField addStatusField;
        private SqlField AddStatusField
        {
            set { addStatusField = value; }
            get { return addStatusField; }
        }

        private SqlField updateStatusField;
        private SqlField UpdateStatusField
        {
            set { updateStatusField = value; }
            get { return updateStatusField; }
        }

        public void SetStatusField(SqlField addStatus, SqlField updateStatus)
        {
            addStatusField = addStatus;
            updateStatusField = updateStatus;
        }

        private string tableName;

        public void AddNonPrimaryField(string fieldName, object fieldValue)
        {
            NonPrimaryFieldList.Add(new SqlField(fieldName, fieldValue,PrividerType));
        }
        #region Sql操作
        public string GetSelectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT 'X' FROM ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(primaryField.GetKeyEqualsValueString());
            return stringBuilder.ToString();
        }

        public string GetUpdateString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" UPDATE ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" SET ");
            foreach (SqlField field in NonPrimaryFieldList)
            {
                if (updateStatusField != null && field.FieldName == updateStatusField.FieldName)
                    continue;
                stringBuilder.Append(" ");
                stringBuilder.Append(field.GetKeyEqualsValueString());
                stringBuilder.Append(",");
            }
            if (updateStatusField != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(updateStatusField.GetKeyEqualsValueString());
                stringBuilder.Append(",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1); //去掉最后一个 , 
            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(primaryField.GetKeyEqualsValueString());
            return stringBuilder.ToString();

        }

        public string GetReplaceString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" REPLACE INTO ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" ");

            StringBuilder sb1 = new StringBuilder();
            sb1.Append("(");

            StringBuilder sb2 = new StringBuilder();
            sb2.Append("(");

            foreach (SqlField field in NonPrimaryFieldList)
            {
                if (addStatusField != null && field.FieldName == addStatusField.FieldName)
                    continue;
                sb1.Append(" ");
                sb1.Append(field.FieldName);
                sb1.Append(",");

                sb2.Append(" ");
                if (field.FieldValue == null)
                    sb2.Append(" null ");
                else
                    sb2.Append(field.FieldValue);
                sb2.Append(",");
            }

            if (addStatusField != null)
            {
                sb1.Append(" ");
                sb1.Append(addStatusField.FieldName);
                sb1.Append(",");

                sb2.Append(" ");
                if (addStatusField.FieldValue == null)
                    sb2.Append(" null ");
                else
                    sb2.Append(addStatusField.FieldValue);
                sb2.Append(",");
            }

            sb1.Remove(sb1.Length - 1, 1);
            sb2.Remove(sb2.Length - 1, 1);
            sb1.Append(" ");
            //sb1.Append(primaryField.FieldName);
            //sb2.Append(primaryField.FieldValue);
            sb1.Append(")");
            sb2.Append(")");

            stringBuilder.Append(sb1.ToString());
            stringBuilder.Append(" VALUES ");
            stringBuilder.Append(sb2.ToString());
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

        public string GetInsertString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" INSERT INTO ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" ");

            StringBuilder sb1 = new StringBuilder();
            sb1.Append("(");


            StringBuilder sb2 = new StringBuilder();
            sb2.Append("(");

            foreach (SqlField field in NonPrimaryFieldList)
            {
                if (addStatusField != null && field.FieldName == addStatusField.FieldName)
                    continue;
                sb1.Append(" ");
                sb1.Append(field.FieldName);
                sb1.Append(",");

                sb2.Append(" ");
                if (field.FieldValue == null)
                    sb2.Append(" null ");
                else
                    sb2.Append(field.FieldValue);
                sb2.Append(",");
            }

            if (addStatusField != null)
            {
                sb1.Append(" ");
                sb1.Append(addStatusField.FieldName);
                sb1.Append(",");

                sb2.Append(" ");
                if (addStatusField.FieldValue == null)
                    sb2.Append(" null ");
                else
                    sb2.Append(addStatusField.FieldValue);
                sb2.Append(",");
            }

            sb1.Remove(sb1.Length - 1, 1);
            sb2.Remove(sb2.Length - 1, 1);
            sb1.Append(" ");
            //sb1.Append(primaryField.FieldName);
            //sb2.Append(primaryField.FieldValue);
            sb1.Append(")");
            sb2.Append(")");

            stringBuilder.Append(sb1.ToString());
            stringBuilder.Append(" VALUES ");
            stringBuilder.Append(sb2.ToString());

            return stringBuilder.ToString();
        }

        public string GetDeleteString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" DELETE FROM  ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(primaryField.GetKeyEqualsValueString());
            return stringBuilder.ToString();
        }
        #endregion

        public string GetBannar()
        {
            return PrimaryField.GetKeyEqualsValueString();
        }

        public static string GetSqlString(SqlFieldList sqlFieldList)
        {
            StringBuilder sb = new StringBuilder();
            if (sqlFieldList.PrividerType == SqlPrividerType.SqlClient)
            {
                if (sqlFieldList.PrimaryField == null)
                    return sqlFieldList.GetInsertString();
                sb.Append(" IF EXISTS (");
                sb.Append(Environment.NewLine);
                sb.Append(sqlFieldList.GetSelectString());
                sb.Append(Environment.NewLine);
                sb.Append(") ");
                sb.Append(Environment.NewLine);
                //sb.Append(" BEGIN ") ;
                sb.Append(Environment.NewLine);
                sb.Append(sqlFieldList.GetUpdateString());
                sb.Append(Environment.NewLine);
                //sb.Append(" END ") ;

                sb.Append(Environment.NewLine);
                sb.Append(" ELSE ");
                sb.Append(Environment.NewLine);
                // sb.Append(Environment.NewLine);

                sb.Append(sqlFieldList.GetInsertString());

                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
            else
                if (sqlFieldList.PrividerType == SqlPrividerType.MySqlClient || sqlFieldList.PrividerType == SqlPrividerType.Sqlite)
                {
                    sb.Append(sqlFieldList.GetReplaceString());
                    sb.Append(Environment.NewLine);
                    return sb.ToString();
                }
                else
                {
                    return null;
                }
        }
        /// <summary>
        /// 新增sql拼接
        /// </summary>
        /// <param name="sqlFieldList"></param>
        /// <returns></returns>
        public static string GetAddSqlString(SqlFieldList sqlFieldList)
        {
            StringBuilder sb = new StringBuilder();
            if (sqlFieldList.PrividerType == SqlPrividerType.SqlClient)
            {
                if (sqlFieldList.PrimaryField == null)
                    return sqlFieldList.GetInsertString();
                //sb.Append(" IF EXISTS (");
                //sb.Append(Environment.NewLine);
                //sb.Append(sqlFieldList.GetSelectString());
                //sb.Append(Environment.NewLine);
                //sb.Append(") ");
                //sb.Append(Environment.NewLine);
                ////sb.Append(" BEGIN ") ;
                //sb.Append(Environment.NewLine);
                //sb.Append(sqlFieldList.GetUpdateString());
                //sb.Append(Environment.NewLine);
                ////sb.Append(" END ") ;

                //sb.Append(Environment.NewLine);
                //sb.Append(" ELSE ");
                //sb.Append(Environment.NewLine);
                // sb.Append(Environment.NewLine);

                sb.Append(sqlFieldList.GetInsertString());

                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
            else
                if (sqlFieldList.PrividerType == SqlPrividerType.MySqlClient || sqlFieldList.PrividerType == SqlPrividerType.Sqlite)
            {
                sb.Append(sqlFieldList.GetReplaceString());
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新sql拼接
        /// </summary>
        /// <param name="sqlFieldList"></param>
        /// <returns></returns>
        public static string GetUpdateSqlString(SqlFieldList sqlFieldList)
        {
            StringBuilder sb = new StringBuilder();
            if (sqlFieldList.PrividerType == SqlPrividerType.SqlClient)
            {
                if (sqlFieldList.PrimaryField == null)
                    return sqlFieldList.GetInsertString();
                //sb.Append(" IF EXISTS (");
                //sb.Append(Environment.NewLine);
                //sb.Append(sqlFieldList.GetSelectString());
                //sb.Append(Environment.NewLine);
                //sb.Append(") ");
                //sb.Append(Environment.NewLine);
                ////sb.Append(" BEGIN ") ;
                //sb.Append(Environment.NewLine);
                sb.Append(sqlFieldList.GetUpdateString());
                sb.Append(Environment.NewLine);
                //sb.Append(" END ") ;

                //sb.Append(Environment.NewLine);
                //sb.Append(" ELSE ");
                //sb.Append(Environment.NewLine);
                //sb.Append(Environment.NewLine);

                //sb.Append(sqlFieldList.GetInsertString());

                //sb.Append(Environment.NewLine);
                return sb.ToString();
            }
            else
                if (sqlFieldList.PrividerType == SqlPrividerType.MySqlClient || sqlFieldList.PrividerType == SqlPrividerType.Sqlite)
            {
                sb.Append(sqlFieldList.GetReplaceString());
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string GetPostgresFieldName(string fieldName)
        {
            var fieldname = fieldName;
            if (fieldname.Contains("[") && fieldname.Contains("]"))
            {
                fieldname = fieldname.Replace("[", "");
                fieldname = fieldname.Replace("]", "");
            }
            return "\"" + fieldname + "\'";
        }
    }
}
