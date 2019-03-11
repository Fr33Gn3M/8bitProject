namespace TH.ServerFramework.Endpoint
{
    using System;

    public interface IEndpointAddressResolverRepo
    {
        EndpointAddressDescription[] GetEndpontAddressDescriptions(string serviceName);
        void RegisterResolver(IEndpointAddressResolver resolver);
    }
}

