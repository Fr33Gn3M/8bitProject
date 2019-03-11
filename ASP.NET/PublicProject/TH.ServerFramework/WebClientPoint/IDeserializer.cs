namespace TH.ServerFramework.WebClientPoint
{
    using System;
    using System.Collections.Generic;

    public interface IDeserializer
    {
        T DeserializeObject<T>(byte[] content);

        IDictionary<string, string> RequestParams { get; set; }
    }
}

