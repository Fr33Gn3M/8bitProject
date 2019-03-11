using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class IdNamePair
	{
		[DataMember()]
		public int ID { get; set; }
		[DataMember()]
		public string Name { get; set; }
	}
}
