// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;



namespace TH.ServerFramework
{
	namespace BackgroundTask
	{
		internal class TaskPool : ITaskPool
		{
			
			private TaskPoolOptions _options;
			private readonly Queue<TaskBlockContext> _taskBlockingQueue;
			private readonly IDictionary<Guid, TaskRunningContext> _runningTasks;
			private readonly IDictionary<Guid, TaskResultContext> _taskResultContextes;
			private object _locker = new object();
			
			public TaskPool(TaskPoolOptions options)
			{
				_options = options;
				_taskBlockingQueue = new Queue<TaskBlockContext>();
				_taskResultContextes = new ConcurrentDictionary<Guid, TaskResultContext>();
				_runningTasks = new ConcurrentDictionary<Guid, TaskRunningContext>();
			}
			
			internal void CleanupTaskResultHistory()
			{
				lock(_locker)
				{
					var taskIdTimes = new Dictionary<Guid, DateTime>();
					foreach (var kv in _taskResultContextes)
					{
						var tskId = kv.Key;
						var tskCtx = kv.Value;
						if (_runningTasks.ContainsKey(tskId))
						{
							continue;
						}
						var complatedTimeStatus = (from e in tskCtx.StatusTimes where e.Status == TaskStatus.ErrorOccured || e.Status == TaskStatus.Success select e).FirstOrDefault();
						if (complatedTimeStatus != null)
						{
							taskIdTimes.Add(tskId, complatedTimeStatus.EventTime);
						}
					}
					if (taskIdTimes.Count < _options.TaskResultHistorySize)
					{
						return ;
					}
					var willCleanupTskIds = ((from e in taskIdTimes orderby e.Value select e).Distinct().Skip((int)_options.TaskResultHistorySize).Select(e => e.Key)).ToArray();
					foreach (var tskId in willCleanupTskIds)
					{
						_taskResultContextes.Remove(tskId);
					}
				}
			}
			
			internal void CleanupTimeoutTasks()
			{
				lock(_locker)
				{
					var runningTasks = new Dictionary<Guid, TaskRunningContext>(_runningTasks);
					foreach (var kv in runningTasks)
					{
						var taskId = kv.Key;
						var runningTskCtx = kv.Value;
						if (runningTskCtx.Timeout == TimeSpan.Zero)
						{
							continue;
						}
						var tskResultCtx = _taskResultContextes[taskId];
						var runTime = (from e in tskResultCtx.StatusTimes where e.Status == TaskStatus.Running select e.EventTime).FirstOrDefault();
						if (DateTime.Now - runTime >= runningTskCtx.Timeout)
						{
							AbortTask(taskId);
						}
					}
				}
			}
			
			internal void EnqueueBlockingQueue()
			{
				do
				{
					lock(_locker)
					{
						if (_taskBlockingQueue.Count == 0 || _runningTasks.Count >= _options.MaxRunningTasks)
						{
							return ;
						}
						var blockCtx = _taskBlockingQueue.Dequeue();
						var runningCtx = new TaskRunningContext();
						runningCtx.Canceler = new CancellationTokenSource();
						runningCtx.Timeout = blockCtx.Timeout;
						runningCtx.TaskExecutor = blockCtx.TaskExecutor;
						_runningTasks.Add(blockCtx.TaskId, runningCtx);
						var proxyTaskExecutor = new TaskExecutorProxy(blockCtx.TaskId, this, blockCtx.TaskExecutor);
						Task.Factory.StartNew(proxyTaskExecutor.OnExecute, runningCtx.Canceler.Token, blockCtx.TaskOptions,TaskScheduler.Default);
					}
					Thread.Sleep(30);
				} while (true);
			}
			
			public void AbortTask(Guid taskId)
			{
				lock(_locker)
				{
					if (!_runningTasks.ContainsKey(taskId))
					{
						throw (new ArgumentException("taskId"));
					}
					var tskCtx = _runningTasks[taskId];
					if (tskCtx.TaskExecutor is ITaskTransaction)
					{
                        ITaskTransaction transaction = (ITaskTransaction)tskCtx.TaskExecutor;
						transaction.OnRollback();
					}
					tskCtx.Canceler.Cancel();
					_runningTasks.Remove(taskId);
				}
			}
			
			public TimedTaskStatus[] GetAllTimedTaskStatus(Guid taskId)
			{
				if (!_taskResultContextes.ContainsKey(taskId))
				{
					return null;
				}
				var ctx = _taskResultContextes[taskId];
				return ctx.StatusTimes;
			}
			
			public ITaskProgressNotify GetTaskProgressNotify(Guid taskId)
			{
				return this.GetTaskNotify(taskId);
			}
			
			public ITaskProgressNotify GetTaskNotify(Guid taskId)
			{
				lock(_locker)
				{
					if (_taskBlockingQueue.Any(p => p.TaskId == taskId))
					{
						var progress = new PendingTaskProgress();
						return progress;
					}
					else if (_runningTasks.ContainsKey(taskId))
					{
						var taskCtx = _runningTasks[taskId];
						if (taskCtx.TaskExecutor is ITaskProgressNotify)
						{
                            return (ITaskProgressNotify)taskCtx.TaskExecutor;
						}
						else
						{
							throw (new NotSupportedException());
						}
					}
					else if (_taskResultContextes.ContainsKey(taskId))
					{
						var taskCtx = _taskResultContextes[taskId];
						return taskCtx.LastProgress;
					}
					else
					{
						return null;
					}
				}
			}
			
			public TaskPoolOptions GetTaskPoolOptions()
			{
				return _options;
			}
			
			public T GetTaskResult<T>(Guid taskId)
			{
				if (_taskResultContextes.ContainsKey(taskId))
				{
					var taskCtx = _taskResultContextes[taskId];
					if (taskCtx.LastProgress.IsComplated())
					{
						return taskCtx.Result;
					}
					else
					{
						return default(T);
					}
				}
				else
				{
					throw (new ArgumentException("taskId"));
				}
			}
			
			public Guid RunTaskBackground(ITaskExecutor taskExecutor, TaskCreationOptions taskOptions, TimeSpan timeout)
			{
				lock(_locker)
				{
					if (_taskBlockingQueue.Count + _runningTasks.Count >= _options.MaxQueueLength + _options.MaxRunningTasks)
					{
						throw (new Exception());
					}
					if (taskOptions == TaskCreationOptions.None)
					{
						taskOptions = _options.DefaultTaskCreationOptions;
					}
					if (timeout == TimeSpan.Zero)
					{
						timeout = _options.TaskResultTtl;
					}
					var taskResultCtx = new TaskResultContext();
                    taskResultCtx.StatusTimes = new TimedTaskStatus[] { new TimedTaskStatus { EventTime = DateTime.Now, Status = TaskStatus.InQueued } };
					var tskId = Guid.NewGuid();
					_taskResultContextes.Add(tskId, taskResultCtx);
					var tskBlockCtx = new TaskBlockContext();
					tskBlockCtx.TaskExecutor = taskExecutor;
					tskBlockCtx.TaskOptions = taskOptions;
#if DEBUG
					tskBlockCtx.TaskOptions = tskBlockCtx.TaskOptions | TaskCreationOptions.AttachedToParent;
#endif
					tskBlockCtx.Timeout = timeout;
					tskBlockCtx.TaskId = tskId;
					_taskBlockingQueue.Enqueue(tskBlockCtx);
					return tskId;
				}
			}
			
			public void UpdateTaskPollOptions(TaskPoolOptions newValue)
			{
				_options = newValue;
			}
			
			public Exception GetLastException(Guid taskId)
			{
				var tskResultCtx = _taskResultContextes[taskId];
				return tskResultCtx.LastException;
			}
			
			internal class TaskBlockContext
			{
				public Guid TaskId {get; set;}
				public ITaskExecutor TaskExecutor {get; set;}
				public TaskCreationOptions TaskOptions {get; set;}
				public TimeSpan Timeout {get; set;}
			}
			
			internal class TaskRunningContext
			{
				public ITaskExecutor TaskExecutor {get; set;}
				public CancellationTokenSource Canceler {get; set;}
				public TimeSpan Timeout {get; set;}
			}
			
			internal class TaskResultContext
			{
				public dynamic Result {get; set;}
				public TimedTaskStatus[] StatusTimes {get; set;}
				public ITaskProgressNotify LastProgress {get; set;}
				public Exception LastException {get; set;}
			}
			
			private class TaskExecutorProxy : ITaskExecutor
			{
				
				private readonly ITaskExecutor _realTaskExecutor;
				private readonly TaskPool _taskPool;
				private readonly Guid _taskId;
				
				public TaskExecutorProxy(Guid taskId, TaskPool taskPool, ITaskExecutor realTaskExecutor)
				{
					_realTaskExecutor = realTaskExecutor;
					_taskPool = taskPool;
					_taskId = taskId;
				}
				
				public void OnExecute()
				{
					Exception lastEx = null;
					bool? forceComplated = null;
					var status = new TimedTaskStatus {EventTime = DateTime.Now, Status = TaskStatus.Running};
					var tskResultCtx = _taskPool._taskResultContextes[_taskId];
					try
					{
						tskResultCtx.StatusTimes = tskResultCtx.StatusTimes.Union(new[] {status}).ToArray();
						_realTaskExecutor.OnExecute();
						if (_realTaskExecutor is ITaskResult)
						{
                            ITaskResult taskResultReader = (ITaskResult)_realTaskExecutor;
							var taskResult = taskResultReader.GetResult();
							tskResultCtx.Result = taskResult;
						}
						status = new TimedTaskStatus {EventTime = DateTime.Now, Status = TaskStatus.Success};
                        tskResultCtx.StatusTimes = tskResultCtx.StatusTimes.Union(new[] { status }).ToArray();
					}
					catch (Exception ex)
					{
						lastEx = ex;
						forceComplated = true;
						tskResultCtx.LastException = ex;
						status = new TimedTaskStatus {EventTime = DateTime.Now, Status = TaskStatus.ErrorOccured};
                        tskResultCtx.StatusTimes = tskResultCtx.StatusTimes.Union(new[] { status }).ToArray();
						if (_realTaskExecutor is ITaskTransaction)
						{
                            ITaskTransaction transaction = (ITaskTransaction)_realTaskExecutor;
							status = new TimedTaskStatus {EventTime = DateTime.Now, Status = TaskStatus.Rollbacking};
                            tskResultCtx.StatusTimes = tskResultCtx.StatusTimes.Union(new[] { status }).ToArray();
							transaction.OnRollback();
							status = new TimedTaskStatus {EventTime = DateTime.Now, Status = TaskStatus.Rollbacked};
                            tskResultCtx.StatusTimes = tskResultCtx.StatusTimes.Union(new[] { status }).ToArray();
						}
					}
					finally
					{
						if (_realTaskExecutor is ITaskProgressNotify)
						{
							tskResultCtx = _taskPool._taskResultContextes[_taskId];
                            var progress = new LastTaskProgress((ITaskProgressNotify)_realTaskExecutor);
							tskResultCtx.LastProgress = progress;
							if (forceComplated != null)
							{
								progress.SetIsComplated(true);
							}
							if (lastEx != null)
							{
								progress.LastException = lastEx;
							}
						}
						if (_taskPool._runningTasks.ContainsKey(_taskId))
						{
							_taskPool._runningTasks.Remove(_taskId);
						}
					}
				}
			}
			
			private class LastTaskProgress : ITaskProgressNotify
			{
				
				private readonly double _currentProgress;
				private readonly string _currentProgressText;
				private readonly double _totalProgress;
				private bool _isComplated;
				private readonly bool _hasProgress;
				
				
				public LastTaskProgress(ITaskProgressNotify taskProgressNotify)
				{
                    _currentProgress = taskProgressNotify.GetCurrentProgress();
                    _currentProgressText = taskProgressNotify.GetCurrentProgressText();
                    _totalProgress = taskProgressNotify.GetTotalProgress();
                    _isComplated = taskProgressNotify.IsComplated();
                    _hasProgress = taskProgressNotify.HasPropgress();
				}
				
				
				public Exception LastException {get; set;}
				
				public void SetIsComplated(bool value)
				{
					_isComplated = value;
				}
				
				public double GetCurrentProgress()
				{
					return _currentProgress;
				}
				
				public string GetCurrentProgressText()
				{
					return _currentProgressText;
				}
				
				public double GetTotalProgress()
				{
					return _totalProgress;
				}
				
				public bool IsComplated()
				{
					return _isComplated;
				}
				
				public bool HasPropgress()
				{
					return _hasProgress;
				}
				
				public Exception GetLastException()
				{
					return LastException;
				}
			}
			
			private class PendingTaskProgress : ITaskProgressNotify
			{
				
				
				public double GetCurrentProgress()
				{
					return 0.0F;
				}
				
				public string GetCurrentProgressText()
				{
					return null;
				}
				
				public double GetTotalProgress()
				{
					return 0.0F;
				}
				
				public bool HasPropgress()
				{
					return false;
				}
				
				public bool IsComplated()
				{
					return false;
				}
				
				public Exception GetLastException()
				{
					return null;
				}
			}



            public Guid[] GetRunningTaskIds()
            {
                throw new NotImplementedException();
            }
        }
	}
}
