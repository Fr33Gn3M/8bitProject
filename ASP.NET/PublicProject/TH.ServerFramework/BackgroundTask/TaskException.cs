using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.BackgroundTask
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class TaskException
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string StackTraceText { get; set; }

        public static TaskException Parse(Exception ex)
        {
            var instance = new TaskException();
            instance.Message = ex.Message;
            instance.Name = ex.GetType().FullName;
            instance.StackTraceText = ex.StackTrace;
            return instance;
        }
    }

}
