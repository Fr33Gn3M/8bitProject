namespace TH.ServerFramework.WebClientPoint
{
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public interface IMyRestClientProfile
    {
        void BeforeExecute(RestRequest request);

        IResourceHandler DefaultResourceHandle { get; }

        string Name { get; }

        IList<IResourceHandler> ResourceHandlers { get; }
    }
}

