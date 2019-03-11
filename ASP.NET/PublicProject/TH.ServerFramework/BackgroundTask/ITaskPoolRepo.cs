using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
  public  interface ITaskPoolRepo
    {
        ITaskPool GetTaskPool(string name = null);
        void CreateTaskPool(string name, TaskPoolOptions taskPoolOptions = null);
        void RemoveTaskPool(string name);
        TaskPoolRepoOptions GetRepoOptions();
        void UpdateRepoOptions(TaskPoolRepoOptions repoOptions);
        void StopService();
        void StartService();

    }
}
