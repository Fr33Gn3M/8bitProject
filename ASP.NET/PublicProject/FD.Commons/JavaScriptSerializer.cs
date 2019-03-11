using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FD.Commons
{
    public class JavaScriptSerializer : ISerializer
    {
        public static JavaScriptSerializer Json = new JavaScriptSerializer();
        private readonly UTF8Encoding _encoding;
        public JavaScriptSerializer()
        {
            _encoding = new UTF8Encoding();
        }

        public string Serialize<T>(T message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            var result = JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Formatting = Formatting.Indented
            });
            return result;
            // return _encoding.GetBytes(result);
        }

        public T Deserialize<T>(string bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            return JsonConvert.DeserializeObject<T>(bytes);
        }

        public string Serialize(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            var result = JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Formatting = Formatting.Indented
            });
            return result;
            // return _encoding.GetBytes(result);
        }

        public object Deserialize(string bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            return JsonConvert.DeserializeObject(bytes);
        }
    }
}
