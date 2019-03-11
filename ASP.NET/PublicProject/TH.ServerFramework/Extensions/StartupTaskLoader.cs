namespace TH.ServerFramework.Extensions
{
    using Bootstrap.Extensions.StartupTasks;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class StartupTaskLoader
    {

        public event OnTaskLoadExceptionUnhandledEventHandler OnTaskLoadExceptionUnhandled;
        public delegate void OnTaskLoadExceptionUnhandledEventHandler(object sender, TaskExceptionUnhandledArgs e);
        public event OnTaskResetExceptionUnhandledEventHandler OnTaskResetExceptionUnhandled;
        public delegate void OnTaskResetExceptionUnhandledEventHandler(object sender, TaskExceptionUnhandledArgs e);

        private readonly IList<IStartupTask> _startupTasks;

        private readonly IList<IStartupTask> _resetTasks;
        public StartupTaskLoader(params IStartupTask[] startupTasks)
        {
            _startupTasks = startupTasks.ToList();
            _resetTasks = _startupTasks.Reverse().ToList();
        }

        public void SetResetTasks(params IStartupTask[] resetTasks)
        {
            _resetTasks.Clear();
            foreach (var resetTask in resetTasks)
            {
                _resetTasks.Add(resetTask);
            }
        }

        public void LoadAll()
        {
#if DEBUG
            foreach (var task in _startupTasks)
            {
                task.Run();
#else
				try {
					task.Run();
				} catch (Exception ex) {
					dynamic e = new TaskExceptionUnhandledArgs(task, ex);
					if (OnTaskLoadExceptionUnhandled != null) {
						OnTaskLoadExceptionUnhandled(this, e);
					}
					if (!e.IsHandled) {
						throw ex;
					}
				}
#endif

            }
        }

        public void ResetAll()
        {
#if DEBUG
            foreach (var task in _resetTasks)
            {
                task.Reset();
#else
				try {
					task.Reset();
				} catch (Exception ex) {
					var e = new TaskExceptionUnhandledArgs(task, ex);
					if (OnTaskResetExceptionUnhandled != null) {
						OnTaskResetExceptionUnhandled(this, e);
					}
					if (!e.IsHandled) {
						throw ex;
					}
				}
#endif
            }
        }

    }
}

