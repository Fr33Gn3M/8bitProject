using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace LX.Commons
{
    public class ConvertTomodel
    {
        /// <summary>
        /// 模型转换方法
        /// </summary>
        /// <typeparam name="Source">原传入模型</typeparam>
        /// <typeparam name="Target">传出模型</typeparam>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static Target ConvertModel<Source, Target>(Source sourceObject)
         where Source : class
         where Target : class, new()
        {
            var ret = new Dictionary<object, object>();
            Dictionary<string, object> dic = new Dictionary<string, object>();

            PropertyInfo[] properties = sourceObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(sourceObject, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);
                }
            }

            XmlDocument xmldoc = new XmlDocument();
            var binPath = AppDomain.CurrentDomain.BaseDirectory;
            xmldoc.Load(binPath + @"Rules\source_target.xml");
            //xmldoc.Load(@"D:\\SVN开发软件\\trunk\\Web系统服务端\\01源代码\\WebServer\\PublicProject\\TH.ModelConvert\\Rules\\source_target.xml");
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlElement element in topM)
            {
                if (element.Name.ToLower() == "filedlist")
                {
                    XmlNodeList nodelist = element.ChildNodes;
                    if (nodelist.Count > 0)
                    {
                        foreach (XmlElement el in nodelist)
                        {
                            dic.Add(el.LastChild.InnerText.Trim(), ret[el.FirstChild.InnerText.Trim()]);
                        }
                    }
                }
            }

            Target target = new Target();
            target = Activator.CreateInstance<Target>();
            foreach (KeyValuePair<string, object> item in dic)
            {
                PropertyInfo prop = target.GetType().GetProperty(item.Key);
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    object value = item.Value;
                    Type itemType = Nullable.GetUnderlyingType(prop.PropertyType) == null ? prop.PropertyType : Nullable.GetUnderlyingType(prop.PropertyType);
                    prop.SetValue(target, Convert.ChangeType(value, itemType), null);
                }
            }
            return target;
        }

    }
}
