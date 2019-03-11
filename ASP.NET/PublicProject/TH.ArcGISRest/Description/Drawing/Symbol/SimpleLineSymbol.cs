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
	public class SimpleLineSymbol : SymbolBase
	{
		[DataMember()]
		public SimpleLineSymbolStyle Style { get; set; }
		[DataMember()]
		public Color Color { get; set; }
		[DataMember()]
		public double Width { get; set; }
	}
}
