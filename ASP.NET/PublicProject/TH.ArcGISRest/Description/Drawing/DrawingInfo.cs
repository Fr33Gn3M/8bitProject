using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using TH.ArcGISRest.Description.Drawing.Label;
using TH.ArcGISRest.Description.Drawing.Render;

namespace TH.ArcGISRest.Description.Drawing
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class DrawingInfo
	{
		[DataMember()]
		public RendererBase Renderer { get; set; }
		[DataMember()]
		public double Transparency { get; set; }
		[DataMember()]
		public LabelClass[] LabelingInfo { get; set; }
	}
}
