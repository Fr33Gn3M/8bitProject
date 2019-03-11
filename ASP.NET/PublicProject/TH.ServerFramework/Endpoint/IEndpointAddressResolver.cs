namespace TH.ServerFramework.Endpoint
{
    using System;

    public interface IEndpointAddressResolver
    {
        EndpointAddressDescription[] GetEndpontAddressDescriptions(string serviceName);
    }
}

