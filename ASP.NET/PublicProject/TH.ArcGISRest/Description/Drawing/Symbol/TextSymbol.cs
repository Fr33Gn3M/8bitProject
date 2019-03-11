using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing.Symbol
{
	public class TextSymbol : SymbolBase
	{
		[DataMember()]
		public Color Color { get; set; }
		[DataMember()]
		public Color BackgroundColor { get; set; }
		[DataMember()]
		public Color BorderLineColor { get; set; }
		[DataMember()]
		public VerticalAlignment VerticalAlignment { get; set; }
		[DataMember()]
		public HorizontalAlignment HorizontalAlignment { get; set; }
		[DataMember()]
		public bool RightToLeft { get; set; }
		[DataMember()]
		public double Angle { get; set; }
		[DataMember()]
		public double XOffset { get; set; }
		[DataMember()]
		public double YOffset { get; set; }
		[DataMember()]
		public bool Kerning { get; set; }
		[DataMember()]
		public Font Font { get; set; }
	}
}
