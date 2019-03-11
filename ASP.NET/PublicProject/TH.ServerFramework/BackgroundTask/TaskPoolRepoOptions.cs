using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class TaskPoolRepoOptions : TaskPoolOptions
    {
        [DataMember]
        public TimeSpan WorkerIdleTimeSpan { get; set; }
        [DataMember]
        public string DefaultPoolName { get; set; }
    }

}
