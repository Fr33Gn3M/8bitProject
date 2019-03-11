using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Catelog
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class CatelogServerInfo
	{
		[DataMember()]
		public string CurrentVersion { get; set; }
		[DataMember()]
		public Uri SoapUrl { get; set; }
		[DataMember()]
		public Uri SecureSoapUrl { get; set; }
		[DataMember()]
		public Uri ToekServiceUrl { get; set; }
	}
}
