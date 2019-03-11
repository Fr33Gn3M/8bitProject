namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;
    using System.Threading.Tasks;

    public class TaskPoolOptionsElem : ConfigurationElement
    {
        private const string DefaultTaskCreationOptionsPropName = "defaultTaskCreationOptions";
        private const string MaxQueueLengthPropName = "maxQueueLength";
        private const string MaxRunningTasksPropName = "maxRunningTasks";
        private const string MaxTaskTimeoutPropName = "maxTaskTimeout";
        private const string TaskResultHistorySizePropName = "taskResultHistorySize";
        private const string TaskResultTtlPropName = "taskResultTtl";

        [ConfigurationProperty(DefaultTaskCreationOptionsPropName)]
        public TaskCreationOptions DefaultTaskCreationOptions
        {
            get
            {
                return (TaskCreationOptions)this[DefaultTaskCreationOptionsPropName];
            }
            set
            {
                this[DefaultTaskCreationOptionsPropName] = value;
            }
        }

        [ConfigurationProperty(MaxQueueLengthPropName, IsRequired = false)]
        public long? MaxQueueLength
        {
            get
            {
                return (long?)this[MaxQueueLengthPropName];
            }
            set
            {
                this[MaxQueueLengthPropName] = value;
            }
        }

        [ConfigurationProperty(MaxRunningTasksPropName)]
        public int MaxRunningTasks
        {
            get
            {
                return (int)this[MaxRunningTasksPropName];
            }
            set
            {
                this[MaxRunningTasksPropName] = value;
            }
        }

        [ConfigurationProperty(MaxTaskTimeoutPropName, IsRequired = false)]
        public TimeSpan? MaxTaskTimeout
        {
            get
            {
                return (TimeSpan?)this[MaxTaskTimeoutPropName];
            }
            set
            {
                this[MaxTaskTimeoutPropName] = value;
            }
        }

        [ConfigurationProperty(TaskResultHistorySizePropName, IsRequired = false)]
        public long? TaskResultHistorySize
        {
            get
            {
                return (long?)this[TaskResultHistorySizePropName];
            }
            set
            {
                this[TaskResultHistorySizePropName] = value;
            }
        }

        [ConfigurationProperty(TaskResultTtlPropName, IsRequired = false)]
        public TimeSpan? TaskResultTtl
        {
            get
            {
                return (TimeSpan?)this[TaskResultTtlPropName];
            }
            set
            {
                this[TaskResultTtlPropName] = value;
            }
        }
    }
}

