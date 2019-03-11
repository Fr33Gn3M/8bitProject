using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace TH.ExceptionManager
{
    public class MessageTypeCode
    {
        public const int NotKnown = 9999;
        public const int Success = 1000;
        //public const int UserAuthentication = 101;
        //public const int DefineCode1001 = 1001;
        //public const int DefineCode1002 = 1002;
        //public const int DefineCode1003 = 1003;
        //public const int DefineCode1004 = 1004;

        private static IDictionary<int, MessageInfo> m_MessageInfos;
        internal static IDictionary<int, MessageInfo> MessageInfos
        {
            get
            {
                if (m_MessageInfos == null)
                    Init();
                return m_MessageInfos;
            }
        }

         internal static void Init()
         {
             m_MessageInfos = new Dictionary<int, MessageInfo>();
             //var binPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(MessageTypeCode)).Location);
             var binPath = AppDomain.CurrentDomain.BaseDirectory;
             var path = System.IO.Path.Combine(binPath, @"bin\Messages.xml");
             XmlSerializer xml = new XmlSerializer(typeof(MessageInfoArr));
             if (System.IO.File.Exists(path) == false)
             {
                 m_MessageInfos = null;
                 return;
             }
             var a = File.ReadAllBytes(path);
             MemoryStream stream = new MemoryStream(a);
             var mRuleArray = xml.Deserialize(stream) as MessageInfoArr;
             if (mRuleArray != null)
             {
                 foreach (var item in mRuleArray.MessageInfos)
                     m_MessageInfos.Add(item.Code, item);
             }
         }
    }

    public class MessageInfoArr
    {
        public MessageInfo[] MessageInfos { get; set; }
    }

    public class MessageInfo
    {
        public string Level { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class ServiceException : Exception
    {
        public int Code { get; set; }
        public ServiceException(string message)
        {
            Code = MessageTypeCode.NotKnown;
            FMessage = message;
        }

        private string FMessage = string.Empty;
        public override string Message
        {
            get
            {
                return FMessage;
            }
        }

        public ServiceException(int code)
        {
            Code = code;
            if (MessageTypeCode.MessageInfos.ContainsKey(code))
                FMessage = MessageTypeCode.MessageInfos[code].Message;
            else
                FMessage = "Code:"+code.ToString();
        }

        public ServiceException(int code,string message)
        {
            Code = code;
            FMessage = message;
        }

    }
}
