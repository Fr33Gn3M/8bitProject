namespace TH.ServerFramework.WebEndpoint
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface ICacheAdministratorService
    {
        [OperationContract]
        void ClearCache(Guid token, string cacheName);
        [OperationContract]
        string[] GetAllCacheKeys(Guid token, string cacheName);
        [OperationContract]
        long GetCacheItemsCount(Guid token, string cacheName);
        [OperationContract]
        string[] GetCacheNames(Guid token);
        [OperationContract]
        void ResetCacheValue(Guid token, string cacheName, string cacheKey);
    }
}

