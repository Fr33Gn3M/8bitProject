using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Geometry
{
	[KnownType(typeof(Point))]
	[KnownType(typeof(Multipoint))]
	[KnownType(typeof(Polyline))]
	[KnownType(typeof(Polygon))]
	[KnownType(typeof(Envelope))]
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public abstract class GeometryBase
	{
		[DataMember()]
		public SpatialReference SpatialReference { get; set; }
	}
}
