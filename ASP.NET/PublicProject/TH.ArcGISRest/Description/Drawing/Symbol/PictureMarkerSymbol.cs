using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing.Symbol
{
	[KnownType(typeof(PictureFillSymbol))]
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class PictureMarkerSymbol : SymbolBase
	{
		[DataMember()]
		public BinaryImage Image { get; set; }
		[DataMember()]
		public Color Color { get; set; }
		[DataMember()]
		public double Angle { get; set; }
		[DataMember()]
		public double XOffset { get; set; }
		[DataMember()]
		public double YOffset { get; set; }
	}
}
