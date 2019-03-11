using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using TH.ArcGISRest.Description;
using TH.ArcGISRest.Description.Drawing;
using TH.ArcGISRest.Description.Geometry;
using TH.ArcGISRest.Description.Table;
using TH.ArcGISRest.Description.Time;

namespace TH.ArcGISRest.Description.MapServer
{
    [DataContract(Namespace = Namespaces.THArcGISRest)]
    public class MapLayerInfo
    {
        [DataMember()]
        public int Id { get; set; }
        [DataMember()]
        public String Name { get; set; }
        [DataMember()]
        public String Description { get; set; }
        [DataMember()]
        public MapLayerType LayerType { get; set; }
        [DataMember()]
        public String DefinitionExpression { get; set; }
        [DataMember()]
        public EsriGeometryType? GeometryType { get; set; }
        [DataMember()]
        public String CopyrightText { get; set; }
        [DataMember()]
        public IdNamePair ParentLayeerGroup { get; set; }
        [DataMember()]
        public IdNamePair[] SubLayers { get; set; }
        [DataMember()]
        public int MinSacle { get; set; }
        [DataMember()]
        public int MaxSacle { get; set; }
        [DataMember()]
        public Envelope Extent { get; set; }
        [DataMember()]
        public TimeInfo TimeInfo { get; set; }
        [DataMember()]
        public DrawingInfo DrawingInfo { get; set; }
        [DataMember()]
        public bool HasAttachments { get; set; }
        [DataMember()]
        public HtmlPopupType HtmlPopupType { get; set; }
        [DataMember()]
        public String DisplayField { get; set; }
        [DataMember()]
        public Field[] Fields { get; set; }
        [DataMember()]
        public FeatureTypeInfo[] Types { get; set; }
        [DataMember()]
        public RelationshipPair[] Relationships { get; set; }
        [DataMember()]
        public String TypeIdField { get; set; }
    }
}
