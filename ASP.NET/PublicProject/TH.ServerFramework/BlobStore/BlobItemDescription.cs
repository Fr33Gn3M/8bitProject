using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.BlobStore
{
    [Serializable]
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class BlobItemDescription
    {
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string SectionName { get; set; }
        [DataMember]
        public IDictionary<string, string> Properties { get; set; }
        [DataMember]
        public long Size { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }
}
