namespace TH.ServerFramework.WebEndpoint.RESTServer
{
    using System;

    public interface IServiceProfileRepo
    {
        void AddServiceProfile(string serviceName, IServiceProfile serviceProfile);
        IServiceProfile GetServiceProfile(string serviceName);
    }
}

