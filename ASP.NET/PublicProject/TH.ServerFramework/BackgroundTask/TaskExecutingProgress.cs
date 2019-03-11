using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class TaskExecutingProgress
    {
        [DataMember]
        public double TotalProgress { get; set; }
        [DataMember]
        public double CuurentProgress { get; set; }
        [DataMember]
        public bool IsComplated { get; set; }
        [DataMember]
        public TaskException LastException { get; set; }
        [DataMember]
        public bool HasProgress { get; set; }
        [DataMember]
        public string CurrentProgressText { get; set; }
    }

}
