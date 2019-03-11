using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    public abstract class TaskCommandBase : ITaskExecutor, ITaskResult
    {
        private readonly object _input;
        public TaskCommandBase(object input)
        {
            _input = input;
        }

        public abstract void OnExecute();
        public abstract object GetResult();
    }
}
