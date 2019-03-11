using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing.Render
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class ClassBreaksRenderer : RendererBase
	{
		[DataMember()]
		public string Field { get; set; }
		[DataMember()]
		public double MinValue { get; set; }
		[DataMember()]
		public ClassBreakInfo[] ClassBreakInfos { get; set; }
	}
}
