using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Time
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class TimeReference
	{
		[DataMember()]
		public string TimeZone { get; set; }
		[DataMember()]
		public bool RespectsDaylightSaving { get; set; }
	}
}
