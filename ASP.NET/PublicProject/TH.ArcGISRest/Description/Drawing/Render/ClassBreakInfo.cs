using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using TH.ArcGISRest.Description.Drawing.Symbol;

namespace TH.ArcGISRest.Description.Drawing.Render
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class ClassBreakInfo
	{
		[DataMember()]
		public int ClassMaxValue { get; set; }
		[DataMember()]
		public string Label { get; set; }
		[DataMember()]
		public string Description { get; set; }
		[DataMember()]
		public SymbolBase Symbol { get; set; }
	}
}
