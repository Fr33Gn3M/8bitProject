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
	public class CatelogFolderItem
	{
		[DataMember()]
		public string CurrentVersion { get; set; }
		[DataMember()]
		public string[] FolderNames { get; set; }
		[DataMember()]
		public KeyValuePair<string, ServiceType>[] ServiceIndexs { get; set; }
	}
}
