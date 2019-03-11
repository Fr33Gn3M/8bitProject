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
	public class Envelope : GeometryBase
	{
		[DataMember()]
		public double XMin { get; set; }
		[DataMember()]
		public double YMin { get; set; }
		[DataMember()]
		public double XMax { get; set; }
		[DataMember()]
		public double YMax { get; set; }
	}
}
