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
	public class UniqueValueRenderer : RendererBase
	{
		[DataMember()]
		public string Field1 { get; set; }
		[DataMember()]
		public string Field2 { get; set; }
		[DataMember()]
		public string Field3 { get; set; }
		[DataMember()]
		public string FieldDelimiter { get; set; }
		[DataMember()]
		public SymbolBase DefaultSymbol { get; set; }
		[DataMember()]
		public string DefaultLabel { get; set; }
		[DataMember()]
		public UniqueValueInfo[] UniqueValueInfos { get; set; }
	}
}
