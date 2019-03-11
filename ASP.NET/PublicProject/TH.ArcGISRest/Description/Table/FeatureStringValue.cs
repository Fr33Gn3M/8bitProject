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
	public class FeatureStringValue : FeatureValueBase
	{
		[DataMember()]
		public string Value { get; set; }
	}
}
