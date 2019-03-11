namespace TH.ServerFramework.Extensions
{
    using Bootstrap.Extensions.StartupTasks;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class TaskExceptionUnhandledArgs : EventArgs
    {
        private readonly IStartupTask _currentTask;
        private readonly Exception _ex;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _IsHandled;

        internal TaskExceptionUnhandledArgs(IStartupTask currentTask, Exception ex)
        {
            this._ex = ex;
            this._currentTask = currentTask;
        }

        public IStartupTask CurrentTask
        {
            get
            {
                return this._currentTask;
            }
        }

        public Exception Ex
        {
            get
            {
                return this._ex;
            }
        }

        public bool IsHandled
        {
            [DebuggerNonUserCode]
            get
            {
                return this._IsHandled;
            }
            [DebuggerNonUserCode]
            set
            {
                this._IsHandled = value;
            }
        }
    }
}

