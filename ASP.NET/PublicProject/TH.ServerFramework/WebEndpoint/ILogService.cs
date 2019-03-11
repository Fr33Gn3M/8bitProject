namespace TH.ServerFramework.WebEndpoint
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface ILogService
    {
        //[OperationContract]
        //long GetLogArchieveItemCount(Guid token, string catalog, DateTime archive);
        //[OperationContract]
        //DateTime[] GetLogArchieves(Guid token, string catalog);
        //[OperationContract]
        //string[] GetLogCatalogs(Guid token);
        //[OperationContract]
        //LogItemBase[] GetLogs(Guid token, string catalog, DateTime archive, long offset, int pageSize);
    }
}

