using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Reflection;

namespace FD.DataBase
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
        public static string DBXMLSerializer<TTarget>(this object collection) where TTarget : class
        {
            if (collection== null)
                return null;
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
        public static TTarget DBXMLDeserialize<TTarget>(this string arr) where TTarget : class
        {
            if (string.IsNullOrEmpty(arr))
                return null;
            TTarget msg = null;
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(arr)))
            {
                DataContractSerializer xml = new DataContractSerializer(typeof(TTarget));
                msg = xml.ReadObject(ms) as TTarget;//反序列化为ContextMenuItemCollection对象
            }
            return msg;
        }


        
    }
}
