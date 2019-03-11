namespace TH.ServerFramework.Endpoint
{
    using System;

    public interface IEndpointRepo
    {
        EndpointConfiguration GetEndpointConfiguration(string endpointName);
        EndpointConfiguration GetEndpointConfiguration(string serviceName, string endpointName);
        EndpointDescription[] GetEndpointDescriptions();
        void Register(IEndpointProvider provider);
        void RemoveEndpointConfigurationByEndpoint(string endpointName);
        void RemoveEndpointConfigurationByService(string serviceName);
        void Unregister(IEndpointProvider provider);
        void UpdateEndpointConfiguration(EndpointConfiguration config);
        void UpdateEndpointConfiguration(string serviceName, EndpointConfiguration config);
    }
}

