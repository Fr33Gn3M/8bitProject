namespace TH.ServerFramework.WebClientPoint
{
    using System;

    public interface ISerializer
    {
        string Serializer(object obj);

        bool LastSerialized { get; }
    }
}

