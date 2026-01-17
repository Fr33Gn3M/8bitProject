using System.IO;
using System.Xml.Serialization;

namespace LX.Commons
{
    public class XmlSerializeHelper
    {
        public static void XmlSerialize<T>(T model, string filepath)
        {
            XmlSerializer helper = new XmlSerializer(typeof(T));
            var stream = new MemoryStream();
            helper.Serialize(stream, model);
            File.WriteAllBytes(filepath, stream.ToArray());
            stream.Dispose();
        }

        public static T XmlDeserialize<T>(string filepath)
        {
            XmlSerializer helper = new XmlSerializer(typeof(T));
            var stream = new MemoryStream(File.ReadAllBytes(filepath));
            if (stream.Length == 0)
                return default(T);
            var t = (T)helper.Deserialize(stream);
            stream.Dispose();
            return t;
        }
    }
}
