using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using System.Collections;
using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;


namespace TH.ServerFramework
{
	namespace BackgroundTask
	{
		internal class TaskPoolRepo : ITaskPoolRepo
		{
			
			private readonly IDictionary<string, TaskPool> _taskPools;
			private TaskPoolRepoOptions _repoOptions;
			private CancellationTokenSource _workerCts;
			
			public TaskPoolRepo()
			{
				_taskPools = new Dictionary<string, TaskPool>();
				_workerCts = new CancellationTokenSource();
			}
			
			public void LoadRepoOptions(TaskPoolRepoOptions repoOptions)
			{
				_repoOptions = repoOptions;
			}
			
			public void CreateTaskPool(string name, TaskPoolOptions taskPoolOptions = null)
			{
				if (_taskPools.ContainsKey(name))
				{
					throw (new ArgumentException("name"));
				}
				if (taskPoolOptions == null)
				{
					taskPoolOptions = Mapper.DynamicMap<TaskPoolOptions>(_repoOptions);
				}
				var taskPool = new TaskPool(taskPoolOptions);
				_taskPools.Add(name, taskPool);
			}
			
			private void Worker()
			{
				do
				{
					Thread.Sleep(_repoOptions.WorkerIdleTimeSpan.Milliseconds);
					foreach (var pool in _taskPools.Values)
					{
						pool.CleanupTaskResultHistory();
						pool.CleanupTimeoutTasks();
						pool.EnqueueBlockingQueue();
					}
				} while (true);
			}
			
			public void StartService()
			{
				Task.Factory.StartNew(Worker, _workerCts.Token);
			}
			
			public void StopService()
			{
				_workerCts.Cancel();
			}
			
			public TaskPoolRepoOptions GetRepoOptions()
			{
				return _repoOptions;
			}
			
			public ITaskPool GetTaskPool(string name = null)
			{
				if (name == null)
				{
					name = (string) _repoOptions.DefaultPoolName;
				}
				return _taskPools[name];
			}
			
			public void RemoveTaskPool(string name)
			{
				throw (new ArgumentException("name"));
			}
			
			public void UpdateRepoOptions(TaskPoolRepoOptions repoOptions)
			{
				_repoOptions = repoOptions;
			}
		}
	}
}
