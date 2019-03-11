using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using TH.ArcGISRest.Description.Drawing;
using TH.ArcGISRest.Description.FeatureServer;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.Description.Table;
using TH.ArcGISRest.Description.Time;
using TH.ArcGISRest.Description.Table;

namespace TH.ArcGISRest.Description.FeatureServer
{
     [DataContract(Namespace=Namespaces.THArcGISRest)]
   public class FeatureLayerInfo
    {
       [DataMember()]
        public int Id {get;set;} 
        [DataMember()]
        public String Name {get;set;} 
        [DataMember()]
        public FeatureLayerType LayerType {get;set;} 
        [DataMember()]
        public String Description {get;set;} 
        [DataMember()]
        public String CopyrightText {get;set;} 
        [DataMember()]
        public RelationshipPair[] Relationships {get;set;} 
        [DataMember()]
        public EsriGeometryType? GeometryType {get;set;} 
        [DataMember()]
        public int MinScale {get;set;} 
        [DataMember()]
        public int MaxScale {get;set;} 
        [DataMember()]
        public Envelope Extent {get;set;} 
        [DataMember()]
        public DrawingInfo DrawingInfo {get;set;} 
        [DataMember()]
        public TimeInfo TimeInfo {get;set;} 
        [DataMember()]
        public bool HasAttachements {get;set;} 
        [DataMember()]
        public HtmlPopupType HtmlPopupType {get;set;} 
        [DataMember()]
        public String ObjectIdField {get;set;} 
        [DataMember()]
        public String GlobalIdField {get;set;} 
        [DataMember()]
        public String DisplayField {get;set;} 
        [DataMember()]
        public String TypeIdType {get;set;} 
        [DataMember()]
        public Field[] Fields {get;set;} 
        [DataMember()]
        public FeatureTypeInfo[] Types {get;set;} 
        [DataMember()]
        public FeatureTemplate[] Templates {get;set;} 
        [DataMember()]
        public FeatureServiceCapability[] Capabilities {get;set;} 
    }
}
