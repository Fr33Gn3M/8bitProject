using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class Color
	{
		[DataMember()]
		public int Red { get; set; }
		[DataMember()]
		public int Green { get; set; }
		[DataMember()]
		public int Blue { get; set; }
		[DataMember()]
		public int Alpha { get; set; }
	}
}
