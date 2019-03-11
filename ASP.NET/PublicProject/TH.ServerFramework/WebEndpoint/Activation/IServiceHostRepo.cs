namespace TH.ServerFramework.WebEndpoint.Activation
{
    using System;
    using System.Collections.Generic;

    public interface IServiceHostRepo
    {
        void AddSource(Func<IDictionary<Type, Uri>> serviceTypeUrlSource);
        Uri GetBaseUrl(Type serviceType);
        Type[] GetServiceTypes();
    }
}

