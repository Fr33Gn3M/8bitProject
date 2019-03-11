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
	public class SimpleMarkerSymbol : SymbolBase
	{
		[DataMember()]
		public SimpleMarkerSymbolStyle Style { get; set; }
		[DataMember()]
		public Color Color { get; set; }
		[DataMember()]
		public double Size { get; set; }
		[DataMember()]
		public double Angle { get; set; }
		[DataMember()]
		public double XOffset { get; set; }
		[DataMember()]
		public double YOffset { get; set; }
		[DataMember()]
		public SimpleLineSymbol Outline { get; set; }
	}
}
