using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Table
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class CodeValue
	{
		[DataMember()]
		public string Name { get; set; }
		[DataMember()]
		public string Code { get; set; }
	}
}
