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
	public class ColorSymbol : SymbolBase
	{
		[DataMember()]
		public Color Color { get; set; }
	}
}
