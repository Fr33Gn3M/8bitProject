using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LX.Commons.ExceptionManager
{
    public class MessageTypeCode
    {
        public const int NotKnown = 9999;
        public const int Success = 1000;
        public const int NotFound = 404;

        public const string CodeStr = "CODE";
        public const string IsHideMessageShowStr = "IsHideMessageShow";

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
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\Messages.xml";
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
                    if (m_MessageInfos.ContainsKey(item.Code) == false)
                        m_MessageInfos.Add(item.Code, item);
            }
        }

        public static string GetMessage(int code)
        {
            if (MessageInfos.ContainsKey(code))
                return MessageInfos[code].Message;
            return null;
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

    public class ServiceException : InvalidOperationException
    {
        public int Code { get; set; }
        public ServiceException(string message, bool IsHideMessageShow = false)
        {
            Code = MessageTypeCode.NotKnown;
            FMessage = message;
            Data.Add(MessageTypeCode.CodeStr, Code);
            Data.Add(MessageTypeCode.IsHideMessageShowStr, IsHideMessageShow);
        }

        private string FMessage = string.Empty;
        public override string Message
        {
            get
            {
                return FMessage;
            }
        }

        public ServiceException(int code, bool IsHideMessageShow = false)
        {
            Code = code;
            if (MessageTypeCode.MessageInfos.ContainsKey(code))
                FMessage = MessageTypeCode.MessageInfos[code].Message;
            else
                FMessage = "Code:" + code.ToString();
            Data.Add(MessageTypeCode.CodeStr, Code);
            Data.Add(MessageTypeCode.IsHideMessageShowStr, IsHideMessageShow);
        }

        public ServiceException(int code, string message, bool IsHideMessageShow = false)
        {
            Code = code;
            FMessage = message;
            Data.Add(MessageTypeCode.CodeStr, Code);
            Data.Add(MessageTypeCode.IsHideMessageShowStr, IsHideMessageShow);
        }

    }
}
