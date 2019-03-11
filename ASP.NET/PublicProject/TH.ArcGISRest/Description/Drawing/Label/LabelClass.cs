using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using TH.ArcGISRest.Description.Drawing.Symbol;

namespace TH.ArcGISRest.Description.Drawing.Label
{
	public class LabelClass
	{
		[DataMember()]
		public LabelPlacement? LabelPlacement { get; set; }
		[DataMember()]
		public string LabelExpression { get; set; }
		[DataMember()]
		public bool UseCodedValues { get; set; }
		[DataMember()]
		public TextSymbol Symbol { get; set; }
		[DataMember()]
		public double MinScale { get; set; }
		[DataMember()]
		public double MaxScale { get; set; }
	}
}
