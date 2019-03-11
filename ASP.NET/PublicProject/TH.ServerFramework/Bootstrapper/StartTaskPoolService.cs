// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Bootstrap.Extensions.StartupTasks;
using Microsoft.Practices.ServiceLocation;
using TH.ServerFramework.BackgroundTask;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.ObjectMappers;


namespace TH.ServerFramework
{
	namespace Bootstrapper
	{
		internal class StartTaskPoolService : IStartupTask
		{
			
			public void Reset()
			{
				var repo = ServiceLocator.Current.GetInstance<ITaskPoolRepo>();
				repo.StopService();
			}
			
			public void Run()
			{
				var repo = ServiceLocator.Current.GetInstance<ITaskPoolRepo>();
				var section = SettingsSection.GetSection();
				var taskPoolServiceElem = section.ApplicationServices.TaskPoolService;
				var repoOptions = new TaskPoolRepoOptions();
				repoOptions.DefaultPoolName = taskPoolServiceElem.DefaultPool;
				repoOptions.WorkerIdleTimeSpan = taskPoolServiceElem.WorkerIdleTimeSpan;
				SetTaskPoolOptions(repoOptions, taskPoolServiceElem.DefaultTaskPoolOptions);
				repo.UpdateRepoOptions(repoOptions);
				foreach (TaskPoolElem elem in taskPoolServiceElem)
				{
					var taskPoolOptions = new TaskPoolOptions();
					SetTaskPoolOptions(taskPoolOptions, elem);
					repo.CreateTaskPool(elem.Name, taskPoolOptions);
				}
				repo.StartService();
                AutoMapper.Mapper.AddProfile<MaperProfile>();
			}
			
			private void SetTaskPoolOptions(TaskPoolOptions options, TaskPoolOptionsElem optionsElem)
			{
                options.DefaultTaskCreationOptions = optionsElem.DefaultTaskCreationOptions;
				if (optionsElem.MaxQueueLength == null)
				{
                    options.MaxQueueLength = int.MaxValue;
				}
				else
				{
                    options.MaxQueueLength = optionsElem.MaxQueueLength.Value;
				}
                options.MaxRunningTasks = optionsElem.MaxRunningTasks;
				if (optionsElem.MaxTaskTimeout == null)
				{
                    options.MaxTaskTimeout = TimeSpan.Zero;
				}
				else
				{
                    options.MaxTaskTimeout = optionsElem.MaxTaskTimeout.Value;
				}
				if (optionsElem.TaskResultHistorySize == null)
				{
                    options.TaskResultHistorySize = int.MaxValue;
				}
				else
				{
                    options.TaskResultHistorySize = optionsElem.TaskResultHistorySize.Value;
				}
				if (optionsElem.TaskResultTtl == null)
				{
                    options.TaskResultTtl = TimeSpan.Zero;
				}
				else
				{
                    options.TaskResultTtl = optionsElem.TaskResultTtl.Value;
				}
			}
		}
	}
}
