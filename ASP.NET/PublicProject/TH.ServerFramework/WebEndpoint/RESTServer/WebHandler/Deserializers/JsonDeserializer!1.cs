namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler.Deserializers
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    public class JsonDeserializer<T> : IQueryStringDeserializer<T>
    {
        private readonly object _context;

        public JsonDeserializer(object content)
        {
            this._context = content;
        }

        public JsonDeserializer()
        {
        }

        public T Deserialize(string raw)
        {
            if (string.IsNullOrEmpty(raw))
            {
                return default(T);
            }
            if (this._context == null)
            {
                return JsonConvert.DeserializeObject<T>(raw);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            StreamingContext s = new StreamingContext(StreamingContextStates.All, this._context);
            settings.Context = s;
            return JsonConvert.DeserializeObject<T>(raw, settings);
        }
    }
}

