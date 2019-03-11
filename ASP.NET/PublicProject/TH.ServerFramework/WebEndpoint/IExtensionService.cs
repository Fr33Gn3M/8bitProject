namespace TH.ServerFramework.WebEndpoint
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IExtensionService
    {
        [OperationContract]
        IDictionary<string, string> GetExtensionArguments(Guid token, string extensionName);
        [OperationContract]
        string[] GetLoadedExtensionNames(Guid token);
        [OperationContract]
        bool IsExtensionEnabled(Guid token, string extensionName);
        [OperationContract]
        void SetExtensionEnableStatus(Guid token, string extensionName, bool enabled);
    }
}

