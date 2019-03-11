using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ServerFramework.BlobStore
{
    [Serializable()]
    [DataContract()]
    public class BlobItem
    {
        [DataMember()]
        public string ItemName { get; set; }
        [DataMember()]
        public string SectionName { get; set; }
        [DataMember()]
        public IDictionary<string, string> Properties { get; set; }
        [DataMember()]
        public byte[] Content { get; set; }
        [DataMember()]
        public System.DateTime CreatedDate { get; set; }
        [DataMember()]
        public System.DateTime ModifiedDate { get; set; }
    }
}
