namespace TH.ServerFramework.WebClientPoint
{
    using System;
    using System.Collections.Generic;

    public interface IResourceHandler
    {
        IDictionary<string, object> DefaultParams { get; set; }

        ISerializer DefaultParamSerializer { get; set; }

        HttpRequestMethod Method { get; set; }

        IDictionary<string, ISerializer> ParamSerializers { get; set; }

        IDictionary<string, string> RequestHeaders { get; set; }

        IStringMatcher ResourceMatcher { get; set; }

        IDeserializer ResponseDeserializer { get; set; }
    }
}

