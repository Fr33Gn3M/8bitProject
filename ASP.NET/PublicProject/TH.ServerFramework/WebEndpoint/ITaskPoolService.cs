namespace TH.ServerFramework.WebEndpoint
{
    using ServerFramework.BackgroundTask;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface ITaskPoolService
    {
        [OperationContract]
        void AbortTask(Guid token, string poolName, Guid taskId);
        [OperationContract]
        TimedTaskStatus[] GetAllTimedTaskStatus(Guid token, string poolName, Guid taskId);
        [OperationContract]
        byte[] GetSerializedTaskResult(Guid token, string poolName, Guid taskId);
        [OperationContract]
        TaskPoolRepoOptions GetServiceOptions(Guid token);
        [OperationContract]
        TaskPoolOptions GetTaskPoolOptions(Guid token, string poolName);
        [OperationContract]
        TaskExecutingProgress GetTaskProgress(Guid token, string poolName, Guid taskId);
        [OperationContract]
        void UpdateServiceOptions(Guid token, TaskPoolRepoOptions serviceOptions);
        [OperationContract]
        void UpdateTaskPoolOptions(Guid token, string poolName, TaskPoolOptions poolOptions);
    }
}

