using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TH.DataBase
{
    [XmlRootAttribute("SqlModelArray")]
    public class SqlModelArray
    {
        [XmlArrayAttribute("Sqls")]
        public SqlModel[] Sqls
        {
            get;
            set;
        }
    }

    [XmlRootAttribute("SqlModel")]
    public class SqlModel
    {
        private string m_SqlType;
        [XmlElementAttribute("SqlType")]
        public string SqlType
        {
            get { return m_SqlType; }
            set
            {
                m_SqlType = value;
            }
        }

        private string m_NameSpaceName;
        [XmlElementAttribute("NameSpaceName")]
        public string NameSpaceName
        {
            get { return m_NameSpaceName; }
            set
            {
                m_NameSpaceName = value;
            }
        }

        private string m_ModelName;
        [XmlElementAttribute("ModelName")]
        public string ModelName
        {
            get { return m_ModelName; }
            set
            {
                m_ModelName = value;
            }
        }

        private string m_Value;
        [XmlElementAttribute("Value")]
        public string Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
            }
        }
    }
}
