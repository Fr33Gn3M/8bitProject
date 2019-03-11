namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler.Deserializers
{
    using System;

    public interface IQueryStringDeserializer<T>
    {
        T Deserialize(string raw);
    }
}

