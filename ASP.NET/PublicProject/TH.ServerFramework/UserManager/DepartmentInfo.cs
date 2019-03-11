using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.UserManager
{
    public class DepartmentInfo
    {
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string DepartmentTitle { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public Dictionary<string, string> Properties { get; set; }
        [DataMember]
        public DepartmentInfo[] SubDepartments { get; set; }
        [DataMember]
        public string ParentDepartmentName { get; set; }
    }

  
}
