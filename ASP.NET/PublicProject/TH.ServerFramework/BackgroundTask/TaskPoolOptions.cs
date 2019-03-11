using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TH.ServerFramework.BackgroundTask
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    [KnownType(typeof(TaskPoolRepoOptions))]
    public class TaskPoolOptions
    {
        [DataMember]
        public long MaxQueueLength { get; set; }
        [DataMember]
        public long MaxRunningTasks { get; set; }
        [DataMember]
        public TimeSpan MaxTaskTimeout { get; set; }
        [DataMember]
        public TaskCreationOptions DefaultTaskCreationOptions { get; set; }
        [DataMember]
        public long TaskResultHistorySize { get; set; }
        [DataMember]
        public TimeSpan TaskResultTtl { get; set; }
    }

}
