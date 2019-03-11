using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Geometry
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class SpatialReference
	{
		[DataMember()]
		public int Wkid { get; set; }
		[DataMember()]
		public string Wkt { get; set; }
	}
}
