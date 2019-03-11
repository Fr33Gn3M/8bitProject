using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.FeatureServer
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class FeatureServerInfo
	{
		[DataMember()]
		public string ServiceDescription { get; set; }
		[DataMember()]
		public IdNamePair[] LayerIndexs { get; set; }
		[DataMember()]
		public IdNamePair[] TableIndexs { get; set; }
	}
}
