namespace TH.ServerFramework.WebClientPoint.Predefined
{
    using Newtonsoft.Json;
    using TH.ServerFramework.WebClientPoint;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class JsonDeserializer : IDeserializer
    {
        private readonly Func<JsonDeserializer, JsonConverter[]> _converterFactory;

        public JsonDeserializer(params JsonConverter[] converters)
        {
           Func<JsonDeserializer, JsonConverter[]>  factory= (JsonDeserializer instance) => { return converters; };
            _converterFactory = factory;
        }
        public JsonDeserializer(Func<JsonDeserializer, JsonConverter[]> convertersFactory)
        {
            _converterFactory = convertersFactory;
        }


        public virtual T DeserializeObject<T>(byte[] content)
        {
            var contentString = Encoding.UTF8.GetString(content);
            var ta = typeof(T);
            var jObj = JsonConvert.DeserializeObject(contentString);
            T result;
            var converters = _converterFactory.Invoke(this);
            if (converters.Any())
            {
                result = JsonConvert.DeserializeObject<T>(contentString, converters);
            }
            else
            {
                result = JsonConvert.DeserializeObject<T>(contentString);
            }
            return result;
        }

        public IDictionary<string, string> RequestParams { get; set; }
    }

}

