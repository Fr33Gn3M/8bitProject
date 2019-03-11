using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TH.DataBase
{
    public class ModelConvert
    {
        public static Dictionary<string, object>[] ConvertDict<T>(T[] models, bool isconvertPart)
        {
            //获取此模型的公共属性
            var dicList = new List<Dictionary<string, object>>();
            foreach (var item in models)
            {
                PropertyInfo[] propertys = item.GetType().GetProperties();
                var dic = new Dictionary<string, object>();
                foreach (PropertyInfo pi in propertys)
                {
                    var member = pi as MemberInfo;
                    var attributes = member.GetCustomAttributes(typeof(ModelAttrubuteIsParted), false);
                    if (attributes.Length > 0 && (attributes[0] as ModelAttrubuteIsParted).IsParted == true && !isconvertPart)
                    {
                        continue;
                    }
                    var tempName = pi.Name;
                    var value = pi.GetValue(item, null);
                    if (value != null)
                        dic.Add(tempName, value);
                    else
                        dic.Add(tempName, null);
                }
                dicList.Add(dic);
            }
            return dicList.ToArray();
        }


        public static T[] ConvertToModel<T>(Dictionary<string, object>[] dics, bool isconvertPart = false)
        {
            var type = typeof(T);
            //定义集合
            IList<T> ts = new List<T>();
            T t = (T)Activator.CreateInstance(type);
            string tempName = "";
            //获取此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (Dictionary<string, object> dic in dics)
            {
                t = (T)Activator.CreateInstance(type);
                foreach (PropertyInfo pi in propertys)
                {
                    var member = pi as MemberInfo;
                    var attributes = member.GetCustomAttributes(typeof(ModelAttrubuteIsParted), false);
                    if (attributes.Length > 0 && (attributes[0] as ModelAttrubuteIsParted).IsParted == true && !isconvertPart)
                    {
                        continue;
                    }

                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dic.ContainsKey(tempName))
                    {
                        //判断此属性是否有set
                        if (!pi.CanWrite)
                            continue;
                        object value = dic[tempName];
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            if (pi.PropertyType == typeof(bool?) || pi.PropertyType == typeof(Boolean?))
                            {
                                if (value.ToString().Equals("1"))
                                    value = true;
                                if (value.ToString().Equals("0"))
                                    value = false;
                            }
                            if (pi.PropertyType == typeof(THArcGeometry))
                            {
                                var geom = value.ToString();
                                value = geom.DBXMLDeserialize<THArcGeometry>();
                            }
                            else
                                if (pi.PropertyType == typeof(THGeometry))
                                {
                                    if (value is THGeometry)
                                        continue;
                                    if (value is SqlGeometry)
                                    {
                                        var geom = value as SqlGeometry;
                                        value = new THGeometry() { WKT = geom.ToString() };
                                    }
                                    else
                                    {
                                      value  =  Newtonsoft.Json.JsonConvert.DeserializeObject<THGeometry>(value.ToString());
                                    }
                                }
                            if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                                value = DateTime.Parse(value.ToString());
                            if (pi.PropertyType == typeof(Guid) || pi.PropertyType == typeof(Guid?))
                                value = Guid.Parse(value.ToString());
                        }
                        if (value != DBNull.Value)
                        {
                            if (value != null && value.GetType() == typeof(Int64))
                                value = int.Parse(value.ToString());
                            if (pi.PropertyType == typeof(long?))
                                value = long.Parse(value.ToString());
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts.ToArray();
        }
    }
    public class ModelTypeConvert<T> where T : class,new()
    {
        private static Dictionary<Type, DataTable> dicTable = new Dictionary<Type, DataTable>();
        public static IList<T> ConvertToModel(DataTable dt)
        {
            //定义集合
            IList<T> ts = new List<T>();
            T t = new T();
            string tempName = "";
            //获取此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (DataRow row in dt.Rows)
            {
                t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        //判断此属性是否有set
                        if (!pi.CanWrite)
                            continue;
                        object value = row[tempName];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        public static IList<T> ConvertToModelFromType(string nameSpace, string className, DataTable dt, bool isconvertPart = true)
        {
            var typename = nameSpace + "." + className + "," + nameSpace;
            var type = Type.GetType(typename);
            //定义集合
            IList<T> ts = new List<T>();
            T t = (T)Activator.CreateInstance(type);
            string tempName = "";
            //获取此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (DataRow row in dt.Rows)
            {
                t = (T)Activator.CreateInstance(type);
                foreach (PropertyInfo pi in propertys)
                {
                    var member = pi as MemberInfo;
                    var attributes = member.GetCustomAttributes(typeof(ModelAttrubuteIsParted), false);
                    if (attributes.Length > 0 && (attributes[0] as ModelAttrubuteIsParted).IsParted == true && !isconvertPart)
                    {
                        continue;
                    }

                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        //判断此属性是否有set
                        if (!pi.CanWrite)
                            continue;
                        object value = row[tempName];
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            if (pi.PropertyType == typeof(bool?) || pi.PropertyType == typeof(Boolean?))
                            {

                                if (value.ToString().Equals("1"))
                                    value = true;
                                if (value.ToString().Equals("0"))
                                    value = false;
                            }
                            if (pi.PropertyType == typeof(THArcGeometry))
                            {
                                var geom = value.ToString();
                                value = geom.DBXMLDeserialize<THArcGeometry>();
                            }
                            else
                                if (pi.PropertyType == typeof(THGeometry))
                                {
                                    var geom = value as SqlGeometry;
                                    value = new THGeometry() { WKT = geom.ToString() };
                                }
                            if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                                value = DateTime.Parse(value.ToString());
                            if (pi.PropertyType == typeof(Guid) || pi.PropertyType == typeof(Guid?))
                                value = Guid.Parse(value.ToString());
                        }
                        if (value != DBNull.Value)
                        {
                            if (value != null && (value.GetType() == typeof(Int64)||(value.GetType() == typeof(Int64?))))
                                value = int.Parse(value.ToString());
                            pi.SetValue(t, value, null);
                        }
                        //if (value != DBNull.Value)
                        //{
                        //    pi.SetValue(t, value, null);
                        //}
                    }
                }
                ts.Add(t);
            }
            return ts;
        }



        public static DataRow ConvertToDataRow(T model)
        {
            string tempName = "";
            DataTable dt;
            //获取此模型的公共属性
            PropertyInfo[] propertys = model.GetType().GetProperties();
            if (dicTable.ContainsKey(model.GetType()))
            {
                dt = dicTable[model.GetType()];
            }
            else
            {
                dt = new DataTable();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    dt.Columns.Add(tempName);
                }
            }
            DataRow t = dt.NewRow();
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;
                var value = pi.GetValue(model, null);
                t[tempName] = value;
            }
            return t;
        }

        public static T[] ConvertToModel(Dictionary<string, object>[] dics, bool isconvertPart = false)
        {
            var type = typeof(T);
            //定义集合
            IList<T> ts = new List<T>();
            T t = (T)Activator.CreateInstance(type);
            string tempName = "";
            //获取此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (Dictionary<string, object> dic in dics)
            {
                t = (T)Activator.CreateInstance(type);
                foreach (PropertyInfo pi in propertys)
                {
                    var member = pi as MemberInfo;
                    var attributes = member.GetCustomAttributes(typeof(ModelAttrubuteIsParted), false);
                    if (attributes.Length > 0 && (attributes[0] as ModelAttrubuteIsParted).IsParted == true && !isconvertPart)
                    {
                        continue;
                    }

                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dic.ContainsKey(tempName))
                    {
                        //判断此属性是否有set
                        if (!pi.CanWrite)
                            continue;
                        object value = dic[tempName];
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            if (pi.PropertyType == typeof(bool?) || pi.PropertyType == typeof(Boolean?))
                            {
                                if (value.ToString().Equals("1"))
                                    value = true;
                                if (value.ToString().Equals("0"))
                                    value = false;
                            }
                            if (pi.PropertyType == typeof(THArcGeometry))
                            {
                                var geom = value.ToString();
                                value = geom.DBXMLDeserialize<THArcGeometry>();
                            }
                            else
                                if (pi.PropertyType == typeof(THGeometry))
                                {
                                    var geom = value as SqlGeometry;
                                    value = new THGeometry() { WKT = geom.ToString() };
                                }

                            if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                                value = DateTime.Parse(value.ToString());
                            if (pi.PropertyType == typeof(Guid) || pi.PropertyType == typeof(Guid?))
                                value = Guid.Parse(value.ToString());
                        }
                        if (value != DBNull.Value)
                        {
                            if (value != null && value.GetType() == typeof(Int64))
                                value = int.Parse(value.ToString());
                            pi.SetValue(t, value, null);
                        }
                        //if (value != DBNull.Value)
                        //{
                        //    pi.SetValue(t, value, null);
                        //}
                    }
                }
                ts.Add(t);
            }
            return ts.ToArray();
        }

        public static void UpdateMdbModelInfo(DataRow row, Dictionary<string, string> modelToMdb, T bdc, bool isconvertPart = true)
        {
            var MdbModel = ModelTypeConvert<T>.ConvertDataRow(row);
            UpdateMdbModelInfo(MdbModel, modelToMdb, bdc, isconvertPart);
        }

        public static void UpdateMdbModelInfo(Dictionary<string, object> MdbModel, Dictionary<string, string> modelToMdb, T bdc, bool isconvertPart = true)
        {
            var modelDic = ModelTypeConvert<T>.ConvertDict(bdc, isconvertPart);
            foreach (var item in modelToMdb)
            {
                if (item.Key.Equals("YSDM"))
                    MdbModel[item.Key] = item.Value;
                if (modelDic.ContainsKey(item.Value))
                    MdbModel[item.Key] = modelDic[item.Value];
            }
        }

        public static Dictionary<string, object> ConvertDataRow(DataRow row)
        {
            var dic = new Dictionary<string, object>();
            foreach (DataColumn item in row.Table.Columns)
                dic.Add(item.ColumnName, row[item.ColumnName]);
            return dic;
        }

        public static void UpdateTableRow(ref DataRow row, Dictionary<string, string> modelToMdb, T bdc, bool isconvertPart = true)
        {
            var modelDic = ModelTypeConvert<T>.ConvertDict(bdc, isconvertPart);
            foreach (var item in modelToMdb)
            {
                if (item.Key.Equals("YSDM"))
                {
                    if (item.Value == null)
                        row[item.Key] = DBNull.Value;
                    else
                        row[item.Key] = item.Value;
                }
                if (modelDic.ContainsKey(item.Value))
                {
                    if (modelDic[item.Value] == null)
                        row[item.Key] = DBNull.Value;
                    else
                        row[item.Key] = modelDic[item.Value];
                }
            }
        }

        public static Dictionary<string, object> ConvertDict(T model, bool isconvertPart)
        {
            string tempName = "";
            DataTable dt;
            //获取此模型的公共属性
            PropertyInfo[] propertys = model.GetType().GetProperties();
            if (dicTable.ContainsKey(model.GetType()))
            {
                dt = dicTable[model.GetType()];
            }
            else
            {
                dt = new DataTable();
                foreach (PropertyInfo pi in propertys)
                {
                    var member = pi as MemberInfo;
                    var attributes = member.GetCustomAttributes(typeof(ModelAttrubuteIsParted), false);
                    if (attributes.Length > 0 && (attributes[0] as ModelAttrubuteIsParted).IsParted == true && !isconvertPart)
                    {
                        continue;
                    }
                    tempName = pi.Name;
                    dt.Columns.Add(tempName);
                }
            }
            var dic = new Dictionary<string, object>();
            foreach (PropertyInfo pi in propertys)
            {
                var member = pi as MemberInfo;
                var attributes = member.GetCustomAttributes(typeof(ModelAttrubuteIsParted), false);
                if (attributes.Length > 0 && (attributes[0] as ModelAttrubuteIsParted).IsParted == true && !isconvertPart)
                {
                    continue;
                }
                tempName = pi.Name;
                var value = pi.GetValue(model, null);
                if (value != null)
                    dic.Add(tempName, value);
                else
                    dic.Add(tempName, null);
            }
            return dic;
        }

        public static SqlTableInfo GetSqlTableInfo(T model, Dictionary<string, string> DataBaseKyFieldTableDic, bool isnotconvertPart = false)
        {
            var typeName = model.GetType().UnderlyingSystemType.Name;
            var dic = ConvertDict(model, isnotconvertPart);
            var sql = new SqlTableInfo();
            sql.TableName = typeName;
            sql.BaseType = model.GetType().UnderlyingSystemType;
            if (DataBaseKyFieldTableDic.ContainsKey(typeName))
                sql.KeyFieldName = DataBaseKyFieldTableDic[typeName];
            sql.Fields = dic;
            return sql;
        }

        public static SqlTableInfo[] GetSqlList(T[] models, Dictionary<string, string> DataBaseKyFieldTableDic, bool isnotconvertPart = false)
        {
            var list = new List<SqlTableInfo>();
            foreach (var item in models)
            {
                var sql = GetSqlTableInfo(item, DataBaseKyFieldTableDic, isnotconvertPart);
                list.Add(sql);
            }
            return list.ToArray();
        }




    }
}
