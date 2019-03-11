using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.ServerFramework.BackgroundTask
{
  public  interface ITaskPool
    {
        TaskPoolOptions GetTaskPoolOptions();
        void UpdateTaskPollOptions(TaskPoolOptions newValue);
        Guid RunTaskBackground(ITaskExecutor taskExecutor, TaskCreationOptions taskOption, TimeSpan timeout);
        TimedTaskStatus[] GetAllTimedTaskStatus(Guid taskId);
        ITaskProgressNotify GetTaskProgressNotify(Guid taskId);
        T GetTaskResult<T>(Guid taskId);
        void AbortTask(Guid taskId);
        Exception GetLastException(Guid taskId);
        Guid[] GetRunningTaskIds();
    }
}
