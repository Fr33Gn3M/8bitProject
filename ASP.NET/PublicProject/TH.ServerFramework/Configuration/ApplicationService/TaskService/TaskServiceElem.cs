namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    [ConfigurationCollection(typeof(TaskPoolElem))]
    public class TaskServiceElem : ConfigurationElementCollection
    {
        private const string DefaultPoolPropName = "defaultPool";
        private const string DefaultTaskPoolOptionsPropName = "DefaultTaskPoolOptions";
        private const string WorkerIdleTimeSpanPropName = "workerIdleTimeSpan";

        protected override ConfigurationElement CreateNewElement()
        {
            return new TaskPoolElem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            TaskPoolElem elem = (TaskPoolElem) element;
            return elem.Name;
        }

        [ConfigurationProperty(DefaultPoolPropName)]
        public string DefaultPool
        {
            get
            {
                return (string)this[DefaultPoolPropName];
            }
            set
            {
                this[DefaultPoolPropName] = value;
            }
        }


        private const string TileTaskPoolPropName = "TileTaskPool";
        [ConfigurationProperty(TileTaskPoolPropName)]
        public string TileTaskPool
        {
            get
            {
                return (string)this[TileTaskPoolPropName];
            }
            set
            {
                this[TileTaskPoolPropName] = value;
            }
        }

        [ConfigurationProperty(DefaultTaskPoolOptionsPropName)]
        public TaskPoolOptionsElem DefaultTaskPoolOptions
        {
            get
            {
                return (TaskPoolOptionsElem)this[DefaultTaskPoolOptionsPropName];
            }
            set
            {
                this[DefaultTaskPoolOptionsPropName] = value;
            }
        }

        [ConfigurationProperty(WorkerIdleTimeSpanPropName)]
        public TimeSpan WorkerIdleTimeSpan
        {
            get
            {
                return (TimeSpan)this[WorkerIdleTimeSpanPropName];
            }
            set
            {
                this[WorkerIdleTimeSpanPropName] = value;
            }
        }
    }
}

