namespace TH.ServerFramework.WebEndpoint
{
    using ServerFramework.BlobStore;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text.RegularExpressions;

    [ServiceContract]
    public interface IBlobStorageService
    {
        [OperationContract]
        void CreateSection(Guid token, string sectionName);
        [OperationContract]
        BlobItemDescription[] FindSectionBlobsByName(Guid token, string sectionName, string itemNameRegexPattern, RegexOptions regexOptions);
        [OperationContract]
        BlobItemDescription[] FindSectionBlobsByProperties(Guid token, string sectionName, IDictionary<string, string> properties, bool ignoreCase);
        [OperationContract]
        BlobItem GetSectionBlob(Guid token, string sectionName, string itemName);
        [OperationContract]
        string[] GetSectionNames(Guid token);
        [OperationContract]
        bool HasBlob(Guid token, string sectionName, string itemName);
        [OperationContract]
        bool HasSection(Guid token, string sectionName);
        [OperationContract]
        void RemoveSection(Guid token, string sectionName);
        [OperationContract]
        void SetBlob(Guid token, string sectionName, BlobItem blobItem);
    }
}

