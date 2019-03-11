using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.UserManager
{
     [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class ModuleInfo
    {
         [DataMember]
         public int Index { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
         [DataMember]
         public string ModuleTitle { get; set; }
         [DataMember]
         public string Remark { get; set; }
         [DataMember]
         public string ImagePath { get; set; }
         [DataMember]
         public string ServiceUrl { get; set; }
         [DataMember]
         public bool IsVisible { get; set; }
         [DataMember]
         public IDictionary<string, string> Properties { get; set; }
    }



}
