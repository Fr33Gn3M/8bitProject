using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class BinaryImage
	{
		[DataMember()]
		public byte[] Content { get; set; }
		[DataMember()]
		public int Width { get; set; }
		[DataMember()]
		public int Height { get; set; }
		[DataMember()]
		public string ContentType { get; set; }
	}
}
