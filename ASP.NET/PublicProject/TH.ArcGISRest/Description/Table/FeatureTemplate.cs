using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.Description.Table
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class FeatureTemplate
    {
        [DataMember()]
        public String Name { get; set; }
        [DataMember()]
        public String Description { get; set; }
        [DataMember()]
        public Feature Prototype { get; set; }
        [DataMember()]
        public DrawingTool? DrawingTool { get; set; }
    }
}
