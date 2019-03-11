using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using TH.ArcGISRest.Description.Geometry;

namespace TH.ArcGISRest.Description.Table
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class Feature
	{
		[DataMember()]
		public IDictionary<string, FeatureValueBase> Attributes { get; set; }
		[DataMember()]
		public GeometryBase Geometry { get; set; }
	}
}
