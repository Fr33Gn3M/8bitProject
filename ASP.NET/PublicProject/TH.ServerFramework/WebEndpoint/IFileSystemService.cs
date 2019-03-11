namespace TH.ServerFramework.WebEndpoint
{
    using ServerFramework.IO;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IFileSystemService
    {
        [OperationContract]
        void CreateFolder(Guid token, string instanceName, string folderPath);
        [OperationContract]
        void CreateProviderInstance(Guid token, string providerName, string instanceName, IDictionary<string, string> arguments);
        [OperationContract]
        byte[] GetFileContent(Guid token, string instanceName, string filePath);
        [OperationContract]
        FileSystemItemBase[] GetFolderSubitems(Guid token, string instanceName, string folderFullPath);
        [OperationContract]
        string[] GetProviderInstanceNames(Guid token);
        [OperationContract]
        void RemoveFileSystemItem(Guid token, string instanceName, string fullPath);
        [OperationContract]
        void WriteFileContent(Guid token, string instanceName, string filePath, byte[] content);
        [OperationContract]
        bool IsExistFolder(Guid token, string instanceName, string folderPath);
        [OperationContract]
        void EditFolderName(Guid token, string instanceName, string oldFolderPath, string newFolderPath);
        [OperationContract]
        string GetDefaultInstanceName(Guid token);
        [OperationContract]
        void SetDefaultInstanceName(Guid token, string instanceName);
        [OperationContract]
        FileSystemItemBase GetFileInfo(Guid token, string instanceName, string fullPath);
        [OperationContract]
        bool IsExistFile(Guid token, string instanceName, string filePath);
        [OperationContract]
        void EditFileName(Guid token, string instanceName, string oldFilePath, string newFilePath);
        [OperationContract]
        byte[] GetImageContent(Guid token, string instanceName, string filePath);
        //[OperationContract]
        //void UploadFile(RemoteFileRequest request);
        //[OperationContract]
        //RemoteFileRequest DownloadFile(DownloadFileRequest request);
    }

    [MessageContract]
    public class DownloadFileRequest
    {
        [MessageHeader]
        public Guid Token;
        [MessageHeader(MustUnderstand = true)]
        public string InstanceName;
        [MessageHeader(MustUnderstand = true)]
        public string FullPath;
    }

    [MessageContract]
    public class RemoteFileRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public Guid Token;
        [MessageHeader(MustUnderstand = true)]
        public string InstanceName;
        [MessageHeader(MustUnderstand = true)]
        public string FullPath;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileData;
    }
}

