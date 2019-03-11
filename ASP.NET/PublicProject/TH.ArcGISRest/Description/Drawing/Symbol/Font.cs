using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing.Symbol
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class Font
	{
		[DataMember()]
		public string Family { get; set; }
		[DataMember()]
		public double Size { get; set; }
		[DataMember()]
		public FontStyle Style { get; set; }
		[DataMember()]
		public FontWeight Weight { get; set; }
		[DataMember()]
		public FontDecoration Decoration { get; set; }
	}
}
