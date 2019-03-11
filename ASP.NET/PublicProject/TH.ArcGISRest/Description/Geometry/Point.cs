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
	public class Point : GeometryBase
	{
		[DataMember()]
		public double X { get; set; }
		[DataMember()]
		public double Y { get; set; }
	}
}
