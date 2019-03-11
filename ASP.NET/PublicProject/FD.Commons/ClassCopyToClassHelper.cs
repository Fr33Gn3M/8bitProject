using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FD.Commons
{
    public class AttributeIsParted : Attribute
    {
        public bool IsParted { get; set; }
        public AttributeIsParted()
        {
            IsParted = true;
        }
        public AttributeIsParted(bool ischanged)
        {
            IsParted = ischanged;
        }
    }

    public static class ClassCopyToClassHelper
    {
        public static void CopyToSelf<TTarget>(this TTarget obj, TTarget source) where TTarget : class
        {
            PropertyInfo[] propertys = source.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                var tempName = pi.Name;
                if (!pi.CanWrite)
                    continue;
                object value = pi.GetValue(source, null);
                if (value != DBNull.Value)
                    pi.SetValue(obj, value, null);
            }
        }

        public static void SetValue<TTarget>(this TTarget obj, Dictionary<string,object> values) where TTarget : class
        {
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if(values.ContainsKey(pi.Name))
                {
                    if (!pi.CanWrite)
                        continue;
                    object value = pi.GetValue(obj, null);
                    if (value != DBNull.Value)
                        pi.SetValue(obj, values[pi.Name], null);
                }
            }
        }

        public static void CopyToAnother<TTarget>(TTarget obj, Object source)
        {
            PropertyInfo[] propertys = source.GetType().GetProperties();
            PropertyInfo[] propertys2 = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                var tempName = pi.Name;
                if (!pi.CanWrite)
                    continue;
                object value = pi.GetValue(source, null);
                if (value != DBNull.Value)
                {
                    foreach (var item in propertys2)
                    {
                        if (item.Name == tempName)
                        {
                            item.SetValue(obj, value, null);
                            continue;
                        }
                    }
                }
            }
        }

        public static void CopyToPartSelf<TTarget>(this TTarget obj, TTarget source) where TTarget : class
        {
            PropertyInfo[] propertys = source.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                var member = pi as MemberInfo;
                var attributes = member.GetCustomAttributes(typeof(AttributeIsParted), false);
                if (attributes.Length > 0 && (attributes[0] as AttributeIsParted).IsParted == true)
                {
                    continue;
                }
                var tempName = pi.Name;
                if (!pi.CanWrite)
                    continue;
                object value = pi.GetValue(source, null);
                if (value != DBNull.Value)
                    pi.SetValue(obj, value, null);
            }
        }

        public static bool ObjIsSame<TTarget>(this TTarget obj, TTarget source) where TTarget : class
        {
            bool result = true;
            PropertyInfo[] propertys = source.GetType().GetProperties();
            PropertyInfo[] propertys2 = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                var member = pi as MemberInfo;
                var attributes = member.GetCustomAttributes(typeof(AttributeIsParted), false);
                if (attributes.Length > 0 && (attributes[0] as AttributeIsParted).IsParted == true)
                {
                    continue;
                }
                var tempName = pi.Name;
                var pi2 = propertys2.Where(m => m.Name == tempName).FirstOrDefault();
                object value = pi.GetValue(source, null);
                object value2 = pi2.GetValue(obj, null);
                if (value != null && value2 != null)
                {
                    if (!value.Equals(value2))
                    {
                        result = false;
                        break;
                    }
                }
                else
                {
                    if (value == null && value2 == null)
                        continue;
                    if (value != null && value2 == null && string.IsNullOrEmpty(value.ToString()))
                        continue;
                    if (value == null && value2 != null && string.IsNullOrEmpty(value2.ToString()))
                        continue;
                    if (value!=value2)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public static object[] ConvertToObjectArray(this Guid[] objs)
        {
            var objList = new List<object>();
            foreach (var item in objs)
                objList.Add(item);
            return objList.ToArray();
        }

        public static object[] ConvertToObjectArray(this int[] objs)
        {
            var objList = new List<object>();
            foreach (var item in objs)
                objList.Add(item);
            return objList.ToArray();
        }

        public static object[] ConvertToObjectArray(this string[] objs)
        {
            var objList = new List<object>();
            foreach (var item in objs)
                objList.Add(item);
            return objList.ToArray();
        }

    }
}
