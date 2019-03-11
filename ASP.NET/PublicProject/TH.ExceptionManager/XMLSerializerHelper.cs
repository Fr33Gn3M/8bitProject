using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Reflection;

namespace TH.ExceptionManager
{
    /// <summary>
    /// 服务数据契约转换帮助类
    /// </summary>
    public static class XMLSerializerHelper
    {
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

    }
}
