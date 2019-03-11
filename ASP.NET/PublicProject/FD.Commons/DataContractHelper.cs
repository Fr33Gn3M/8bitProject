using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Reflection;

namespace FD.Commons
{
    /// <summary>
    /// 服务数据契约转换帮助类
    /// </summary>
    public static class DataContractHelper
    {
        /// <summary>
        /// 系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string Serializer<TTarget>(this object collection) where TTarget : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(TTarget));
                ser.WriteObject(ms, collection);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
        }
        /// <summary>
        /// 反系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static TTarget Deserialize<TTarget>(this string arr) where TTarget : class
        {
            TTarget msg = null;
            ;
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(arr)))
            {
                DataContractSerializer xml = new DataContractSerializer(typeof(TTarget));
                msg = xml.ReadObject(ms) as TTarget;//反序列化为ContextMenuItemCollection对象
            }
            return msg;
        }

        public static TTarget[] ArrayTOModel<TTarget>(this object[] arr) where TTarget : class
        {
            
            if (arr == null || arr.Length == 0)
                return null;
            var list = new List<TTarget>();
            foreach (var item in arr)
            {
                list.Add((TTarget)item);
            }
            return list.ToArray();
        }

        public static object Clone(object source)
        {
            if (source == null)
                return null;
            var sourceSerializer = new DataContractSerializer(source.GetType());
            var targetSerializer = new DataContractSerializer(source.GetType());
            using (var ms = new MemoryStream())
            {
                sourceSerializer.WriteObject(ms, source);
                ms.Position = 0;
                var target = targetSerializer.ReadObject(ms);
                return target;
            }
        }
        /// <summary>
        ///  通过序列化转换对象到其它类型。这两种类型必须具有相同的数据契约。
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源类型对象</param>
        /// <returns>目标类型对象</returns>
        public static TTarget ConverTo<TTarget>(this object source) where TTarget : class
        {
            if (source == null)
                return null;
            var sourceSerializer = new DataContractSerializer(source.GetType());
            var targetSerializer = new DataContractSerializer(typeof(TTarget));
            using (var ms = new MemoryStream())
            {
                sourceSerializer.WriteObject(ms, source);
                ms.Position = 0;
                var target = targetSerializer.ReadObject(ms) as TTarget;
                return target;
            }
        }

        public static ObservableCollection<TTarget> ConverCollectionTo<TSource, TTarget>(this ObservableCollection<TSource> source)
            where TSource : class
            where TTarget : class
        {
            if (source == null)
                return null;
            if (source.Count == 0)
                return new ObservableCollection<TTarget>();
            var result = new ObservableCollection<TTarget>();
            foreach (var sourceItem in source)
            {
                var targetItem = ConverTo<TTarget>(sourceItem);
                result.Add(targetItem);
            }
            return result;
        }

        /// <summary>
        /// 系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string XMLSerializer<TTarget>(this object collection) where TTarget : class
        {
            string xml = null;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer ser = new XmlSerializer(typeof(TTarget));
                ser.Serialize(ms, collection);
                ms.Position = 0;
                xml = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xml;
        }
        /// <summary>
        /// 反系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static TTarget XMLDeserialize<TTarget>(this string xml) where TTarget : class
        {
            if (string.IsNullOrEmpty(xml))
                return null;

            TTarget msg = null;
            var xmlSerializer = new XmlSerializer(typeof(TTarget));
            var xmlBytes = Encoding.UTF8.GetBytes(xml);
            using (MemoryStream ms = new MemoryStream(xmlBytes))
            {
                msg = xmlSerializer.Deserialize(ms) as TTarget;
            }
            return msg;
        }

        public static Dictionary<string, object> AddKeyValue(this Dictionary<string, object> dic, string key, object value)
        {
            if (dic == null)
                dic = new Dictionary<string, object>();
            dic.Add(key, value);
            return dic;
        }

        /// <summary>
        /// 系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string JSONSerializer<TTarget>(this object collection) where TTarget : class
        {
            if (collection == null)
                return null;
            var xml = Newtonsoft.Json.JsonConvert.SerializeObject(collection);
            return xml;
        }
        /// <summary>
        /// 反系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static TTarget JSONDeserialize<TTarget>(this string xml) where TTarget : class
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<TTarget>(xml);
            return msg;
        }

        public static string ArraySerializer(string[] collection)
        {
            if (collection == null)
                return null;
            StringBuilder buider = new StringBuilder();
            int index = 1;
            foreach (var item in collection)
            {
                buider.Append(item);
                if (index != collection.Length)
                    buider.Append(";");
                index++;
            }
            return buider.ToString();
        }

        public static string ArraySerializer(Guid[] collection)
        {
            if (collection == null)
                return null;
            StringBuilder buider = new StringBuilder();
            int index = 1;
            foreach (var item in collection)
            {
                buider.Append("'" + item + "'");
                if (index != collection.Length)
                    buider.Append(";");
                index++;
            }
            return buider.ToString();
        }

        public static string ArraySerializer(int[] collection)
        {
            if (collection == null)
                return null;
            StringBuilder buider = new StringBuilder();
            int index = 1;
            foreach (var item in collection)
            {
                buider.Append(item);
                if (index != collection.Length)
                    buider.Append(";");
                index++;
            }
            return buider.ToString();
        }

        #region dic

        /// <summary>
        /// 系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string DictionarySerializer(this Dictionary<string, string> collection)
        {
            if (collection == null)
                return null;
            var builder = new StringBuilder();
            foreach (var item in collection)
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                    builder.Append(";");
                builder.Append(item.Key + "," + item.Value);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 反系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static Dictionary<string, string> DictionaryDeserialize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var arr = str.Split(';');
            var dic = new Dictionary<string, string>();
            foreach (var item in arr)
            {
                var t = item.Split(',');
                if (t.Length == 2)
                    dic.Add(t[0], t[1]);
            }
            return dic;
        }



        #endregion


        public static string GetJsonString(Type type)
        {
            PropertyInfo[] propertys = type.GetProperties();
            StringBuilder builder = new StringBuilder();
            var count = propertys.Count() - 1;
            int index = 0;
            builder.Append("{");
            foreach (var item in propertys)
            {
                builder.Append("\"" + item.Name + "\":");
                builder.Append("null");
                if (count > index)
                    builder.Append(",");
                index++;
            }
            builder.Append("}");
            return builder.ToString();
        }

    }
}
