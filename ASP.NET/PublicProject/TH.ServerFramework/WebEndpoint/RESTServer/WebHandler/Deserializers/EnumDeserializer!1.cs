namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler.Deserializers
{
    using System;
    using System.Runtime.CompilerServices;

    public class EnumDeserializer<T> : IQueryStringDeserializer<T>
    {
        public T Deserialize(string raw)
        {
            return (T)Enum.Parse(typeof(T), raw);
        }
    }
}

