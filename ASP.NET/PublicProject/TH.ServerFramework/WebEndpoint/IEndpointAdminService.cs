namespace TH.ServerFramework.WebEndpoint
{
    using ServerFramework.Endpoint;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IEndpointAdminService
    {
        [OperationContract]
        EndpointConfiguration GetEndpointConfiguration(Guid token, string endpointName);
        [OperationContract]
        EndpointDescription[] GetEndpointDescriptions(Guid token);
        [OperationContract]
        EndpointAddressDescription[] GetEndpontAddressDescriptions(string serviceName);
        [OperationContract]
        EndpointConfiguration GetServiceEndpointConfiguration(Guid token, string endpointName, string serviceName);
        [OperationContract]
        void UpdateEndpointConfiguration(Guid token, EndpointConfiguration config);
        [OperationContract]
        void UpdateServiceEndpointConfiguration(Guid token, string serviceName, EndpointConfiguration config);
    }
}

