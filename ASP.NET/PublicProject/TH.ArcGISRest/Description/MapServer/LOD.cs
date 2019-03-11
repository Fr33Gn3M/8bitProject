using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.MapServer
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class LOD
	{
		[DataMember()]
		public int Level { get; set; }
		[DataMember()]
		public double Resolution { get; set; }
		[DataMember()]
		public double Sacle { get; set; }
	}
}
